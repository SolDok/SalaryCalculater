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

namespace SalaryCalculator
{
    /// <summary>
    /// Логика взаимодействия для AboutEmployeeForm.xaml
    /// Получить даты ограничения
    /// Получить операции проходящие через эти ограничения
    /// Вывести операции на форму
    /// </summary>
    public partial class AboutEmployeeForm : Window
    {
        all_employees all_emps;
        internal string sqlExpression;
        internal int selectedEmployee = MainWindow.EmployeeIndex;
        public AboutEmployeeForm(all_employees All_emps)
        {
            this.all_emps = All_emps;
            InitializeComponent();
            fullNameBlock.Text = all_emps.GetEmployee(selectedEmployee).FullName;


        }
        private void ShowListOperations (ObservableCollection<string> operations)
        {
            //Костыль!
            //Без ClearValue вовзращает исключение "Операция недопустима, пока используется ItemsSource"
            //Хороший способ решить эту проблему через привязку данных
            ListOfOperations.ClearValue(ItemsControl.ItemsSourceProperty);
            ListOfOperations.Items.Clear();
            ListOfOperations.ItemsSource = operations;
           
        }
        private void datePicker2_SelectedDate(object sender, SelectionChangedEventArgs e)
        {
            string date1 = datePicker1.Text;
            string date2 = datePicker2.Text;
            sqlExpression = $"SELECT * FROM work_operations WHERE (emp_id = {selectedEmployee}) AND (w_date BETWEEN '{date1}' AND '{date2}')";
            Operations op1 = new Operations();
            op1.GetStringsFromDB(MainWindow.connectionString,sqlExpression);
            ShowListOperations(op1.GetListOfOperations());
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
            this.Owner.Show();
        }
        private void ButtonAddOperation_Click(object sender, RoutedEventArgs e)
        {
            AddOpsForm addOpsForm = new AddOpsForm();
            addOpsForm.ShowDialog();
        }
    }
    class Operations
    {
        //Класс для работы с операциями (рабочеми днями) рабочих
        ObservableCollection<string> ListEmpOps = new ObservableCollection<string> ();
        
        public ObservableCollection<string> GetListOfOperations()
        {
            return ListEmpOps;
        }
        public void GetStringsFromDB(string connString,string sqlExpression)
        {
            using (SqliteConnection conn = new SqliteConnection(connString)) {
                //метод получения записей из БД
                conn.Open();
                using (SqliteCommand cmd = new SqliteCommand(sqlExpression, conn))
                {
                    try
                    {
                        
                        SqliteDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string op_id = reader.GetString(0);
                                string op_title = reader.GetString(1);
                                string op_date = reader.GetString(2);
                                string op_time = reader.GetString(3);
                                string op_rate = reader.GetString(4);
                                string op_summ = reader.GetString(5);

                                string operation = $"id: {op_id} Title: {op_title} Date: {op_date} Rate: {op_rate} Sum: {op_summ}";
                                ListEmpOps.Add(operation);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message + " Exception from reader";
                        MessageBox.Show(message);
                    }
                }
            }
        }
    }
}
