using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Client
{
    public sealed class ServerConnection
    {
        // singleton pattern
        private static ServerConnection instance = null;

        private ServerConnection() { }

        public static ServerConnection Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ServerConnection();
                }
                return instance;
            }
        }
        // connection
        public string Host { set; get; }
        public ushort Port { set; get; }
        public Socket ConnectionSocket { set; get; }
        
        public bool Connect()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Host);
            foreach (IPAddress address in hostEntry.AddressList)
            {
                IPEndPoint ipEndPoint = new IPEndPoint(address, Port);
                Socket tempSocket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    tempSocket.Connect(ipEndPoint);
                    if (tempSocket.Connected)
                    {
                        ConnectionSocket = tempSocket;
                        return true;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return false;
        }
    }
}
