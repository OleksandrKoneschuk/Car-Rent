using MyLib;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace Course
{
    public partial class sign_up : Window
    {
        DataBase dataBase = new DataBase();
        public sign_up()
        {
            InitializeComponent();
        }

        private void button_Sign_up_Click(object sender, RoutedEventArgs e)
        {
            var lastName = textBox_lastName.Text;
            var firstName = textBox_firstName.Text;
            var surname = textBox_surname.Text;
            var passportNumber = textBox_passportNumber.Text;
            var phoneNumber = textBox_phoneNumber.Text;
            var driverLicense = textBox_driverLicense.Text;
            var password = textBox_password.Text;


            if (string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(surname) ||
                string.IsNullOrWhiteSpace(passportNumber) ||
                string.IsNullOrWhiteSpace(phoneNumber) ||
                string.IsNullOrWhiteSpace(driverLicense) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Будь ласка, заповніть всі поля.", "Помилка вводу", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (phoneNumber.Length > 50 || password.Length > 50)
            {
                MessageBox.Show("Довжина логіну або пароля перевищує 50 символів!", "Помилка вводу", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (checkUser())
            {
                return;
            }

            DateTime currentDate = DateTime.Now;
            var registration_Date = currentDate.ToString("yyyy-MM-dd");

            string querystring = $"insert into Klient(last_Name, first_Name, surname, passport_Number, phone_Number, driver_License_Number, password_user, registration_Date) " +
                $"values('{lastName}', '{firstName}', '{surname}', '{passportNumber}', '{phoneNumber}', '{driverLicense}', '{password}', '{registration_Date}')";

            SqlCommand command = new SqlCommand(querystring, dataBase.getSqlConnection());
            dataBase.openConnection();

            if(command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Ви успішно зареєстровані!", "Успішно!", MessageBoxButton.OK, MessageBoxImage.Information);
                log_in log_in = new log_in();
                log_in.Show();
                this.Close();
            }
            else
                MessageBox.Show("Не зареєстровано! Перевірте введення!", "Реєстрація не вдалась!", MessageBoxButton.OK, MessageBoxImage.Error);

            dataBase.closeConnection();
        }

        private Boolean checkUser()
        {
            var loginUser = textBox_phoneNumber.Text;
            var passUser = textBox_password.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select ID_Klient, phone_Number, password_user from Klient where phone_Number = '{loginUser}' and password_user = '{passUser}'";

            SqlCommand command = new SqlCommand(querystring, dataBase.getSqlConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count > 0) 
            {
                MessageBox.Show("Користувач вже існує!", "Користувач існує!", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
            else
            { 
                return false; 
            }
        }
    }
}
