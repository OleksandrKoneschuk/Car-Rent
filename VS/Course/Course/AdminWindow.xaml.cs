using Microsoft.Win32;
using MyLib;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Course
{
    public partial class AdminWindow : Window
    {
        private readonly DataBase dataBase = SqlDataBase.Instance;
        private byte[] selectedImageData;

        public AdminWindow()
        {
            InitializeComponent();
        }

        private void OpenImageDialog(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Зображення|*.jpg;*.jpeg;*.png;*.gif|Всі файли|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                selectedImageData = File.ReadAllBytes(imagePath);
                imagePreview.Source = new BitmapImage(new Uri(imagePath));
            }
        }

        private void AddCarToDatabase()
        {
            if (selectedImageData == null)
            {
                MessageBox.Show("Будь ласка, оберіть фото", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string carNumber = textBox_carNumber.Text;
            string carBrand = textBox_carBrand.Text;
            string carModel = textBox_carModel.Text;
            string carColor = textBox_carColor.Text;
            int carYear = int.Parse(textBox_carYear.Text);
            decimal averageFuelConsumption = decimal.Parse(textBox_averageFuelConsumption.Text);
            decimal cost1_3DayRental = decimal.Parse(textBox_cost1_3DayRental.Text);
            decimal cost4_9DayRental = decimal.Parse(textBox_cost4_9DayRental.Text);
            decimal cost10_25DayRental = decimal.Parse(textBox_cost10_25DayRental.Text);
            decimal cost26DayRental = decimal.Parse(textBox_cost26DayRental.Text);

            dataBase.OpenConnection();

            string queryString = @"INSERT INTO Avto
                                   (car_Number, car_Brand, car_Model, car_Color, car_Year, 
                                    average_Fuel_Consumption, PhotoData, 
                                    cost_1_3_Day_Rental, cost_4_9_Day_Rental, cost_10_25_Day_Rental, cost_26_Day_Rental)
                                   VALUES
                                   (@CarNumber, @CarBrand, @CarModel, @CarColor, @CarYear, 
                                    @AverageFuelConsumption, @PhotoData, 
                                    @Cost1_3DayRental, @Cost4_9DayRental, @Cost10_25DayRental, @Cost26DayRental)";

            using (SqlCommand command = new SqlCommand(queryString, dataBase.GetSqlConnection()))
            {
                command.Parameters.AddWithValue("@CarNumber", carNumber);
                command.Parameters.AddWithValue("@CarBrand", carBrand);
                command.Parameters.AddWithValue("@CarModel", carModel);
                command.Parameters.AddWithValue("@CarColor", carColor);
                command.Parameters.AddWithValue("@CarYear", carYear);
                command.Parameters.AddWithValue("@AverageFuelConsumption", averageFuelConsumption);
                command.Parameters.AddWithValue("@PhotoData", selectedImageData);
                command.Parameters.AddWithValue("@Cost1_3DayRental", cost1_3DayRental);
                command.Parameters.AddWithValue("@Cost4_9DayRental", cost4_9DayRental);
                command.Parameters.AddWithValue("@Cost10_25DayRental", cost10_25DayRental);
                command.Parameters.AddWithValue("@Cost26DayRental", cost26DayRental);

                command.ExecuteNonQuery();
            }

            dataBase.CloseConnection();
            MessageBox.Show("Дані успішно додані до бази даних", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void button_AddToBD_Click(object sender, RoutedEventArgs e)
        {
            AddCarToDatabase();
        }

        private void button_GoToAdminMenu_Click(object sender, RoutedEventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
            Close();
        }
    }
}