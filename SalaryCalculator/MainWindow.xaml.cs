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
            all_employees all_emps = new(connectionString);
            foreach (employee emp in all_emps.GetArrayEmployeee())
            {
                List_employees.Items.Add(emp.F_name + " " + emp.S_name);
            }
        }

    }

    public class employee
    {
        public string F_name { get; set; }
        public string S_name { get; set; }
        
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
        private employee[] data = new employee[3];

        public all_employees(string connString)
        {
            ReadEmployeesFromDB(connString);
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
