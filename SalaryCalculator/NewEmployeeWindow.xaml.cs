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
    /// Логика взаимодействия для NewEmployeeWindow.xaml
    /// </summary>
    public partial class NewEmployeeWindow : Window
    {
        public NewEmployeeWindow()
        {
            InitializeComponent();
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            //Обработчик для вызова метода добавления сотрудника
            this.result = all_employees.CreateNewEmployee(MainWindow.connectionString, this.F_name, this.S_name);
            this.DialogResult = true;
        }
        public string F_name
        {
            get { return f_nameBox.Text; }
        }
        public string S_name
        {
            get { return s_nameBox.Text; }
        }
        private int result = 0;
        public int Result
        {
            get { return result; }
            set { result = value;  }
        }
    }
}
