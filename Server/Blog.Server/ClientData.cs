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
        private Socket clientSocket = null;
        private Thread clientThread = null;
        private int id = -1;
        public int Id { get { return id; } set { id = value; } }
        public Socket ClientSocket { get { return clientSocket; } set { clientSocket = value; } }
        public Thread ClientThread { get { return clientThread; } set { clientThread = value; } }

        public ClientData(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            clientThread = new Thread(dataListening);
            clientThread.Start(clientSocket);
        }

        public async void dataListening(object cSocket)
        {
            String content = String.Empty;
            Socket clientSocket = (Socket)cSocket;
            LoggingService.Instance.AddLog("*Client: " +
(((IPEndPoint)(clientSocket.RemoteEndPoint)).Address.ToString()) + ": " +
(((IPEndPoint)(clientSocket.RemoteEndPoint)).Port.ToString()) + " open");

            byte[] bytes;
            int bytesRead = 0;

            while (true)
            {
                try
                {
                    if (!clientSocket.Connected)
                    {
                        LoggingService.Instance.AddLog("*Client: " +
                            (((IPEndPoint)(clientSocket.RemoteEndPoint)).Address.ToString()) + ": " +
                            (((IPEndPoint)(clientSocket.RemoteEndPoint)).Port.ToString()) + " closed");
                        LoggingService.Instance.RemoveClient(this);
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Close();
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
                            LoggingService.Instance.AddLog("-->\t" + content);
                            if (content.StartsWith("4\tEOT\t"))
                            {
                                LoggingService.Instance.AddLog("*Client: " +
                                    (((IPEndPoint)(clientSocket.RemoteEndPoint)).Address.ToString()) + ": " +
                                    (((IPEndPoint)(clientSocket.RemoteEndPoint)).Port.ToString()) + " closed");

                                LoggingService.Instance.RemoveClient(this);
                                clientSocket.Shutdown(SocketShutdown.Both);
                                clientSocket.Close();
                                return;
                            }

                            content = await PacketAnalyzeService.Instance.getPacketResponse(content, this);
#if IMPROVED_PACKET_ENCRYPTION
                            content = string.Format("{0}{1}", CryptoService.Encrypt<AesManaged>(content.Substring(0, content.Length - StringConstants.PacketEnding.Length), StringConstants.SymmetricKey, StringConstants.SymmetricSalt), StringConstants.PacketEnding);
#endif
                            LoggingService.Instance.AddLog("<--\t" + content);
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
                catch (SocketException)
                {
                }
            }
        }
    }
}