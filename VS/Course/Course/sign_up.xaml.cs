using MyLib;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Course
{
    public partial class sign_up : Window
    {
        private readonly DataBase dataBase = SqlDataBase.Instance;

        public sign_up()
        {
            InitializeComponent();
        }

        private void button_Sign_up_Click(object sender, RoutedEventArgs e)
        {
            string lastName = textBox_lastName.Text;
            string firstName = textBox_firstName.Text;
            string surname = textBox_surname.Text;
            string passportNumber = textBox_passportNumber.Text;
            string phoneNumber = textBox_phoneNumber.Text;
            string driverLicense = textBox_driverLicense.Text;
            string password = textBox_password.Text;

            if (string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(passportNumber) || string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(driverLicense) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Будь ласка, заповніть всі поля.", "Помилка вводу", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (phoneNumber.Length > 50 || password.Length > 50)
            {
                MessageBox.Show("Довжина логіну або пароля перевищує 50 символів!", "Помилка вводу", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (CheckUserExists())
            {
                return;
            }

            DateTime currentDate = DateTime.Now;
            string registrationDate = currentDate.ToString("yyyy-MM-dd");

            string queryString = $"INSERT INTO Klient(last_Name, first_Name, surname, passport_Number, phone_Number, driver_License_Number, password_user, registration_Date) VALUES('{lastName}', '{firstName}', '{surname}', '{passportNumber}', '{phoneNumber}', '{driverLicense}', '{password}', '{registrationDate}')";

            SqlCommand command = new SqlCommand(queryString, dataBase.GetSqlConnection());
            dataBase.OpenConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Ви успішно зареєстровані!", "Успішно!", MessageBoxButton.OK, MessageBoxImage.Information);
                log_in logInWindow = new log_in();
                logInWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Не зареєстровано! Перевірте введення!", "Реєстрація не вдалась!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            dataBase.CloseConnection();
        }

        private bool CheckUserExists()
        {
            string loginUser = textBox_phoneNumber.Text;
            string passUser = textBox_password.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string queryString = "SELECT ID_Klient, phone_Number, password_user FROM Klient WHERE phone_Number = @loginUser AND password_user = @passUser";

            using (SqlCommand command = new SqlCommand(queryString, dataBase.GetSqlConnection()))
            {
                command.Parameters.AddWithValue("@loginUser", loginUser);
                command.Parameters.AddWithValue("@passUser", passUser);

                adapter.SelectCommand = command;
                adapter.Fill(table);
            }

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Користувач вже існує!", "Користувач існує!", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
            return false;
        }
    }
}
