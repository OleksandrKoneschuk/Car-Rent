using MyLib;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;


namespace Course
{
    public partial class log_in : Window
    {
        DataBase dataBase = new DataBase();

        public log_in()
        {
            InitializeComponent();
        }

        private void button_Log_in_Click(object sender, RoutedEventArgs e)
        {
            var loginUser = textBox_login.Text;
            var passUser = textBox_password.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            dataBase.openConnection();

            string querystring = $"select ID_Klient, phone_Number, password_user from Klient where phone_Number = '{loginUser}' and password_user = '{passUser}'";

            SqlCommand command = new SqlCommand(querystring, dataBase.getSqlConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            object result = command.ExecuteScalar();

            if (table.Rows.Count == 1 && loginUser != "0979742402")
            {
                int idKlient = (int)result;
                MessageBox.Show("Ви успішно увійшли!", "Успішно!", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow MainWindow = new MainWindow(idKlient);
                MainWindow.Show();
                dataBase.closeConnection();
                this.Close();
            }
            else if(loginUser == "0979742402")
            {
                MessageBox.Show("Ви успішно увійшли в акаунт адміністратора!", "Успішно!", MessageBoxButton.OK, MessageBoxImage.Information);
                AdminMenu AdminMenu = new AdminMenu();
                AdminMenu.Show();
                dataBase.closeConnection();
                this.Close();
            }
            else
                MessageBox.Show("Такого акаунту не існує або \nнеправильний пароль!", "Акаунту не існує!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void SignUpLabel_Click(object sender, MouseButtonEventArgs e)
        {
            sign_up signUpWindow = new sign_up();
            signUpWindow.Show();
            this.Close(); 
        }
    }
}