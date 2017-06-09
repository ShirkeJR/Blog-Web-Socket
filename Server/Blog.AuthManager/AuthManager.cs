using Blog.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Blog.AuthManager
{
    public class AuthManager
    {
        #region Singleton

        private static AuthManager _instance = null;
        private static volatile object threadSyncLock = new object();

        private AuthManager()
        {
        }

        public static AuthManager Instance
        {
            get
            {
                lock (threadSyncLock)
                {
                    if (_instance == null) _instance = new AuthManager();
                    return _instance;
                }
            }
        }

        #endregion Singleton

        public async Task<bool> Exists(string login, string password)
        {
            string query = "SELECT COUNT(*) FROM [dbo].[Users] WHERE [Login] = @Login";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["@Login"] = login;
            using (SqlConnection conn = await SqlManager.Instance.CreateConnection())
            {
                var result = await SqlManager.Instance.ExecuteScalarAsync(conn, query, parameters);
                int value = Convert.ToInt32(result.ToString());
                return value > 0;
            }
        }

        public async Task<bool> Register(string login, string password)
        {
            if (await Exists(login, password))
                return false;

            string query = "INSERT INTO [dbo].[Users] ([Login], [Password], [IsLocked], [BlogName]) VALUES (@Login, @Password, @IsLocked, @BlogName)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["@Login"] = login;
            parameters["@Password"] = password.GetSHA256String();
            parameters["@IsLocked"] = false;
            parameters["@BlogName"] = string.Format("Blog {0}", login);
            using (SqlConnection conn = await SqlManager.Instance.CreateConnection())
            {
                var result = await SqlManager.Instance.ExecuteNonQueryAsync(conn, query, parameters);
                int value = Convert.ToInt32(result.ToString());
                return value == 1;
            }
        }

        public async Task<int> Login(string login, string password)
        {
            if (!(await Exists(login, password)))
                return -1;

            string query = "SELECT [Id] FROM [dbo].[Users] WHERE [Login] = @Login AND [Password] = @Password AND [IsLocked] = 0";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["@Login"] = login;
            parameters["@Password"] = password.GetSHA256String();
            using (SqlConnection conn = await SqlManager.Instance.CreateConnection())
            {
                var result = await SqlManager.Instance.ExecuteScalarAsync(conn, query, parameters);
                int value = Convert.ToInt32(result.ToString());
                return value;
            }
        }
    }
}