using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
