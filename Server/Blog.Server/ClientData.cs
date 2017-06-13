using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blog.Server
{
    class ClientData
    {
        private Socket clientSocket;
        private Thread clientThread;
        private ListBox logBox;
        private int id;
        public int Id { get { return id; } set { id = value; } }



        public ClientData(Socket clientSocket, ListBox lg)
        {
            this.clientSocket = clientSocket;
            this.logBox = lg;
            id = -1;
            clientThread = new Thread(dataListening);
            clientThread.Start(clientSocket);
     
        }
        public async void dataListening(object cSocket)
        {
            String content = String.Empty;
            Socket clientSocket = (Socket)cSocket;

            byte[] bytes;
            int bytesRead = 0;

            while(true)
            {
                try
                {
                    bytes = new byte[clientSocket.SendBufferSize];
                    bytesRead = clientSocket.Receive(bytes); 

                    if (bytesRead > 0)
                    {
                        content = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                        if (content.IndexOf("/rn/rn/rn$$") > -1)
                        {  
                            logBox.Invoke((MethodInvoker)delegate { logBox.Items.Add(content); });
                            content = await PacketAnalyzeService.Instance.getPacketResponse(content, this);
                            logBox.Invoke((MethodInvoker)delegate { logBox.Items.Add(content); });
                            byte[] byteData = Encoding.ASCII.GetBytes(content);
                            clientSocket.Send(byteData);
                        }
                        else
                        {
                            content = await PacketAnalyzeService.Instance.getPacketResponse("daoijhdoiajsd", this);
                            logBox.Invoke((MethodInvoker)delegate { logBox.Items.Add(content); });
                            byte[] byteData = Encoding.ASCII.GetBytes(content);
                            clientSocket.Send(byteData);
                        }
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("Client Disconnected.");
                }
            }
        }
    }
}
