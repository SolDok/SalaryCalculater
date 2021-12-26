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
        private void UpdateListOperations (ObservableCollection<Operation> operations)
        {
            operationsGrid.ClearValue(ItemsControl.ItemsSourceProperty);
            operationsGrid.ItemsSource = operations;
            operationsGrid.Items.Refresh();
        }
        private void datePicker2_SelectedDate(object sender, SelectionChangedEventArgs e)
        {
            string date1 = datePicker1.Text;
            string date2 = datePicker2.Text;
            sqlExpression = $"SELECT * FROM work_operations WHERE (emp_id = {selectedEmployee}) AND (w_date BETWEEN '{date1}' AND '{date2}')";
            Operations op1 = new Operations();
            op1.ReadStringsFromDB(MainWindow.connectionString,sqlExpression);
            UpdateListOperations(op1.ReadListOfOperations());
        }
        private void ButtonAddOperation_Click(object sender, RoutedEventArgs e)
        {
            AddOpsForm addOpsForm = new AddOpsForm();
            addOpsForm.Owner = this;
            addOpsForm.ShowDialog();
        }
        private void ButtonDeleteOperation_Click(object sender, RoutedEventArgs e)
        {
            object obj = operationsGrid.SelectedItem;
            Operation? SelectedOperation = (Operation)obj;
            AcceptForm deleteAccept = new AcceptForm();
            if (deleteAccept.ShowDialog() == true)
            {
                Operations.DeleteFromDB(MainWindow.connectionString,"work_operations",SelectedOperation.op_id);
            }
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
            this.Owner.Show();
        }
        
    }
    internal class Operation
    {
        public Operation (int Op_id, string Op_title, string Op_date, double Op_time, double Op_rate, double Op_sum)
        {
            this.op_id = Op_id;
            this.op_title = Op_title;
            this.op_date = Op_date;
            this.op_hours = Op_time;
            this.op_rate = Op_rate;
            this.op_sum = Op_sum;
        }
        public int op_id { get; set; }
        public string op_title { get; set; } 
        public string op_date { get; set; }
        public double op_hours { get; set; }
        public double op_rate { get; set; }
        public double op_sum { get; set; }
        
    }
    class Operations
    {
        //Класс для работы с операциями (рабочеми днями) рабочих
        ObservableCollection<Operation> ListEmpOps = new ObservableCollection<Operation> ();
        
        public ObservableCollection<Operation> ReadListOfOperations()
        {
            return ListEmpOps;
        }
        public void ReadStringsFromDB(string connString,string sqlExpression)
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
                                int op_id = reader.GetInt32(0);
                                string op_title = reader.GetString(1);
                                string op_date = reader.GetString(2);
                                double op_time = reader.GetDouble(3);
                                double op_rate = reader.GetDouble(4);
                                double op_summ = reader.GetDouble(5);

                                ListEmpOps.Add(new Operation(op_id, op_title, op_date, op_time, op_rate,op_summ));
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
        static public int InsertEmployeeOperation (string connString, string op_title, string op_date, double op_hours, double op_rate, double op_summ,int emp_id)
        {
            //Метод добавления операций в БД
            string sqlExpression = $"INSERT INTO work_operations (w_title, w_date, w_hours, w_rate, w_summ, emp_id) VALUES ('{op_title}','{op_date}', {op_hours}, {op_rate}, {op_summ}, {emp_id});";
            var conn = new SqliteConnection(connString);
            SqliteCommand cmd = new SqliteCommand(sqlExpression, conn);
            int NumberOfModified = 0;
            try
            {
                conn.Open();
                NumberOfModified = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string message = ex.Message + "Exception from InsertEmployeeOperation";
                MessageBox.Show(message);
            }
            return NumberOfModified;
        }
        static internal int DeleteFromDB(string connectionString,string tableName, int id)
        {
            string sqlExpression = $"DELETE FROM {tableName} WHERE id = {id}";
            SqliteConnection conn = new SqliteConnection(connectionString);
            SqliteCommand cmd = new SqliteCommand(sqlExpression, conn);
            int NumberOfModified = 0;
            try
            {
                conn.Open();
                NumberOfModified = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string message = ex.Message + " From DeleteEmployeeFromDB";
                MessageBox.Show(message);
            }
            return NumberOfModified;
        }
    }
}
