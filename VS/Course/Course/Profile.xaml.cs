using MyLib;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace Course
{

    public partial class Profile : Window
    {
        private int _idKlient;
        DataBase dataBase = new DataBase();

        public Profile(int idKlient)
        {
            InitializeComponent();

           _idKlient = idKlient;
            GetProfile();
        }

        private void GetProfile()
        {
            try
            {
                dataBase.openConnection();

                string queryString = "SELECT * FROM Klient WHERE ID_Klient = @_idKlient";

                using (SqlCommand command = new SqlCommand(queryString, dataBase.getSqlConnection()))
                {
                    command.Parameters.AddWithValue("@_idKlient", _idKlient);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string last_Name = reader["last_Name"].ToString();
                            string first_Name = reader["first_Name"].ToString();
                            string surname = reader["surname"].ToString();

                            full_nameTextBox.Text = $"{last_Name} {first_Name} {surname}";

                            loginTextBox.Text = reader["phone_Number"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні даних про клієнта: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                dataBase.closeConnection();
            }
        }


        private void MainWindow_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow(_idKlient);
            MainWindow.Show();
            this.Close();
        }

        private void button_exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
