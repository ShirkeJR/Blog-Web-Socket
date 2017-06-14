﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blog.Server
{
    class TestServer
    {
        #region Singleton

        private static volatile TestServer _instance = null;
        private static volatile object threadSyncLock = new object();

  
        private TestServer()
        {
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clients = new List<ClientData>();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            localEndPoint = new IPEndPoint(ipAddress, 11000);
        }

        public static TestServer Instance
        {
            get
            {
                lock (threadSyncLock)
                {
                    if (_instance == null) _instance = new TestServer();
                    return _instance;
                }
            }
        }

        #endregion Singleton

        private Socket listenerSocket;
        private List<ClientData> _clients;
        public ListBox logBox { set; get; }
        public ListBox clientBox { set; get; }
        private IPHostEntry ipHostInfo;
        private IPAddress ipAddress;
        private IPEndPoint localEndPoint;

        public void StartListening()
        {
            Socket s;
            listenerSocket.Bind(localEndPoint);
            while (true)
            {
                listenerSocket.Listen(0);
                s = listenerSocket.Accept();
                clientBox.Invoke((MethodInvoker)delegate { clientBox.Items.Add((((IPEndPoint)(s.RemoteEndPoint)).Address.ToString()) + ": " + ((IPEndPoint)(s.RemoteEndPoint)).Port.ToString()); });
                _clients.Add(new ClientData(s, logBox, clientBox));
            }
        }
    }

}
