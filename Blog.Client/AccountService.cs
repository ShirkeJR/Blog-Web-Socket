using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Client
{
    public sealed class AccountService
    {
        #region Singleton
        private static volatile AccountService instance = null;
        private static volatile object threadSyncLock = new object();

        private AccountService() { }
        public static AccountService Instance
        {
            get
            {
                lock (threadSyncLock)
                {
                    if (instance == null) instance = new AccountService();
                    return instance;
                }
            }
        }
        #endregion Singleton
        public User User { set; get; }
        public bool Logged { set; get; } = false;

        public bool Register(string login, string password)
        {
            if (!ConnectionService.Instance.Connected()) return false;

            Frame request = new Frame("REGISTER", new string[] { login, password });
            Frame response = null;

            ConnectionService.Instance.SendFrame(request);
            response = ConnectionService.Instance.ReceiveFrame();

            return !response.CheckError();
        }
        public bool Login(string login, string password)
        {
            if (!ConnectionService.Instance.Connected()) return false;

            Frame request = new Frame("LOGIN", new string[] { login, password });
            Frame response = null;

            ConnectionService.Instance.SendFrame(request);
            response = ConnectionService.Instance.ReceiveFrame();

            if (!response.CheckError())
            {
                User = new User(login, Convert.ToUInt32(response.Parametres[1]));
                Logged = true;
                return true;
            }
            else return false;     
        }
        public bool Logout()
        {
            if (!ConnectionService.Instance.Connected()) return false;

            Frame request = new Frame("THX_BYE", null);
            Frame response;

            ConnectionService.Instance.SendFrame(request);
            response = ConnectionService.Instance.ReceiveFrame();

            if (!response.CheckError())
            {
                User = null;
                Logged = false;
                return true;
            }
            else return false;
        }
    }
}
