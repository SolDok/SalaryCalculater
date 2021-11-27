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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.Sqlite;

namespace SalaryCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string connectionString = "Data Source = SalaryDB.db";
            NewListBox(List_employees, connectionString);
        }

        public void NewListBox(ListBox ListBoxName, string connectionString)
        {
            all_employees all_emps = new(connectionString);
            CreateListBox(ListBoxName, all_emps.GetArrayEmployeee());
        }
        private void CreateListBox(ListBox ListBoxName,employee[] arr)
        {
            ListBoxName.Items.Clear();
            for (int i = 0; i < arr.Length; i++)
            {
                ListBoxName.Items.Add(arr[i].FullName);
            }
        }
        private void ListItemSelected(object sender, RoutedEventArgs e)
        {
            object emp = List_employees.SelectedItem;
            object index = List_employees.SelectedIndex;
        }
        private void AddGridLine(string str)
        {

        }
        private void Logic_Click(object sender, RoutedEventArgs e)
        {
            NewEmployeeWindow newEmployeeWindow = new();

            if (newEmployeeWindow.ShowDialog() == true)
            {
               if (newEmployeeWindow.result == 1)
               {
                    MessageBox.Show("Новый сотрудник был добавлен в базу");
                    string connectionString = "Data Source = SalaryDB.db";
                    NewListBox(List_employees,connectionString);
                }
                else
               {
                    MessageBox.Show("Ошибка при добавлении");
               }
            }
        }
    }


    public class employee
    {
        public string F_name { get; set; }
        public string S_name { get; set; }

        public string FullName
        {
            get { return F_name + " " + S_name; } 
        }
        
        public employee (string f_name, string s_name)
        {
            F_name = f_name;
            S_name = s_name;
        }
        //класс сотрудник
    }

    public class all_employees
    {
        //класс со всеми сотрудниками в массиве data
        //с помощью метода ReadEmployeeFromDB
        //получаем данные из БД и добавляем в data
        private employee[] data;

        public all_employees(string connString)
        {
            int count = GetNumberOfEmployees(connString);
            data = new employee[count];
            ReadEmployeesFromDB(connString);
        }
        
        private int GetNumberOfEmployees(string connString)
        {
            string sqlExpression = "SELECT COUNT(*) FROM employees";
            var conn = new SqliteConnection(connString);
            SqliteCommand cmd = new SqliteCommand(sqlExpression, conn);
            int count = 0;
            try
            {
                conn.Open();
                count = (int)(long)cmd.ExecuteScalar();
                // интересный момент с "распоковкой", если напрямую в int, выпадает исключение
            }
            catch (Exception ex)
            {
                string message = ex.Message + " From GetNumberOfEmployees";
                MessageBox.Show(message);
            }
            return count;
        }

        void ReadEmployeesFromDB(string connString)
        {
            string sql = "SELECT * FROM employees";
            var conn = new SqliteConnection(connString);
            SqliteCommand cmd = new SqliteCommand(sql, conn);
            try
            {
                conn.Open();
                SqliteDataReader reader = cmd.ExecuteReader();        
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        string f_name = reader.GetString(1);
                        string s_name = reader.GetString(2);
                        employee emp = new employee(f_name, s_name);
                        data[i] = emp;
                        i++;
                    }
                }
                else
                {
                    MessageBox.Show("reader не считал таблицу/пустой");
                }    
            }
            catch (Exception ex)
            {
                    MessageBox.Show(ex.Message);
            }
        }
        
        static internal int AddNewEmployee(string connString, string emp_f_name, string emp_s_name)
        {
            string sqlExpression = $"INSERT INTO employees (f_name, s_name) VALUES ('{emp_f_name}','{emp_s_name}');";
            var conn = new SqliteConnection(connString);
            SqliteCommand cmd = new SqliteCommand(sqlExpression, conn);
            int number = 0;
            try
            {
                conn.Open();
                number = (int)cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string message = ex.Message + "Exception from AddNewEmployee";
                MessageBox.Show(message);
            }
            return number;
        }
        public employee GetEmployee(int i)
        {
            return data[i];
        }
        public employee[] GetArrayEmployeee()
        {
            return data;
        }
    }
 
}
