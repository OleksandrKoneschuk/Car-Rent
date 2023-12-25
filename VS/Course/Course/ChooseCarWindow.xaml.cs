using MyLib;
using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;

namespace Course
{
    public partial class ChooseCarWindow : Window
    {
        DataBase dataBase = new DataBase();

        private DateTime _startDate;
        private DateTime _endDate;

        private int _idKlient;
        private int _idAvto;

        private decimal _cost1_3DayRental;
        private decimal _cost4_9DayRental;
        private decimal _cost10_25DayRental;
        private decimal _cost26DayRental;
        public ChooseCarWindow(DateTime startDate, DateTime endDate, int ID_Avto, int idKlient)
        {
            InitializeComponent();

            _idKlient = idKlient;
            _idAvto = ID_Avto;

            _startDate = startDate;
            _endDate = endDate;

            Start_Date_DatePicker.SelectedDate = _startDate;
            end_Of_Rental_DatePicker.SelectedDate = _endDate;

            СostСalculation();
        }

        private void СostСalculation()
        {
            try
            {
                dataBase.openConnection();

                string queryString = @"
                SELECT cost_1_3_Day_Rental, cost_4_9_Day_Rental, cost_10_25_Day_Rental, cost_26_Day_Rental
                FROM Avto
                WHERE ID_Avto = @_idAvto
                ";

                using (SqlCommand command = new SqlCommand(queryString, dataBase.getSqlConnection()))
                {
                    command.Parameters.AddWithValue("@_idAvto", _idAvto);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            _cost1_3DayRental = (decimal)reader["cost_1_3_Day_Rental"];
                            _cost4_9DayRental = (decimal)reader["cost_4_9_Day_Rental"];
                            _cost10_25DayRental = (decimal)reader["cost_10_25_Day_Rental"];
                            _cost26DayRental = (decimal)reader["cost_26_Day_Rental"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні даних про ціни авто: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                dataBase.closeConnection();
            }


            TimeSpan difference = _endDate - _startDate;
            int numberOfDays = difference.Days;

            decimal cost;
            if (numberOfDays >= 1 && numberOfDays <= 3)
            {
                cost = _cost1_3DayRental * numberOfDays;
            }
            else if (numberOfDays >= 4 && numberOfDays <= 9)
            {
                cost = _cost4_9DayRental * numberOfDays;
            }
            else if (numberOfDays >= 10 && numberOfDays <= 25)
            {
                cost = _cost10_25DayRental * numberOfDays;
            }
            else
            {
                cost = _cost26DayRental * numberOfDays;
            }

            cost_TextBox.Text = "Вартість: $" + cost.ToString();
        }

        private void button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void button_Submit_Click(object sender, RoutedEventArgs e)
        {
            var startDate = _startDate.ToString("yyyy-MM-dd");
            var endDate = _endDate.ToString("yyyy-MM-dd");

            string queryString = $"INSERT INTO Rent (rental_Start_Date, end_Of_Rental, actual_End_Of_Rental, ID_Klient, ID_Avto)" +
                $"VALUES ('{startDate}', '{endDate}', ' ', '{_idKlient}', '{_idAvto}')";

            SqlCommand command = new SqlCommand(queryString, dataBase.getSqlConnection());
            dataBase.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Ви успішно забронювали авто!", "Успішно!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Не вдалося заброньвати авто! Спробуйте ще раз!", "Бронювання не вдалось!", MessageBoxButton.OK, MessageBoxImage.Error);

            dataBase.closeConnection();
        }
    }
}
