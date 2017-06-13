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

        public string Register(string login, string password)
        {
            Frame request = new Frame("REGISTER", new string[] { login, password });
            Frame response;

            ConnectionService.Instance.SendFrame(request);
            response = ConnectionService.Instance.ReceiveFrame();

            switch (response.Command)
            {
                case "QUE?":
                    {
                        return String.Format("QUE?");
                    }
                case "IDENTIFY_PLS":
                    {
                        return String.Format("IDENTIFY_PLS");
                    }
                case "REGISTER":
                    {
                        return response.Parametres[0];
                    }
                default:
                    {
                        return String.Format("ERROR");
                    }
            }
        }
        public string Login(string login, string password)
        {
            Frame request = new Frame("LOGIN", new string[] { login, password });
            Frame response;

            ConnectionService.Instance.SendFrame(request);
            response = ConnectionService.Instance.ReceiveFrame();

            switch (response.Command)
            {
                case "QUE?":
                    {
                        return String.Format("QUE?");
                    }
                case "IDENTIFY_PLS":
                    {
                        return String.Format("IDENTIFY_PLS");
                    }
                case "LOGIN":
                    {
                        if (response.Parametres[0].Equals("OK"))
                        {
                            User = new User(login, Convert.ToUInt32(response.Parametres[1]));
                            return response.Parametres[1];
                        }
                        else
                            return response.Parametres[0];
                    }
                default:
                    {
                        return String.Format("ERROR");
                    }
            }
        }
        public string Logout()
        {
            Frame request = new Frame("THX_BYE", null);
            Frame response;

            ConnectionService.Instance.SendFrame(request);
            response = ConnectionService.Instance.ReceiveFrame();

            switch (response.Command)
            {
                case "QUE?":
                    {
                        return String.Format("QUE?");
                    }
                case "IDENTIFY_PLS":
                    {
                        return String.Format("IDENTIFY_PLS");
                    }
                case "THX_BYE":
                    {
                        User = null;
                        return String.Format("THX_BYE");
                    }
                default:
                    {
                        return String.Format("ERROR");
                    }
            }
        }
    }
}
