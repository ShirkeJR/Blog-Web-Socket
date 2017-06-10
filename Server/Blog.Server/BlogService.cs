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


        public async Task<List<string>> DisplayBlogs()
        {
            string query = "SELECT [Id], [BlogName] FROM [dbo].[Users]";
            using (SqlConnection conn = await SqlManager.Instance.CreateConnection())
            {
                var result = await SqlManager.Instance.ExecuteReaderAsync(conn, query);
                List<string> blogList = new List<string>();
                while (result.Read())
                {
                    blogList.Add(Convert.ToString(result["Id"]) + "|" + Convert.ToString(result["BlogName"]));
                }
                return blogList;
            }
        }

        public async Task<bool> BlogExists(int id)
        {
            string query = "SELECT COUNT(*) FROM [dbo].[Posts] WHERE [UserId] = @Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["@Id"] = id;
            using (SqlConnection conn = await SqlManager.Instance.CreateConnection())
            {
                var result = await SqlManager.Instance.ExecuteScalarAsync(conn, query, parameters);
                int value = Convert.ToInt32(result.ToString());
                return value > 0;
            }
        }

        public async Task<List<string>> DisplayBlogPosts(int id)
        {

            string query = "SELECT [Id], [Title] FROM [dbo].[Posts] WHERE [UserId] = @Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["@Id"] = id;
            using (SqlConnection conn = await SqlManager.Instance.CreateConnection())
            {
                var result = await SqlManager.Instance.ExecuteReaderAsync(conn, query, parameters);
                List<string> blogPostsList = new List<string>();
                while (result.Read())
                {
                    blogPostsList.Add(Convert.ToString(result["Id"]) + "|" + Convert.ToString(result["Title"]));
                }
                return blogPostsList;
            }
        }



    }
}
