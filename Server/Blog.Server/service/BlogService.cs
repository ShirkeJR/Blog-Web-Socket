using Blog.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Blog.Server
{
    internal class BlogService
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
                if (!result.HasRows)
                    return "";
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
            string query = "SELECT [Id], [Title], [Content], [Date] FROM [dbo].[Posts] WHERE [Id] = @Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["@Id"] = id;
            using (SqlConnection conn = await SqlManager.Instance.CreateConnection())
            {
                string entry = "";
                var result = await SqlManager.Instance.ExecuteReaderAsync(conn, query, parameters);
                if (!result.HasRows)
                    return "";
                while (result.Read())
                {
                    entry = Convert.ToString(result["Id"]) + "\t" + Convert.ToString(result["Title"]) + "\t" + Convert.ToString(result["Date"]) + "\t" + Convert.ToString(result["Content"]);
                }
                return entry;
            }
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

        public async Task<bool> AddEntry(int id, string title, string content)
        {
            DateTime date = DateTime.Now;
            string query = "INSERT INTO [dbo].[Posts] ([UserId], [Title], [Content], [Date]) VALUES (@Id, @Title, @Content, @Date)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["@Id"] = id;
            parameters["@Title"] = title;
            parameters["@Content"] = content;
            parameters["@Date"] = date;
            using (SqlConnection conn = await SqlManager.Instance.CreateConnection())
            {
                var result = await SqlManager.Instance.ExecuteNonQueryAsync(conn, query, parameters);
                int value = Convert.ToInt32(result.ToString());
                return value == 1;
            }
        }

        public async Task<int> GetEntryId(int id, string title)
        {
            string query = "SELECT [Id] FROM [dbo].[Posts] WHERE [UserId] = @Id AND [Title] = @Title";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["@Id"] = id;
            parameters["@Title"] = title;
            using (SqlConnection conn = await SqlManager.Instance.CreateConnection())
            {
                var result = await SqlManager.Instance.ExecuteReaderAsync(conn, query, parameters);
                result.Read();
                if (!result.HasRows)
                    return 0;
                else
                    return Convert.ToInt32(result["Id"]);
            }
        }

        public async Task<bool> DeleteEntry(int id, int clientId)
        {
            string query = "DELETE FROM [dbo].[Posts] WHERE [UserId] = @ClientID AND [Id] = @Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["@Id"] = id;
            parameters["@ClientId"] = clientId;
            using (SqlConnection conn = await SqlManager.Instance.CreateConnection())
            {
                var result = await SqlManager.Instance.ExecuteNonQueryAsync(conn, query, parameters);
                int value = Convert.ToInt32(result.ToString());
                return value == 1;
            }
        }


    }
}