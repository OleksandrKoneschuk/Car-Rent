using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib.DB
{
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
