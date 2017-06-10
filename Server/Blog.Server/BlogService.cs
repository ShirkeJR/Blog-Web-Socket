using Blog.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Server
{
    class BlogService
    {
        #region Singleton

        private static volatile BlogService _instance = null;
        private static volatile object threadSyncLock = new object();

        private BlogService()
        {
        }

        public static BlogService Instance
        {
            get
            {
                lock (threadSyncLock)
                {
                    if (_instance == null) _instance = new BlogService();
                    return _instance;
                }
            }
        }

        #endregion Singleton

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

    }
}
