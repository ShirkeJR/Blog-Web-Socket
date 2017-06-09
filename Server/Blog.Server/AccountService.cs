using Blog.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

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
            return await AuthManager.AuthManager.Instance.Exists(login, password);
        }

        public async Task<bool> Register(string login, string password)
        {
            return await AuthManager.AuthManager.Instance.Register(login, password);
        }
        
        public async Task<int> Login(string login, string password)
        {
            return await AuthManager.AuthManager.Instance.Login(login, password);
        }

        public async Task<bool> ChangeBlogName(int userId, int blogId, string newName)
        {
            if (userId != blogId)
                return false;

            if (newName.Length == 0 || string.IsNullOrEmpty(newName) || string.IsNullOrWhiteSpace(newName))
                return false;

            string query = "UPDATE [dbo].[Users] SET [BlogName] = @BlogName WHERE [Id] = @Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["@BlogName"] = newName;
            parameters["@Id"] = blogId;
            using (SqlConnection conn = await SqlManager.Instance.CreateConnection())
            {
                var result = await SqlManager.Instance.ExecuteNonQueryAsync(conn, query, parameters);
                int value = Convert.ToInt32(result.ToString());
                return value == 1;
            }
        }

        public bool Disconnect(int userId)
        {
            // TODO: delete logged in user from clients list with connection close
            return true;
        }
    }
}