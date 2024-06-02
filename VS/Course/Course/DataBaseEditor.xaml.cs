using MyLib;
using MyLib.DB;
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
        private readonly DataBase dataBase = SqlDataBase.Instance;
        private string _showBy;
        private string _columnName;
        private object _Id;
        private string _firstColumnName;

        public DataBaseEditor(string queryString, int check, string showBy)
        {
            InitializeComponent();
            LoadDataIntoGrid(queryString);
            _showBy = showBy;

            ConfigureUI(check);
        }

        private void ConfigureUI(int check)
        {
            if (check == 1)
            {
                showComboBox.Visibility = Visibility.Hidden;
                gridId.Visibility = Visibility.Hidden;
                AddComboBoxItem("Таблиця оренди авто", "Rent");
            }
            else if (check == 2)
            {
                gridTime.Visibility = Visibility.Hidden;
                gridId.Visibility = Visibility.Hidden;
                AddComboBoxItem("Таблиця авто", "Avto");
                AddComboBoxItem("Таблиця клієнт", "Klient");
                AddComboBoxItem("Таблиця оренди авто", "Rent");
                AddComboBoxItem("Таблиця штрафів", "Fines");
                AddComboBoxItem("Таблиця знижок", "Discount");
            }
            else
            {
                gridTime.Visibility = Visibility.Hidden;
                AddComboBoxItem("Таблиця штрафів", "Fines");
                AddComboBoxItem("Таблиця знижок", "Discount");
            }
        }

        private void AddComboBoxItem(string content, string tag)
        {
            ComboBoxItem item = new ComboBoxItem() { Content = content, Tag = tag };
            showComboBox.Items.Add(item);
        }

        private void showComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (showComboBox.SelectedItem is ComboBoxItem selectedItem)
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
                if (selectedCell.Column.GetCellContent(selectedCell.Item) is TextBlock textBlock)
                {
                    textBox_forUpdate.Text = textBlock.Text;

                    _columnName = selectedCell.Column.Header.ToString();
                    _firstColumnName = dataGrid.Columns.Count > 0 ? dataGrid.Columns[0].Header.ToString() : string.Empty;

                    if (selectedCell.Item is DataRowView dataRowView)
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
                dataBase.OpenConnection();
                string updateQuery = $"UPDATE {_showBy} SET {_columnName} = @NewValue WHERE {_firstColumnName} = @ConditionValue";

                using (SqlCommand command = new SqlCommand(updateQuery, dataBase.GetSqlConnection()))
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
                dataBase.CloseConnection();
                textBox_forUpdate.Clear();
            }
        }

        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dataBase.OpenConnection();
                string deleteQuery = $"DELETE FROM {_showBy} WHERE {_firstColumnName} = @IdToDelete";

                using (SqlCommand command = new SqlCommand(deleteQuery, dataBase.GetSqlConnection()))
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
                dataBase.CloseConnection();
                LoadDataIntoGrid($"SELECT * FROM {_showBy}");
                textBox_forUpdate.Clear();
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedCells.Count > 0)
            {
                var selectedCell = dataGrid.SelectedCells[0];
                if (selectedCell.Column.GetCellContent(selectedCell.Item) is TextBlock textBlock)
                {
                    textBlock.Text = textBox_forUpdate.Text;
                    DataUpdate(textBox_forUpdate.Text);
                }
            }
        }

        private void LoadDataIntoGrid(string queryString)
        {
            DataTable dataTable = GetDataFromDatabase(queryString);
            dataGrid.ItemsSource = dataTable.DefaultView;
            HideColumn("PhotoData");
        }

        private void HideColumn(string columnName)
        {
            var column = dataGrid.Columns.FirstOrDefault(c => c.Header.ToString() == columnName);
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
                dataBase.OpenConnection();
                using (var command = new SqlCommand(queryString, dataBase.GetSqlConnection()))
                {
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні даних з БД: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                dataBase.CloseConnection();
            }
            return dataTable;
        }

        private void ProcessTable()
        {
            string proc = null;
            if (_showBy == "Klient")
            {
                proc = "SearchInfoInKlient";
            }
            else if (_showBy == "Avto")
            {
                proc = "SearchInfoInAvto";
            }
            else if (_showBy == "Rent")
            {
                proc = "SearchInfoInRent";
            }
            else
            {
                MessageBox.Show($"Не відома таблиця: {_showBy}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Search(proc);
        }

        private void Search(string proc)
        {
            try
            {
                dataBase.OpenConnection();
                using (var command = new SqlCommand(proc, dataBase.GetSqlConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@searchText", searchTextBox.Text);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
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
                dataBase.CloseConnection();
            }
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProcessTable();
        }

        private void button_GoToAdminMenu_Click(object sender, RoutedEventArgs e)
        {
            var adminMenu = new AdminMenu();
            adminMenu.Show();
            this.Close();
        }

        private void GetDateTime_Click(object sender, RoutedEventArgs e)
        {
            if (_showBy == "Rent" && _columnName == "actual_End_Of_Rental")
            {
                var currentDateTime = DateTime.Now;
                string formattedDateTime = currentDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                timeTextBox.Text = currentDateTime.ToString("HH:mm:ss.fff");
                textBox_forUpdate.Text = formattedDateTime;
                MessageBox.Show($"Ви вибрали: {formattedDateTime}");
            }
            else
            {
                MessageBox.Show("Помилка при виборі комірок, ви можете змінити дату тільки в таблиці Rent для її стовпця actual_End_Of_Rental", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetIdKlient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dataBase.OpenConnection();
                string insertQuery = $"INSERT INTO {_showBy} (ID_Klient) Values (@ID_Klient)";
                using (var command = new SqlCommand(insertQuery, dataBase.GetSqlConnection()))
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
                dataBase.CloseConnection();
                LoadDataIntoGrid($"SELECT * FROM {_showBy}");
                idTextBox.Clear();
            }
        }
    }
}
