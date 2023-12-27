using MyLib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows;

namespace Course
{

    public partial class Profile : Window
    {
        private int _idKlient;
        DataBase dataBase = new DataBase();
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

        public Profile(int idKlient)
        {
            InitializeComponent();
            
           _idKlient = idKlient;
            GetProfile();
            LoadBookingHistory(_idKlient);
            LoadFinesHistory(_idKlient);
            LoadDiscountHistory(_idKlient);
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

        private void LoadBookingHistory(int idKlient)
        {
            try
            {
                dataBase.openConnection();

                string queryString = @"
            SELECT rental_Start_Date, end_Of_Rental, actual_End_Of_Rental
            FROM Rent
            WHERE ID_Klient = @idKlient
        ";

                using (SqlCommand command = new SqlCommand(queryString, dataBase.getSqlConnection()))
                {
                    command.Parameters.AddWithValue("@idKlient", idKlient);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<BookingHistoryItem> bookingHistory = new List<BookingHistoryItem>();

                        while (reader.Read())
                        {
                            DateTime rentalStartDate = (DateTime)reader["rental_Start_Date"];
                            DateTime endOfRental = (DateTime)reader["end_Of_Rental"];
                            DateTime actualEndOfRental = (DateTime)reader["actual_End_Of_Rental"];



                            string actualEndDateText = (actualEndOfRental == DateTime.ParseExact("1900-01-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture))
                                ? "Авто ще не повернули"
                                : actualEndOfRental.ToString("dd.MM.yyyy HH:mm:ss");
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
                dataBase.closeConnection();
            }
        }


        private void LoadFinesHistory(int idKlient)
        {
            try
            {
                dataBase.openConnection();

                string queryString = @"
            SELECT name_Fines, sum_Fines
            FROM Fines
            WHERE ID_Klient = @idKlient
        ";

                using (SqlCommand command = new SqlCommand(queryString, dataBase.getSqlConnection()))
                {
                    command.Parameters.AddWithValue("@idKlient", idKlient);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<FinesHistoryItem> finesHistory = new List<FinesHistoryItem>();

                        while (reader.Read())
                        {
                            string name_Fines = reader["name_Fines"].ToString();
                            string sum_Fines = reader["sum_Fines"].ToString();

                            finesHistory.Add(new FinesHistoryItem
                            {
                                NameFines = name_Fines,
                                SumFines = sum_Fines
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
                dataBase.closeConnection();
            }
        }


        private void LoadDiscountHistory(int idKlient)
        {
            try
            {
                dataBase.openConnection();

                string queryString = @"
                    SELECT name_Discount, percent_Discount
                    FROM Discount
                    WHERE ID_Klient = @idKlient
        ";

                using (SqlCommand command = new SqlCommand(queryString, dataBase.getSqlConnection()))
                {
                    command.Parameters.AddWithValue("@idKlient", idKlient);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<DiscountHistoryItem> discountHistory = new List<DiscountHistoryItem>();

                        while (reader.Read())
                        {
                            string name_Discount = reader["name_Discount"].ToString();
                            string percent_Discount = reader["percent_Discount"].ToString();

                            discountHistory.Add(new DiscountHistoryItem
                            {
                                NameDiscount = name_Discount,
                                PercentDiscount = percent_Discount
                            });
                        }

                        discountListBox.ItemsSource = discountHistory;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні історії персональних акцій користувача: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
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
