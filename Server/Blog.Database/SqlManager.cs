using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Blog.Database
{
    public class SqlManager
    {
        #region Singleton

        private static SqlManager _instance = null;
        private static object threadSyncLock = new object();

        private SqlManager()
        {
        }

        public static SqlManager Instance
        {
            get
            {
                lock (threadSyncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new SqlManager();
                    }

                    return _instance;
                }
            }
        }

        #endregion Singleton

        public string ConnectionString { get { return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString; } }

        public async Task<SqlConnection> CreateConnection()
        {
            var sqlConn = new SqlConnection(ConnectionString);
            await sqlConn.OpenAsync();
            return sqlConn;
        }

        public SqlDataReader ExecuteReader(SqlConnection sqlConnection, string query, Dictionary<string, object> parameters = null)
        {
            if (sqlConnection == null ||
                sqlConnection.State != ConnectionState.Open ||
                string.IsNullOrEmpty(query) ||
                string.IsNullOrWhiteSpace(query) ||
                query == string.Empty ||
                query.Length == 0) return null;
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    if (parameters != null && parameters.Count > 0)
                        foreach (var entry in parameters)
                            sqlCommand.Parameters.AddWithValue(entry.Key, entry.Value);

                    return sqlCommand.ExecuteReader();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public async Task<DbDataReader> ExecuteReaderAsync(SqlConnection sqlConnection, string query, Dictionary<string, object> parameters = null)
        {
            if (sqlConnection == null ||
                sqlConnection.State != ConnectionState.Open ||
                string.IsNullOrEmpty(query) ||
                string.IsNullOrWhiteSpace(query) ||
                query == string.Empty ||
                query.Length == 0) return null;
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    if (parameters != null && parameters.Count > 0)
                        foreach (var entry in parameters)
                            sqlCommand.Parameters.AddWithValue(entry.Key, entry.Value);

                    return await sqlCommand.ExecuteReaderAsync();
                }
            }
            catch (SqlException)
            {
                return null;
                throw;
            }
        }

        public int ExecuteNonQuery(SqlConnection sqlConnection, string query, Dictionary<string, object> parameters = null)
        {
            if (sqlConnection == null ||
                sqlConnection.State != ConnectionState.Open ||
                string.IsNullOrEmpty(query) ||
                string.IsNullOrWhiteSpace(query) ||
                query == string.Empty ||
                query.Length == 0) return -1;
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    if (parameters != null && parameters.Count > 0)
                        foreach (var entry in parameters)
                            sqlCommand.Parameters.AddWithValue(entry.Key, entry.Value);

                    return sqlCommand.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                return -1;
                throw;
            }
        }

        public async Task<int> ExecuteNonQueryAsync(SqlConnection sqlConnection, string query, Dictionary<string, object> parameters = null)
        {
            if (sqlConnection == null ||
                sqlConnection.State != ConnectionState.Open ||
                string.IsNullOrEmpty(query) ||
                string.IsNullOrWhiteSpace(query) ||
                query == string.Empty ||
                query.Length == 0) return -1;
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    if (parameters != null && parameters.Count > 0)
                        foreach (var entry in parameters)
                            sqlCommand.Parameters.AddWithValue(entry.Key, entry.Value);

                    return await sqlCommand.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException)
            {
                return -1;
                throw;
            }
        }

        public object ExecuteScalar(SqlConnection sqlConnection, string query, Dictionary<string, object> parameters = null)
        {
            if (sqlConnection == null ||
                sqlConnection.State != ConnectionState.Open ||
                string.IsNullOrEmpty(query) ||
                string.IsNullOrWhiteSpace(query) ||
                query == string.Empty ||
                query.Length == 0) return null;
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    if (parameters != null && parameters.Count > 0)
                        foreach (var entry in parameters)
                            sqlCommand.Parameters.AddWithValue(entry.Key, entry.Value);

                    return sqlCommand.ExecuteScalar();
                }
            }
            catch (SqlException)
            {
                return null;
                throw;
            }
        }

        public async Task<object> ExecuteScalarAsync(SqlConnection sqlConnection, string query, Dictionary<string, object> parameters = null)
        {
            if (sqlConnection == null ||
                sqlConnection.State != ConnectionState.Open ||
                string.IsNullOrEmpty(query) ||
                string.IsNullOrWhiteSpace(query) ||
                query == string.Empty ||
                query.Length == 0) return null;
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    if (parameters != null && parameters.Count > 0)
                        foreach (var entry in parameters)
                            sqlCommand.Parameters.AddWithValue(entry.Key, entry.Value);

                    return await sqlCommand.ExecuteScalarAsync();
                }
            }
            catch (SqlException)
            {
                return null;
                throw;
            }
        }
    }
}