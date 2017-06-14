using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Server
{
    class ClientManager
    {
        #region Singleton

        private static ClientManager _instance = null;
        private static volatile object threadSyncLock = new object();

        private ClientManager()
        {
            clientList = new List<ClientObject>();
        }

        public static ClientManager Instance
        {
            get
            {
                lock (threadSyncLock)
                {
                    if (_instance == null) _instance = new ClientManager();
                    return _instance;
                }
            }
        }

        #endregion Singleton

        private static List<ClientObject> clientList;

        public static void addNewClient(ClientObject clientObject)
        {
        }

    }
}
