using MyLib;
using MyLib.DB;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows;

namespace Course
{
    public partial class Profile : Window
    {
        private readonly DataBase dataBase = SqlDataBase.Instance;
        private int _idKlient;

        public Profile(int idKlient)
        {
            InitializeComponent();
            _idKlient = idKlient;
            LoadProfile();
            LoadBookingHistory();
            LoadFinesHistory();
            LoadDiscountHistory();
        }

        private void LoadProfile()
        {
            try
            {
                dataBase.OpenConnection();

                string queryString = "SELECT * FROM Klient WHERE ID_Klient = @_idKlient";
                using (SqlCommand command = new SqlCommand(queryString, dataBase.GetSqlConnection()))
                {
                    command.Parameters.AddWithValue("@_idKlient", _idKlient);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            full_nameTextBox.Text = $"{reader["last_Name"]} {reader["first_Name"]} {reader["surname"]}";
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
                dataBase.CloseConnection();
            }
        }

        private void LoadBookingHistory()
        {
            try
            {
                dataBase.OpenConnection();
                string queryString = @"
                    SELECT rental_Start_Date, end_Of_Rental, actual_End_Of_Rental
                    FROM Rent
                    WHERE ID_Klient = @idKlient";
                using (SqlCommand command = new SqlCommand(queryString, dataBase.GetSqlConnection()))
                {
                    command.Parameters.AddWithValue("@idKlient", _idKlient);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var bookingHistory = new List<BookingHistoryItem>();
                        while (reader.Read())
                        {
                            var rentalStartDate = reader.GetDateTime(reader.GetOrdinal("rental_Start_Date"));
                            var endOfRental = reader.GetDateTime(reader.GetOrdinal("end_Of_Rental"));
                            var actualEndOfRental = reader.IsDBNull(reader.GetOrdinal("actual_End_Of_Rental")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("actual_End_Of_Rental"));

                            string actualEndDateText = actualEndOfRental.HasValue
                                ? actualEndOfRental.Value.ToString("dd.MM.yyyy HH:mm:ss")
                                : "Авто ще не повернули";

                            bookingHistory.Add(new BookingHistoryItem
                            {
                                StartDate = rentalStartDate,
                                EndDate = endOfRental,
                                ActualEndDate = actualEndDateText
                            });
                        }
                        orderListBox.ItemsSource = bookingHistory;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні історії бронювань: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                dataBase.CloseConnection();
            }
        }

        private void LoadFinesHistory()
        {
            try
            {
                dataBase.OpenConnection();
                string queryString = @"
                    SELECT name_Fines, sum_Fines
                    FROM Fines
                    WHERE ID_Klient = @idKlient";
                using (SqlCommand command = new SqlCommand(queryString, dataBase.GetSqlConnection()))
                {
                    command.Parameters.AddWithValue("@idKlient", _idKlient);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var finesHistory = new List<FinesHistoryItem>();
                        while (reader.Read())
                        {
                            finesHistory.Add(new FinesHistoryItem
                            {
                                NameFines = reader["name_Fines"].ToString(),
                                SumFines = reader["sum_Fines"].ToString()
                            });
                        }
                        finesListBox.ItemsSource = finesHistory;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні історії штрафів: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                dataBase.CloseConnection();
            }
        }

        private void LoadDiscountHistory()
        {
            try
            {
                dataBase.OpenConnection();
                string queryString = @"
                    SELECT name_Discount, percent_Discount
                    FROM Discount
                    WHERE ID_Klient = @idKlient";
                using (SqlCommand command = new SqlCommand(queryString, dataBase.GetSqlConnection()))
                {
                    command.Parameters.AddWithValue("@idKlient", _idKlient);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var discountHistory = new List<DiscountHistoryItem>();
                        while (reader.Read())
                        {
                            discountHistory.Add(new DiscountHistoryItem
                            {
                                NameDiscount = reader["name_Discount"].ToString(),
                                PercentDiscount = reader["percent_Discount"].ToString()
                            });
                        }
                        discountListBox.ItemsSource = discountHistory;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні історії знижок: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                dataBase.CloseConnection();
            }
        }

        private void MainWindow_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_idKlient);
            mainWindow.Show();
            this.Close();
        }

        private void button_exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public class BookingHistoryItem
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string ActualEndDate { get; set; }
        }

        public class FinesHistoryItem
        {
            public string NameFines { get; set; }
            public string SumFines { get; set; }
        }

        public class DiscountHistoryItem
        {
            public string NameDiscount { get; set; }
            public string PercentDiscount { get; set; }
        }
    }
}
