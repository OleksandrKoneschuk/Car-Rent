using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Course
{
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
            DataBaseEditor DataBaseEditor = new DataBaseEditor("SELECT * FROM Rent", 1, "Rent");
            DataBaseEditor.Show();
            this.Close();
        }

        private void finesDiscount_Click(object sender, RoutedEventArgs e)
        {
            DataBaseEditor DataBaseEditor = new DataBaseEditor("SELECT * FROM Discount", 3, "Discount");
            DataBaseEditor.Show();
            this.Close();
        }
    }
}
