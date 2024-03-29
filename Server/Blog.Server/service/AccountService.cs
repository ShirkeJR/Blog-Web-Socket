﻿using System.Threading.Tasks;

namespace Blog.Server
{
    public class AccountService
    {
        #region Singleton

        private static volatile AccountService _instance = null;
        private static volatile object threadSyncLock = new object();

        private AccountService()
        {
        }

        public static AccountService Instance
        {
            get
            {
                lock (threadSyncLock)
                {
                    if (_instance == null) _instance = new AccountService();
                    return _instance;
                }
            }
        }

        #endregion Singleton

        public async Task<bool> Exists(string login, string password)
        {
            return await AuthManager.Instance.Exists(login, password);
        }

        public async Task<bool> Register(string login, string password)
        {
            return await AuthManager.Instance.Register(login, password);
        }

        public async Task<int> Login(string login, string password)
        {
            return await AuthManager.Instance.Login(login, password);
        }

        public async Task<bool> IsLocked(string login)
        {
            return await AuthManager.Instance.IsLocked(login);
        }
    }
}