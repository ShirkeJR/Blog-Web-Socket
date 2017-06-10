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


        public async Task<string> DisplayBlogs()
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
                var paramsArray = blogList.ToArray();
                var paramsList = string.Join("\t", paramsArray);
                return paramsList;
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

        public async Task<string> DisplayBlogPosts(int id)
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
                var paramsArray = blogPostsList.ToArray();
                var paramsList = string.Join("\t", paramsArray);
                return paramsList;
            }
        }
        public async Task<string> DisplayEntry(int id)
        {
            string query = "SELECT [Id], [Title], [Date]. [Content] FROM [dbo].[Posts] WHERE [Id] = @Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["@Id"] = id;
            using (SqlConnection conn = await SqlManager.Instance.CreateConnection())
            {
                string entry = "";
                var result = await SqlManager.Instance.ExecuteReaderAsync(conn, query, parameters);
                while (result.Read())
                {
                    entry = Convert.ToString(result["Id"]) + "\t" + Convert.ToString(result["Title"]) + "\t" + Convert.ToString(result["Date"]) + "\t" + Convert.ToString(result["Content"]);
                }
                return entry;
            }
        }


    }
}
