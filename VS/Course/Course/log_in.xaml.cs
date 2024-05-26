using MyLib;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Course
{
    public partial class log_in : Window
    {
        private readonly DataBase dataBase = SqlDataBase.Instance;

        public log_in()
        {
            InitializeComponent();
        }

        private void button_Log_in_Click(object sender, RoutedEventArgs e)
        {
            string loginUser = textBox_login.Text;
            string passUser = textBox_password.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            dataBase.OpenConnection();

            string queryString = $"SELECT ID_Klient, phone_Number, password_user FROM Klient WHERE phone_Number = '{loginUser}' AND password_user = '{passUser}'";
            SqlCommand command = new SqlCommand(queryString, dataBase.GetSqlConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            object result = command.ExecuteScalar();

            if (table.Rows.Count == 1 && loginUser != "0979742402")
            {
                int idKlient = (int)result;
                MessageBox.Show("Ви успішно увійшли!", "Успішно!", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mainWindow = new MainWindow(idKlient);
                mainWindow.Show();
                dataBase.CloseConnection();
                this.Close();
            }
            else if (table.Rows.Count == 1 && loginUser == "0979742402")
            {
                MessageBox.Show("Ви успішно увійшли в акаунт адміністратора!", "Успішно!", MessageBoxButton.OK, MessageBoxImage.Information);
                AdminMenu adminMenu = new AdminMenu();
                adminMenu.Show();
                dataBase.CloseConnection();
                this.Close();
            }
            else
            {
                MessageBox.Show("Такого акаунту не існує або \nне правильний пароль!", "Акаунту не існує!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SignUpLabel_Click(object sender, MouseButtonEventArgs e)
        {
            sign_up signUpWindow = new sign_up();
            signUpWindow.Show();
            this.Close();
        }
    }
}