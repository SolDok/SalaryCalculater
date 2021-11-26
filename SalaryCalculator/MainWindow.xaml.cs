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
            string connectionString = "Data source = SalaryDB.db";
            all_employees all_emps = new (connectionString);
            List_employees.Items.Add(all_emps.GetEmployee(0));
            InitializeComponent();
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
    }

    public class all_employees
    {
        static private employee[] data;

        public all_employees(string connString)
        {
            ReadEmployeesFromDB(connString);
        }

        static public void ReadEmployeesFromDB(string connString)
        {
            string sql = "SELECT * FROM employee";
            using (SqliteConnection conn = new SqliteConnection(connString))
            {
                SqliteCommand cmd = new SqliteCommand(sql, conn);
                try
                {
                    conn.Open();
                    using (SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            for (int i = 0; reader.Read(); i++)
                            {
                                var f_name = reader.GetString(1);
                                var s_name = reader.GetString(2);
                                employee emp = new employee(f_name, s_name);
                                data[i] = emp;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public employee GetEmployee(int i)
        {
            return data[i];
        }
    }
 
}
