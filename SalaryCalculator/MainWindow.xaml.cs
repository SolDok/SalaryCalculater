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
            GetListBox(List_employees, MainWindow.connectionString);
        }

        internal const string connectionString = "Data Source = SalaryDB.db";

        static internal int EmployeeIndex;
        static private int ListBoxIndex;

        //костыль?Через конструктор метод SearchEmployee не работает
        all_employees all_emps = new(connectionString);

        //Реализовал передачу выбранного элемента с ListBox как статическое свойство класса MainWindow
        //Таким же образов передаю строку подключения к БД
        //Похорошему нужно пересмотреть об инкапсуляции методов в класс работы с БД
        //Вынести все методы связанные с ListBox в отдельный класс

        //Нужен метод, который получает данные с бд, проверяет все элементы ListBox,
        //если не равны, добавляет элемент в ListBox

        private void UpdateListBox()
        {
            //метод реализующий отображение нового элемента в ListBox
            all_emps = new all_employees(connectionString);
            int Numbers = all_emps.GetNumbersOfEmployees(connectionString);
            if (Numbers != List_employees.Items.Count)
            {
                List_employees.Items.Add(all_emps.GetLastEmployee().FullName);
            }

        }

        internal void RemoveElementListBox(int index) 
        {
            List_employees.Items.RemoveAt(index);
        }
        internal void GetListBox(ListBox ListBoxName, string connectionString)
        {
            //метод для получения данных из БД и отрисовки этих данных в ListBox
            all_emps = new(connectionString);
            CreateListBox(ListBoxName, all_emps.GetArrayEmployeee());
        }
        private void CreateListBox(ListBox ListBoxName,employee[] arr)
        { 
            //метод для отрисовки элементов ListBox
            //ListBoxName.Items.Clear();
            for (int i = 0; i < arr.Length; i++)
            {
                ListBoxName.Items.Add(arr[i].FullName);
            }
        }
        private void ListItemSelected(object sender, RoutedEventArgs e)
        {
            //меняет локальную переменную MainWindow на индекс выбранного элемента
            //из главного ListBox
            //кликаем, получаем value(FullName), ищем совпадения в all_employees.data
            //Метод устанавливает MainWindow.index значение, полученное из SearchEmployee
            try
            { 
                if (List_employees.SelectedValue is not null)
                {
                    //Проверка на null нужна, иначе по какой-то причине после метода удаления,
                    //либо SelectedValue падает в null, либо сам обработчик вызывается, когда
                    //не должен

                    string? ListSelectedValue = List_employees.SelectedValue.ToString();
                    int ListSelectedIndex = List_employees.SelectedIndex;
                    MainWindow.EmployeeIndex = all_employees.SearchEmployee(ListSelectedValue, all_emps.GetArrayEmployeee());
                    MainWindow.ListBoxIndex = ListSelectedIndex;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message + " From ListItemSelected";
                MessageBox.Show(message);
            }
        }
        
        private void LogicClickNewEmployee(object sender, RoutedEventArgs e)
        {
            //Метод вызова диалоговово окна для добавления сотрудника
            //также для обновления ListBox в случае нового сотрудника
            NewEmployeeWindow newEmployeeWindow = new();

            if (newEmployeeWindow.ShowDialog() == true)
            {
               if (newEmployeeWindow.Result == 1)
               {
                    MessageBox.Show("Новый сотрудник был добавлен в базу");
                    UpdateListBox(); 
               }
                else
                {
                    MessageBox.Show("Ошибка при добавлении");
                }
            }
        }
        private void LogicClickDeleteEmployee (object sender, RoutedEventArgs e)
        {
            //Идея, попробовать реализвоать LogicClick'и с помощью дженериков
            AcceptForm acceptForm = new();

            if (acceptForm.ShowDialog() == true)
            {
                if (acceptForm.DeleteResult == 1)
                {
                    MessageBox.Show("Сотрудник был удалён");
                    RemoveElementListBox(MainWindow.ListBoxIndex);
                }
                else
                {
                    MessageBox.Show("Ошибка при удалении");
                }
            }
        }
    }


    public class employee
    {
        //класс сотрудника
        public string F_name { get; set; }
        public string S_name { get; set; }
        public int Id_employee { get; set; }

        public string FullName
        {
            get { return F_name + " " + S_name; } 
        }
        
        public employee (string f_name, string s_name, int id_employee)
        {
            F_name = f_name;
            S_name = s_name;
            Id_employee = id_employee;
        }
    }

    public class all_employees
    {
        //главный класс программы
        //класс со всеми сотрудниками в массиве data
        //с помощью метода ReadEmployeeFromDB
        //получаем данные из БД и добавляем в data
        private employee[] data;

        public all_employees(string connString)
        {
            int count = GetNumbersOfEmployees(connString);
            data = new employee[count];
            ReadEmployeesFromDB(connString);
        }
        
        internal int GetNumbersOfEmployees(string connString)
        {
            //считает кол-во сотрудников в БД
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
        
        static internal int CreateNewEmployee(string connString, string emp_f_name, string emp_s_name)
        {
            //Метод добавления сотрудников в БД
            string sqlExpression = $"INSERT INTO employees (f_name, s_name) VALUES ('{emp_f_name}','{emp_s_name}');";
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
                string message = ex.Message + "Exception from AddNewEmployee";
                MessageBox.Show(message);
            }
            return NumberOfModified;
        }
        void ReadEmployeesFromDB(string connString)
        {
            //метод получения сотрудников из БД
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
                        int id_employee = reader.GetInt32(0);
                        string f_name = reader.GetString(1);
                        string s_name = reader.GetString(2);
                        employee emp = new employee(f_name, s_name, id_employee);
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
                string message = ex.Message + " Exception from reader";
                MessageBox.Show(message);
            }
        }

        static internal int DeleteEmployeeFromDB (string connectionString,int index)
        {
            string sqlExpression = $"DELETE FROM employees WHERE id = {index}";
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
        static internal int SearchEmployee(string FullName,employee[] data)
        {
            //костыльный метод для возвращения оригинального id из ListBox
            int id = 0;
            foreach (employee obj in data)
            {
                if (FullName == obj.FullName)
                {
                    id = obj.Id_employee;
                }
            }
            if (id == 0)
            {
                MessageBox.Show("id = 0 from SearchEmployee");
            }
            return id;

        }
        public employee GetEmployee(int i)
        {
            return data[i];
        }
        public employee GetLastEmployee()
        {
            return data.Last();
        }
        public employee[] GetArrayEmployeee()
        {
            return data;
        }
    }
 
}
