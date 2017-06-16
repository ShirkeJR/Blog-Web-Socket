#define IMPROVED_PACKET_ENCRYPTION

using Blog.Constants;
using Blog.Utils;
using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Blog.Server
{
    internal class ClientData
    {
        private Socket _clientSocket = null;
        private Thread _clientThread = null;
        private bool isOpen = true;
        private int _id = -1;
        public int Id { get { return _id; } set { _id = value; } }
        public Socket ClientSocket { get { return _clientSocket; } set { _clientSocket = value; } }
        public Thread ClientThread { get { return _clientThread; } set { _clientThread = value; } }
        public bool IsOpen { get { return isOpen; } set { isOpen = value; } }

        public ClientData(Socket clientSocket)
        {
            _clientSocket = clientSocket;
            _clientThread = new Thread(dataListening);
            _clientThread.Start(clientSocket);
        }

        public async void dataListening(object cSocket)
        {
            string content = string.Empty;
            Socket clientSocket = (Socket)cSocket;
            LoggingService.Instance.AddLog("*Client: " + ToString() + " open");

            while (isOpen)
            {
                try
                {
                    if (!clientSocket.Connected)
                    {
                        LoggingService.Instance.AddLog("*Client: " + ToString() + " closed");
                        LoggingService.Instance.RemoveClient(this);
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Close();
                        isOpen = false;
                        return;
                    }

                    content = receiveData(clientSocket);

                        if (CryptoService.isEncrypted(content))
                        {
#if IMPROVED_PACKET_ENCRYPTION
                            var content2 = CryptoService.Decrypt<AesManaged>(content.Substring(0, content.Length - StringConstants.PacketEnding.Length), StringConstants.SymmetricKey, StringConstants.SymmetricSalt);
                            content = string.Format("{0}{1}", content2, content.Substring(content.Length - StringConstants.PacketEnding.Length));
#endif
                            if (content.IndexOf(StringConstants.PacketEnding) > -1)
                            {
                                if (content.Split('\t').Length < 3)
                                    defaultSend(clientSocket);
                                else
                                {
                                    LoggingService.Instance.AddLog("> " + ToString() + "\t-->\t" + content);
                                    if (content.StartsWith("4\tEOT\t"))
                                    {
                                        LoggingService.Instance.AddLog("*Client: " + ToString() + " closed");
                                        LoggingService.Instance.RemoveClient(this);
                                        clientSocket.Shutdown(SocketShutdown.Both);
                                        clientSocket.Close();
                                        isOpen = false;
                                        return;
                                    }

                                    content = await PacketAnalyzeService.Instance.getPacketResponse(content, this);
                                    LoggingService.Instance.AddLog("> " + ToString() + "\t<--\t" + content);
#if IMPROVED_PACKET_ENCRYPTION
                                    content = string.Format("{0}{1}", CryptoService.Encrypt<AesManaged>(content.Substring(0, content.Length - StringConstants.PacketEnding.Length), StringConstants.SymmetricKey, StringConstants.SymmetricSalt), StringConstants.PacketEnding);
#endif
                                    sendData(clientSocket, content);
                            }
                            }
                            else
                                defaultSend(clientSocket);
                        }
                        else
                            defaultSend(clientSocket);
                }
                catch (SocketException ex)
                {
                    LoggingService.Instance.WriteException(ex);
                }
            }
        }

        public string receiveData(Socket clientSocket)
        {
            string content = string.Empty;
            byte[] bytesReceived = new byte[1];

            while (!content.EndsWith(StringConstants.PacketEnding))
            {
                clientSocket.Receive(bytesReceived, bytesReceived.Length, 0);
                content = content + Encoding.ASCII.GetString(bytesReceived, 0, 1);
            }
            return content;
        }

        public void sendData(Socket clientSocket, string content)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(content);
            int bytesSend = 0;

            bytesSend = clientSocket.Send(byteData);
            while (bytesSend != byteData.Length)
            {
                LoggingService.Instance.AddLog("Error with sending message, client: " + ToString());
                bytesSend = clientSocket.Send(byteData);
                if (!clientSocket.Connected)
                {
                    LoggingService.Instance.AddLog("*Client: " + ToString() + " closed");
                    LoggingService.Instance.RemoveClient(this);
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    isOpen = false;
                    return;
                }
            }
        }


        public void defaultSend(Socket clientSocket)
        {
            string returnMessage = StringConstants.UnrecognizedCommandAnswer;
            string content = string.Format(StringConstants.GlobalPacketFormat, returnMessage.Length + 1, returnMessage, StringConstants.PacketEnding);
            LoggingService.Instance.AddLog(content);
            sendData(clientSocket, content);
        }

        public override string ToString()
        {
            return string.Format(((IPEndPoint)(_clientSocket.RemoteEndPoint)).ToString());
        }
    }
}