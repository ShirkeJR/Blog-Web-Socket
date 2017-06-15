using Blog.Constants;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Blog.Server
{
    internal class TestServer
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
            localEndPoint = new IPEndPoint(ipAddress, Int16Constants.DefaultPort);
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
        private IPHostEntry ipHostInfo;
        private IPAddress ipAddress;
        private IPEndPoint localEndPoint;

        public void StartListening()
        {
            listenerSocket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, 0);
            Socket s;
            listenerSocket.Bind(localEndPoint);
            LoggingService.Instance.AddLog(string.Format("Socket bound to - " + localEndPoint.ToString()));
            while (true)
            {
                listenerSocket.Listen(0);
                s = listenerSocket.Accept();
                ClientData clientData = new ClientData(s);
                LoggingService.Instance.AddClient(clientData);
                _clients.Add(clientData);
            }
        }
    }
}