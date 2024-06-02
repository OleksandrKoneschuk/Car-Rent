using System.Data.SqlClient;

namespace MyLib
{
    public abstract class DataBase
    {
        protected SqlConnection sqlConnection;

        public abstract void OpenConnection();
        public abstract void CloseConnection();
        public SqlConnection GetSqlConnection()
        {
            return sqlConnection;
        }
    }
}
