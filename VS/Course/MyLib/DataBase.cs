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

    public class SqlDataBase : DataBase
    {
        private static SqlDataBase instance;

        private SqlDataBase()
        {
            sqlConnection = new SqlConnection(@"Data Source=DESKTOP-FUAH6BA;Initial Catalog=Car_Rent;Integrated Security=True");
        }

        public static SqlDataBase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SqlDataBase();
                }
                return instance;
            }
        }

        public override void OpenConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public override void CloseConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
    }

    // Приклад іншого типу бази даних, наприклад, для MySQL
    public class MySqlDataBase : DataBase
    {
        private static MySqlDataBase instance;

        private MySqlDataBase()
        {
            // Ініціалізація з'єднання з MySQL базою даних
            // sqlConnection = new MySqlConnection("your_connection_string_here");
        }

        public static MySqlDataBase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MySqlDataBase();
                }
                return instance;
            }
        }

        public override void OpenConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public override void CloseConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
    }
}
