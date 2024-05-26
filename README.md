# Car Rent Application

## Description

Car Rent Application is a software solution for car rental services that allows users to book cars, view booking history, receive discounts, and manage the database of cars, customers, and bookings.

## Features

1. Registration and Authentication
   - Users can create a new account and log in to the system.
   - Administrators can access a special administrative interface.

2. Car Booking
   - Users can select cars for rental by specifying the start and end dates of the rental period.
   - Cars that are already booked for the selected dates will not be available for booking.

3. Profile Management
   - Users can view and update their personal information.
   - View history of bookings, fines, and discounts.

4. Administrative Interface
   - Administrators can add, update, and delete records in the database of cars, customers, and bookings.
   - Manage fines and discounts for customers.

## Running Locally

### Requirements

- Microsoft Visual Studio
- .NET Framework
- SQL Server

### Steps to Run

1. Clone the repository
   ```bash
   git clone https://github.com/OleksandrKoneschuk/Car-Rent.git
   ```

2. Open the project in Visual Studio
   - Open Visual Studio.
   - Select "File" > "Open" > "Project/Solution".
   - Select the solution file (.sln) in the project folder.

3. Set up the database
   - Create a SQL Server database.
   - Execute scripts to create tables and insert initial data.

4. Configure the database connection
   - Open the file `DataBase.cs`.
   - Update the connection string to match your database settings.

5. Run the project
   - Click the "Start" button in Visual Studio.

## Programming Principles

1. Single Responsibility Principle:
   - The `AdminMenu` class is responsible only for handling user interface events in the administrative menu.
   - [AdminMenu.xaml.cs](VS/Course/Course/AdminMenu.xaml.cs)
   ```csharp
   public partial class AdminMenu : Window
   {
       public AdminMenu()
       {
           InitializeComponent();
       }

       private void open_AdminWindow_Click(object sender, RoutedEventArgs e)
       {
           AdminWindow AdminWindow = new AdminWindow();
           AdminWindow.Show();
           this.Close();
       }
       // Other event handling methods...
   }
   ```

2. Encapsulation:
   - Encapsulation of car properties in the `Car` class.
   - [Car.cs](VS/Course/MyLib/Car.cs)
   ```csharp
   public class Car
   {
       public int ID_Avto { get; set; }
       public string CarNumber { get; set; }
       public string CarBrand { get; set; }
       public string CarModel { get; set; }
       public string CarColor { get; set; }
       public int CarYear { get; set; }
       public decimal AverageFuelConsumption { get; set; }
       public byte[] PhotoData { get; set; }
       public decimal Cost1_3DayRental { get; set; }
       public decimal Cost4_9DayRental { get; set; }
       public decimal Cost10_25DayRental { get; set; }
       public decimal Cost26DayRental { get; set; }
       public string CarBrandModel => $"{CarBrand} {CarModel}";
   }
   ```

3. Open/Closed Principle:
   - The `DataBase` class can be extended to support new types of connections without modifying its code.
   - [DataBase.cs](VS/Course/MyLib/DataBase.cs)
   ```csharp
   public abstract class DataBase
   {
       protected SqlConnection sqlConnection;

       public abstract void OpenConnection();
       public abstract void CloseConnection();
       public SqlConnection GetSqlConnection()
       {
           return sqlConnection;
       }
   }

   public class SqlDataBase : DataBase
   {
       private static SqlDataBase instance;

       private SqlDataBase()
       {
           sqlConnection = new SqlConnection(@"Data Source=DESKTOP-FUAH6BA;Initial Catalog=Car_Rent;Integrated Security=True");
       }

       public static SqlDataBase Instance
       {
           get
           {
               if (instance == null)
               {
                   instance = new SqlDataBase();
               }
               return instance;
           }
       }

       public override void OpenConnection()
       {
           if (sqlConnection.State == System.Data.ConnectionState.Closed)
           {
               sqlConnection.Open();
           }
       }

       public override void CloseConnection()
       {
           if (sqlConnection.State == System.Data.ConnectionState.Open)
           {
               sqlConnection.Close();
           }
       }
   }
   ```

4. Liskov Substitution Principle:
   - The `ElectricCar` and `GasCar` subclasses can be used in place of the `Car` base class.
   - [Car.cs](VS/Course/MyLib/Car.cs)
   ```csharp
   public class ElectricCar : Car
   {
       public int BatteryCapacity { get; set; }
       public int RangePerCharge { get; set; }
   }

   public class GasCar : Car
   {
       public decimal TankCapacity { get; set; }
       public decimal FuelEfficiency { get; set; }
   }
   ```

5. Dependency Inversion Principle:
   - Classes should depend on abstractions, not concrete implementations. For example, using interfaces for sending messages.
   - [IMessageService.cs](VS/Course/MyLib/IMessageService.cs), [SmsMessageService.cs](VS/Course/MyLib/SmsMessageService%20.cs)
   ```csharp
   public interface IMessageService
   {
       void SendMessage(string phoneNumber, string message);
   }

   public class SmsMessageService : IMessageService
   {
       public void SendMessage(string phoneNumber, string message)
       {
           // Real SMS sending implementation goes here
           Console.WriteLine($"Sending SMS to {phoneNumber}: {message}");
       }
   }
   ```

## Design Patterns

1. Singleton Pattern:
   - Used to ensure a single instance of the database in the `SqlDataBase` class.
   - [DataBase.cs](VS/Course/MyLib/DataBase.cs)
   ```csharp
   public class SqlDataBase : DataBase
   {
       private static SqlDataBase instance;

       private SqlDataBase()
       {
           sqlConnection = new SqlConnection(@"Data Source=DESKTOP-FUAH6BA;Initial Catalog=Car_Rent;Integrated Security=True");
       }

       public static SqlDataBase Instance
       {
           get
           {
               if (instance == null)
               {
                   instance = new SqlDataBase();
               }
               return instance;
           }
       }

       public override void OpenConnection()
       {
           if (sqlConnection.State == System.Data.ConnectionState.Closed)
           {
               sqlConnection.Open();
           }
       }

       public override void CloseConnection()
       {
           if (sqlConnection.State == System.Data.ConnectionState.Open)
           {
               sqlConnection.Close();
           }
       }
   }
   ```

2. Factory Method:
   - Method for creating an image selection dialog in the `AdminWindow` class.
   - [AdminWindow.xaml.cs](VS/Course/Course/AdminWindow.xaml.cs)
   ```csharp
   private void OpenImageDialog(object sender, RoutedEventArgs e)
   {
       OpenFileDialog openFileDialog = new OpenFileDialog
       {
           Filter = "Images|*.jpg;*.jpeg;*.png;*.gif|All files|*.*"
       };

       if (openFileDialog.ShowDialog() == true)
       {
           string imagePath = openFileDialog.FileName;
           selectedImageData = File.ReadAllBytes(imagePath);
           imagePreview.Source = new BitmapImage(new Uri(imagePath));
       }
   }
   ```

3. Observer Pattern:
   - Implementation of the observer pattern for updating car data in the `CarDataNotifier` class.
   - [Observer.cs](VS/Course/MyLib/Observer.cs)
   ```csharp
   public interface IObserver
   {
       void Update();
   }

   public interface ISubject
   {
       void Attach(IObserver observer);
       void Detach(IObserver observer);
       void Notify();
   }

   public class CarDataNotifier : ISubject
   {
       private List<IObserver> observers = new List<IObserver>();

       public void Attach(IObserver observer)
       {
           observers.Add(observer);
       }

       public void Detach(IObserver observer)
       {
           observers.Remove(observer);
       }

       public void Notify()
       {
           foreach (var observer in observers)
           {
               observer.Update();
           }
       }
   }
   ```

4. Command Pattern:
   - Used for handling events in the `AdminMenu` class.
   - [AdminMenu.xaml.cs](VS/Course/Course/AdminMenu.xaml.cs)
   ```csharp
   private void open_AdminWindow_Click(object sender, RoutedEventArgs e)
   {
       AdminWindow AdminWindow = new AdminWindow();
       AdminWindow.Show();
       this.Close();
   }

   private void open_DataBaseEditor_Click(object sender, RoutedEventArgs e)
   {
       DataBaseEditor DataBaseEditor = new DataBaseEditor("SELECT * FROM Klient", 2, "Klient");
       DataBaseEditor.Show();
       this.Close();
   }

   private void back_to_log_in_Click(object sender, RoutedEventArgs e)
   {
       log_in log_in = new log_in();
       log_in.Show();
       this.Close();
   }

   private void button_exit_Click(object sender, RoutedEventArgs e)
   {
       Application.Current.Shutdown();
   }

   private void return_TheCar_Click(object sender, RoutedEventArgs e)
   {
       Data

    BaseEditor DataBaseEditor = new DataBaseEditor("SELECT * FROM Rent", 1, "Rent");
         DataBaseEditor.Show();
         this.Close();
     }
  
     private void finesDiscount_Click(object sender, RoutedEventArgs e)
     {
         DataBaseEditor DataBaseEditor = new DataBaseEditor("SELECT * FROM Discount", 3, "Discount");
         DataBaseEditor.Show();
         this.Close();
     }
   ```

## Refactoring Techniques

1. Method Extraction:
   - The `AddCarToDatabase` method extracted for adding a car to the database in the `AdminWindow` class.
   - [AdminWindow.xaml.cs](VS/Course/Course/AdminWindow.xaml.cs#L36)
   ```csharp
   private void AddCarToDatabase()
   {
       if (selectedImageData == null)
       {
           MessageBox.Show("Please select a photo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
       MessageBox.Show("Data successfully added to the database", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
   }
   ```

2. Refactoring with Method Extraction:
   - In the `log_in.xaml.cs` and `sign_up.xaml.cs` files, methods were split into smaller ones for better readability.
   - [log_in.xaml.cs](VS/Course/Course/log_in.xaml.cs), [sign_up.xaml.cs](VS/Course/Course/sign_up.xaml.cs)
   ```csharp
   private void LogIn()
   {
       string username = usernameTextBox.Text;
       string password = passwordTextBox.Password;

       if (ValidateUser(username, password))
       {
           // Login logic
       }
       else
       {
           MessageBox.Show("Invalid username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
       }
   }

   private bool ValidateUser(string username, string password)
   {
       // User validation logic
       return true; // Example
   }
   ```

3. Encapsulation:
   - Encapsulation of car properties in the `Car` class.
   - [Car.cs](VS/Course/MyLib/Car.cs)
   ```csharp
   public class Car
   {
       public int ID_Avto { get; set; }
       public string CarNumber { get; set; }
       public string CarBrand { get; set; }
       public string CarModel { get; set; }
       public string CarColor { get; set; }
       public int CarYear { get; set; }
       public decimal AverageFuelConsumption { get; set; }
       public byte[] PhotoData { get; set; }
       public decimal Cost1_3DayRental { get; set; }
       public decimal Cost4_9DayRental { get; set; }
       public decimal Cost10_25DayRental { get; set; }
       public decimal Cost26DayRental { get; set; }
       public string CarBrandModel => $"{CarBrand} {CarModel}";
   }
   ```
