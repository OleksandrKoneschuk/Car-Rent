using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib.DB
{
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
}
