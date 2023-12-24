using MyLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;


namespace Course
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Car> cars;
        DataBase dataBase = new DataBase();

        private int _idKlient;
        public MainWindow(int idKlient)
        {
            InitializeComponent();
            _idKlient = idKlient;

            cars = new ObservableCollection<Car>();
            carListBox.ItemsSource = cars;

            UpdateCarsCollection("SELECT * FROM Avto");
        }


        private void sortComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)sortComboBox.SelectedItem;
            if (selectedItem != null)
            {
                string orderBy = selectedItem.Tag.ToString();
                string queryString = $"SELECT * FROM Avto ORDER BY {orderBy}";
                UpdateCarsCollection(queryString);
            }
        }

        private void Search()
        {
            string queryString = $"SELECT * FROM Avto WHERE concat (ID_Avto, car_Number, car_Brand, car_Model, car_Color, car_Year, average_Fuel_Consumption) like '%" + searchTextBox.Text + "%'";
            UpdateCarsCollection(queryString);
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void UpdateCarsCollection(string queryString)
        {
            cars.Clear();
            foreach (var car in GetCarData(queryString))
            {
                cars.Add(car);
            }
        }

        private List<Car> GetCarData(string queryString)
        {
            try
            {
                dataBase.openConnection();

                using (SqlCommand command = new SqlCommand(queryString, dataBase.getSqlConnection()))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Car> cars = new List<Car>();

                        while (reader.Read())
                        {
                            string carBrand = reader["car_Brand"].ToString();
                            string carModel = reader["car_Model"].ToString();
                            Car car = new Car
                            {
                                ID_Avto = (int)reader["ID_Avto"],
                                CarNumber = reader["car_Number"].ToString(),
                                CarBrandModel = $"{carBrand} {carModel}",
                                CarColor = reader["car_Color"].ToString(),
                                CarYear = (int)reader["car_Year"],
                                AverageFuelConsumption = (decimal)reader["average_Fuel_Consumption"],
                                PhotoData = reader["PhotoData"] as byte[],
                                Cost1_3DayRental = (decimal)reader["cost_1_3_Day_Rental"],
                                Cost4_9DayRental = (decimal)reader["cost_4_9_Day_Rental"],
                                Cost10_25DayRental = (decimal)reader["cost_10_25_Day_Rental"],
                                Cost26DayRental = (decimal)reader["cost_26_Day_Rental"]
                            };

                            cars.Add(car);
                        }
                        return cars;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні даних про авто: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            finally
            {
                dataBase.closeConnection();
            }
        }

        private void Profile_Button_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile(_idKlient);
            profile.Show();
            this.Close();
        }
    }
}
