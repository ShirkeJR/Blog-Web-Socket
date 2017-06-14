using System.Net;
using System.Windows.Forms;

namespace Blog.Server
{
    internal class LoggingService
    {
        #region Singleton

        private static LoggingService _instance = null;
        private static volatile object threadSyncLock = new object();

        private LoggingService()
        {
        }

        public static LoggingService Instance
        {
            get
            {
                lock (threadSyncLock)
                {
                    if (_instance == null)
                        _instance = new LoggingService();
                    return _instance;
                }
            }
        }

        #endregion Singleton

        private ListBox _logBox = null;
        private ListBox _clientBox = null;

        public void Initialize(ListBox logBox, ListBox clientBox)
        {
            _logBox = logBox;
            _clientBox = clientBox;
        }

        public void AddLog(string message)
        {
            _logBox.Invoke((MethodInvoker)delegate
            {
                _logBox.Items.Add(message);
            });
        }

        public void AddClient(ClientData clientData)
        {
            _clientBox.Invoke((MethodInvoker)delegate
            {
                _clientBox.Items.Add((((IPEndPoint)(clientData.ClientSocket.RemoteEndPoint)).Address.ToString()) + ": " + ((IPEndPoint)(clientData.ClientSocket.RemoteEndPoint)).Port.ToString());
            });
        }

        public void RemoveClient(ClientData clientData)
        {
            _clientBox.Invoke((MethodInvoker)delegate
            {
                _clientBox.Items.Remove((((IPEndPoint)(clientData.ClientSocket.RemoteEndPoint)).Address.ToString()) + ": " + ((IPEndPoint)(clientData.ClientSocket.RemoteEndPoint)).Port.ToString());
            });
        }
    }
}