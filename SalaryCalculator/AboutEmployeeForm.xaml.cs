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
            sqlExpression = $"SELECT * FROM work_operations WHERE emp_id = {selectedEmployee}";


        }
        //Мне нужны: Индекс из таблицы с главной формы, Объект из all_employee.data
        private void ShowListOperations (List<String> operations)
        {
            ListOfOperations.Items.Clear();
            ListOfOperations.ItemsSource = operations;
        }
        private void datePicker2_SelectedDate(object sender, SelectionChangedEventArgs e)
        {
            Operations op1 = new Operations();
            op1.GetStringsFromDB(MainWindow.connectionString,sqlExpression);
            ShowListOperations(op1.GetListOfOperations());
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ButtonAddOperation_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    class Operations
    {

        List<String> ListEmpOps = new List<String>();
        
        public List<String> GetListOfOperations()
        {
            return ListEmpOps;
        }
        public void GetStringsFromDB(string connString,string sqlExpression)
        {
            //метод получения записей из 
            var conn = new SqliteConnection(connString);
            SqliteCommand cmd = new SqliteCommand(sqlExpression, conn);
            try
            {
                conn.Open();
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
