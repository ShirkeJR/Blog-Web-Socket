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

            byte[] bytes;
            int bytesRead = 0;

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
                    bytes = new byte[clientSocket.SendBufferSize];
                    bytesRead = clientSocket.Receive(bytes);

                    if (bytesRead > 0)
                    {
                        content = Encoding.ASCII.GetString(bytes, 0, bytesRead);
#if IMPROVED_PACKET_ENCRYPTION
                        var content2 = CryptoService.Decrypt<AesManaged>(content.Substring(0, content.Length - StringConstants.PacketEnding.Length), StringConstants.SymmetricKey, StringConstants.SymmetricSalt);
                        content = string.Format("{0}{1}", content2, content.Substring(content.Length - StringConstants.PacketEnding.Length));
#endif
                        if (content.IndexOf(StringConstants.PacketEnding) > -1)
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
                            byte[] byteData = Encoding.ASCII.GetBytes(content);
                            clientSocket.Send(byteData);
                        }
                        else
                        {
                            content = await PacketAnalyzeService.Instance.getPacketResponse("daoijhdoiajsd", this);
                            LoggingService.Instance.AddLog(content);
                            byte[] byteData = Encoding.ASCII.GetBytes(content);
                            clientSocket.Send(byteData);
                        }
                    }
                }
                catch (SocketException ex)
                {
                    LoggingService.Instance.WriteException(ex);
                }
            }
        }
        public override string ToString()
        {
            return string.Format(((IPEndPoint)(_clientSocket.RemoteEndPoint)).ToString());
        }
    }
}