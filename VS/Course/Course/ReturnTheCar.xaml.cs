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
    /// <summary>
    /// Логика взаимодействия для ReturnTheCar.xaml
    /// </summary>
    public partial class ReturnTheCar : Window
    {
        public ReturnTheCar()
        {
            InitializeComponent();
        }

        private void button_GoToAdminMenu_Click(object sender, RoutedEventArgs e)
        {
            AdminMenu AdminMenu = new AdminMenu();
            AdminMenu.Show();
            this.Close();
        }
    }
}
