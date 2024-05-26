using MyLib;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Course
{
    public partial class ChooseCarWindow : Window
    {
        private readonly DataBase dataBase = SqlDataBase.Instance;
        private readonly IMessageService messageService;

        private DateTime _startDate;
        private DateTime _endDate;
        private int _idKlient;
        private int _idAvto;
        private decimal _cost;
        private int _percentDiscount;

        private decimal _cost1_3DayRental;
        private decimal _cost4_9DayRental;
        private decimal _cost10_25DayRental;
        private decimal _cost26DayRental;

        public ChooseCarWindow(DateTime startDate, DateTime endDate, int idAvto, int idKlient, IMessageService messageService)
        {
            InitializeComponent();
            _idKlient = idKlient;
            _idAvto = idAvto;
            _startDate = startDate;
            _endDate = endDate;
            this.messageService = messageService;

            Start_Date_DatePicker.SelectedDate = _startDate;
            end_Of_Rental_DatePicker.SelectedDate = _endDate;
            GetDiscount();
            CalculateCost();
        }

        private void CalculateCost()
        {
            try
            {
                dataBase.OpenConnection();
                string queryString = @"
                    SELECT cost_1_3_Day_Rental, cost_4_9_Day_Rental, cost_10_25_Day_Rental, cost_26_Day_Rental
                    FROM Avto
                    WHERE ID_Avto = @_idAvto";
                using (SqlCommand command = new SqlCommand(queryString, dataBase.GetSqlConnection()))
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
                dataBase.CloseConnection();
            }

            TimeSpan difference = _endDate - _startDate;
            int numberOfDays = difference.Days;

            decimal priceWithoutDiscount;
            if (numberOfDays >= 1 && numberOfDays <= 3)
            {
                priceWithoutDiscount = _cost1_3DayRental * numberOfDays;
            }
            else if (numberOfDays >= 4 && numberOfDays <= 9)
            {
                priceWithoutDiscount = _cost4_9DayRental * numberOfDays;
            }

            else if (numberOfDays >= 10 && numberOfDays <= 25)
            {
                priceWithoutDiscount = _cost10_25DayRental * numberOfDays;
            }
            else
            {
                priceWithoutDiscount = _cost26DayRental * numberOfDays;
            }

            _cost = priceWithoutDiscount - (priceWithoutDiscount * _percentDiscount / 100);
            cost_TextBox.Text = $"Вартість: ${_cost:0.00}";
        }

        private void GetDiscount()
        {
            try
            {
                dataBase.OpenConnection();
                string queryString = "SELECT percent_Discount FROM Discount WHERE ID_Klient = @_idKlient";
                using (SqlCommand command = new SqlCommand(queryString, dataBase.GetSqlConnection()))
                {
                    command.Parameters.AddWithValue("@_idKlient", _idKlient);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int count = 0;
                        while (reader.Read())
                        {
                            int currentDiscount = reader.GetInt32(0);
                            if (count == 0)
                            {
                                _percentDiscount = currentDiscount;
                            }
                            else
                            {
                                _percentDiscount += currentDiscount / 3;
                            }
                            count++;
                        }
                        if (count <= 0)
                        {
                            MessageBox.Show($"Для клієнта з ID {_idKlient} не знайдено знижок.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні знижок: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                dataBase.CloseConnection();
            }
        }

        private void button_Submit_Click(object sender, RoutedEventArgs e)
        {
            if (CheckRent())
            {
                return;
            }

            try
            {
                string startDate = _startDate.ToString("yyyy-MM-dd");
                string endDate = _endDate.ToString("yyyy-MM-dd");

                string queryString = @"
                    INSERT INTO Rent (rental_Start_Date, end_Of_Rental, actual_End_Of_Rental, rental_Price, ID_Klient, ID_Avto)
                    VALUES (@startDate, @endDate, NULL, @cost, @idKlient, @idAvto)";

                SqlCommand command = new SqlCommand(queryString, dataBase.GetSqlConnection());
                dataBase.OpenConnection();

                command.Parameters.AddWithValue("@startDate", startDate);
                command.Parameters.AddWithValue("@endDate", endDate);
                command.Parameters.AddWithValue("@cost", _cost);
                command.Parameters.AddWithValue("@idKlient", _idKlient);
                command.Parameters.AddWithValue("@idAvto", _idAvto);

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Ви успішно забронювали авто!", "Успішно!", MessageBoxButton.OK, MessageBoxImage.Information);

                    SendSmsNotification();
                }
                else
                {
                    MessageBox.Show("Не вдалося забронювати авто! Спробуйте ще раз!", "Бронювання не вдалось!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при бронюванні авто: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                dataBase.CloseConnection();
            }
        }

        private void SendSmsNotification()
        {
            string phoneNumber = "0971234567"; 
            string message = $"Ваше бронювання авто успішно завершено. Дати оренди: {_startDate.ToShortDateString()} - {_endDate.ToShortDateString()}.";
            messageService.SendMessage(phoneNumber, message);

            MessageBox.Show($"Sending SMS to {phoneNumber}: {message}", "SMS Notification", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CheckRent()
        {
            string startDate = _startDate.ToString("yyyy-MM-dd");
            string endDate = _endDate.ToString("yyyy-MM-dd");

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string queryString = @"
                SELECT 1
                FROM Rent
                WHERE ID_Avto = @idAvto AND 
                NOT (end_Of_Rental < @startDate OR rental_Start_Date > @endDate)";

            using (SqlCommand command = new SqlCommand(queryString, dataBase.GetSqlConnection()))
            {
                command.Parameters.AddWithValue("@startDate", startDate);
                command.Parameters.AddWithValue("@endDate", endDate);
                command.Parameters.AddWithValue("@idAvto", _idAvto);

                adapter.SelectCommand = command;
                adapter.Fill(table);
            }

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Машина зайнята на ці числа!", "Заброньовано!", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
            return false;
        }

        private void button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void HandleException(string errorMessage, Exception ex)
        {
            MessageBox.Show($"{errorMessage}: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
