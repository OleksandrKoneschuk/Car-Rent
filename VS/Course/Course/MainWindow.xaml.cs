using MyLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Course
{
    public partial class MainWindow : Window, IObserver
    {
        private readonly ObservableCollection<Car> cars = new ObservableCollection<Car>();
        private readonly CarDataNotifier carDataNotifier = new CarDataNotifier();
        private readonly DataBase dataBase = SqlDataBase.Instance;

        private int _idKlient;
        private DateTime _startDate;
        private DateTime _endDate;

        public MainWindow(int idKlient)
        {
            InitializeComponent();
            _idKlient = idKlient;
            carListBox.ItemsSource = cars;
            carDataNotifier.Attach(this);

            InitializeDates();
            UpdateCarsCollection("SELECT * FROM Avto");
        }

        private void InitializeDates()
        {
            DateTime currentDate = DateTime.Now;
            DateTime nextDay = currentDate.AddDays(1);
            Start_Date_DatePicker.DisplayDateStart = currentDate;
            end_Of_Rental_DatePicker.DisplayDateStart = nextDay;

            _startDate = currentDate;
            _endDate = nextDay;
        }

        public void Update()
        {
            UpdateCarsCollection("SELECT * FROM Avto");
        }

        private void sortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)sortComboBox.SelectedItem;
            if (selectedItem != null)
            {
                string orderBy = selectedItem.Tag.ToString();
                string queryString = $"SELECT * FROM Avto ORDER BY {orderBy}";
                UpdateCarsCollection(queryString);
            }
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            string queryString = $"SELECT * FROM Avto WHERE CONCAT(ID_Avto, car_Number, car_Brand, car_Model, car_Color, car_Year, average_Fuel_Consumption) LIKE '%" + searchTextBox.Text + "%'";
            UpdateCarsCollection(queryString);
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
            List<Car> carList = new List<Car>();
            dataBase.OpenConnection();

            using (SqlCommand command = new SqlCommand(queryString, dataBase.GetSqlConnection()))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var car = new Car
                    {
                        ID_Avto = (int)reader["ID_Avto"],
                        CarNumber = reader["car_Number"].ToString(),
                        CarBrand = reader["car_Brand"].ToString(),
                        CarModel = reader["car_Model"].ToString(),
                        CarColor = reader["car_Color"].ToString(),
                        CarYear = (int)reader["car_Year"],
                        AverageFuelConsumption = (decimal)reader["average_Fuel_Consumption"],
                        PhotoData = reader["PhotoData"] as byte[],
                        Cost1_3DayRental = (decimal)reader["cost_1_3_Day_Rental"],
                        Cost4_9DayRental = (decimal)reader["cost_4_9_Day_Rental"],
                        Cost10_25DayRental = (decimal)reader["cost_10_25_Day_Rental"],
                        Cost26DayRental = (decimal)reader["cost_26_Day_Rental"]
                    };
                    carList.Add(car);
                }
            }

            dataBase.CloseConnection();
            return carList;
        }

        private void button_FindAvto_Click(object sender, RoutedEventArgs e)
        {
            _startDate = (Start_Date_DatePicker.SelectedDate ?? DateTime.MinValue).Date;
            _endDate = (end_Of_Rental_DatePicker.SelectedDate ?? DateTime.MinValue).Date;

            if (_startDate < _endDate)
            {
                string queryString = $"SELECT Avto.* FROM Avto LEFT JOIN Rent ON Avto.ID_Avto = Rent.ID_Avto WHERE Rent.ID_Renta IS NULL OR (Rent.end_Of_Rental < '{_startDate:yyyy-MM-dd}' OR Rent.rental_Start_Date > '{_endDate:yyyy-MM-dd}')";
                UpdateCarsCollection(queryString);
            }
            else
            {
                MessageBox.Show("Дата початку повинна бути менше за дату завершення.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Profile_Button_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile(_idKlient);
            profile.Show();
            this.Close();
        }

        private void button_ChooseCar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                if (_startDate < _endDate)
                {
                    int idAvto = (int)clickedButton.Tag;
                    IMessageService messageService = new SmsMessageService();
                    ChooseCarWindow childWindow = new ChooseCarWindow(_startDate, _endDate, idAvto, _idKlient, messageService);
                    childWindow.Owner = this;
                    childWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Дата початку повинна бути менше за дату завершення.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            carDataNotifier.Detach(this);
        }
    }
}
