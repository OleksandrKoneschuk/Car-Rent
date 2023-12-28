using MyLib;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Course
{

    public partial class DataBaseEditor : Window
    {
        DataBase dataBase = new DataBase();
        private string _showBy;
        private string _columnName;
        private object _Id;
        private string _firstColumnName;


        public DataBaseEditor(string queryString, int check, string showBy)
        {
            InitializeComponent();
            LoadDataIntoGrid(queryString);

            if (check == 1)
            {
                showComboBox.Visibility = Visibility.Hidden;
                gridId.Visibility = Visibility.Hidden;
                _showBy = showBy;
                ComboBoxItem item3 = new ComboBoxItem() { Content = "Таблиця оренди авто", Tag = "Rent" };
                showComboBox.Items.Add(item3);

            }
            else if(check == 2)
            {
                gridTime.Visibility = Visibility.Hidden;
                gridId.Visibility = Visibility.Hidden;
                _showBy = showBy;

                ComboBoxItem item1 = new ComboBoxItem() { Content = "Таблиця авто", Tag = "Avto" };
                ComboBoxItem item2 = new ComboBoxItem() { Content = "Таблиця клієнт", Tag = "Klient" };
                ComboBoxItem item3 = new ComboBoxItem() { Content = "Таблиця оренди авто", Tag = "Rent" };
                ComboBoxItem item4 = new ComboBoxItem() { Content = "Таблиця штрафів", Tag = "Fines" };
                ComboBoxItem item5 = new ComboBoxItem() { Content = "Таблиця знижок", Tag = "Discount" };

                showComboBox.Items.Add(item1);
                showComboBox.Items.Add(item2);
                showComboBox.Items.Add(item3);
                showComboBox.Items.Add(item4);
                showComboBox.Items.Add(item5);
            }
            else
            {
                gridTime.Visibility = Visibility.Hidden;
                ComboBoxItem item4 = new ComboBoxItem() { Content = "Таблиця штрафів", Tag = "Fines" };
                ComboBoxItem item5 = new ComboBoxItem() { Content = "Таблиця знижок", Tag = "Discount" };

                showComboBox.Items.Add(item4);
                showComboBox.Items.Add(item5);
            }
        }


        private void showComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)showComboBox.SelectedItem;
            if (selectedItem != null)
            {
                _showBy = selectedItem.Tag.ToString();
                string queryString = $"SELECT * FROM {_showBy}";
                LoadDataIntoGrid(queryString);
            }
        }


        private void DataGrid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is DataGridCell))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
            {
                return;
            }

            DataGridCellInfo selectedCell = dataGrid.SelectedCells.FirstOrDefault();

            if (selectedCell != null)
            {
                object cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);

                if (cellContent is TextBlock textBlock)
                {
                    string cellValue = textBlock.Text;

                    textBox_forUpdate.Text = cellValue;

                    _columnName = selectedCell.Column.Header.ToString();

                    _firstColumnName = dataGrid.Columns.Count > 0 ? dataGrid.Columns[0].Header.ToString() : string.Empty;

                    object rowData = selectedCell.Item;

                    if (rowData is DataRowView dataRowView)
                    {
                        _Id = dataRowView.Row.ItemArray.Length > 0 ? dataRowView.Row.ItemArray[0] : null;
                    }
                }
            }
        }


        private void DataUpdate(string newValue)
        {
            try
            {
                dataBase.openConnection();

                string updateQuery = $"UPDATE {_showBy} SET {_columnName} = @NewValue WHERE {_firstColumnName} = @ConditionValue";

                using (SqlCommand command = new SqlCommand(updateQuery, dataBase.getSqlConnection()))
                {
                    command.Parameters.AddWithValue("@NewValue", newValue);
                    command.Parameters.AddWithValue("@ConditionValue", _Id);

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Дані успішно оновлено", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при оновленні даних: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                dataBase.closeConnection();
                textBox_forUpdate.Clear();
            }
        }


        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dataBase.openConnection();

                string deleteQuery = $"DELETE FROM {_showBy} WHERE {_firstColumnName} = @IdToDelete";

                using (SqlCommand command = new SqlCommand(deleteQuery, dataBase.getSqlConnection()))
                {
                    command.Parameters.AddWithValue("@IdToDelete", _Id);

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Рядок успішно видалено", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при видаленні рядка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                dataBase.closeConnection();
                LoadDataIntoGrid($"SELECT * FROM {_showBy}");
                textBox_forUpdate.Clear();
            }
        }


        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            DataGridCellInfo selectedCell = dataGrid.SelectedCells[0];

            object cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
            string newValue = textBox_forUpdate.Text;

            if (cellContent is TextBlock textBlock)
            {
                textBlock.Text = newValue;
            }

            DataUpdate(newValue);
            textBox_forUpdate.Clear();
        }


        private void LoadDataIntoGrid(string queryString)
        {
            DataTable dataTable = GetDataFromDatabase(queryString);
            dataGrid.ItemsSource = dataTable.DefaultView;
            HideColumn("PhotoData");
        }


        private void HideColumn(string columnName)
        {
            DataGridColumn column = dataGrid.Columns.FirstOrDefault(c => c.Header.ToString() == columnName);

            if (column != null)
            {
                column.Visibility = Visibility.Collapsed;
            }
        }


        private DataTable GetDataFromDatabase(string queryString)
        {
            DataTable dataTable = new DataTable();
            try
            {
                dataBase.openConnection();

                using (SqlCommand command = new SqlCommand(queryString, dataBase.getSqlConnection()))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні даних з БД: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            finally
            {
                dataBase.closeConnection();
            }
        }

        private void ProcessTable()
        {
            string proc;
            switch (_showBy)
            {
                case "Klient":
                    proc = "SearchInfoInKlient";
                    Search(proc);
                    break;

                case "Avto":
                    proc = "SearchInfoInAvto";
                    Search(proc);
                    break;

                case "Rent":
                    proc = "SearchInfoInRent";
                    Search(proc);
                    break;


                default:
                    MessageBox.Show($"Не відома таблиця: {_showBy}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }


        private void Search(string proc)
        {
            try
            {
                dataBase.openConnection();

                using (SqlCommand command = new SqlCommand(proc, dataBase.getSqlConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@searchText", searchTextBox.Text);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGrid.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при пошуку даних: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                dataBase.closeConnection();
            }
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProcessTable();
        }

        private void button_GoToAdminMenu_Click(object sender, RoutedEventArgs e)
        {
            AdminMenu AdminMenu = new AdminMenu();
            AdminMenu.Show();
            this.Close();
        }

        private void GetDateTime_Click(object sender, RoutedEventArgs e)
        {
            if (_showBy == "Rent" && _columnName == "actual_End_Of_Rental")
            {
                DateTime currentDateTime = DateTime.Now;
                string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                timeTextBox.Text = currentDateTime.ToString("HH:mm:ss.fff");

                textBox_forUpdate.Text = formattedDateTime;
                MessageBox.Show($"Ви вибрали: {formattedDateTime}");
            }
            else
                MessageBox.Show($"Помилка при виборі комірок, ви можете \nзмінити дату тільки в таблиці Rent \nдля її стовпця actual_End_Of_Rental", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void GetIdKlient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dataBase.openConnection();

                string insertQuery = $"INSERT INTO {_showBy} (ID_Klient) Values (@ID_Klient);";

                using (SqlCommand command = new SqlCommand(insertQuery, dataBase.getSqlConnection()))
                {

                    command.Parameters.AddWithValue("@ID_Klient", idTextBox.Text);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Дані успішно додано", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при вставці даних: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                dataBase.closeConnection();
                LoadDataIntoGrid($"SELECT * FROM {_showBy}");
                idTextBox.Clear();
            }
        }
    }
}
