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
    /// Логика взаимодействия для AcceptForm.xaml
    /// </summary>
    public partial class AcceptForm : Window
    {
        public AcceptForm()
        {
            InitializeComponent();
        }
        private void AcceptClickDelete (object sender, RoutedEventArgs e)
        {
            //Обработчик для вызова метода добавления сотрудника
            //index - выбранный элемент на MainWindow
            this.result = all_employees.DeleteEmployeeFromDB(MainWindow.connectionString, MainWindow.EmployeeIndex);
            this.DialogResult = true;
        }
        private int result = 0;
        public int DeleteResult
        {
            get { return result; }
            set { result = value; }
        }
    }
}
