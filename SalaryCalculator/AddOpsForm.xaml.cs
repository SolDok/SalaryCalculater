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
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace SalaryCalculator
{
    /// <summary>
    /// Логика взаимодействия для AddOpsForm.xaml
    /// </summary>
    public partial class AddOpsForm : Window
    {
        OperationModel op;
        public AddOpsForm()
        {
            InitializeComponent();
            op = new OperationModel(0,"","",0,0,0);
            this.DataContext = op;
        }

        int result = 0;
        public string op_title { get; set; }
        public string op_date { get; set; }
        public double op_hours { get; set; }
        public double op_rate { get; set; }
        public double op_sum { get; set; }
        private void AcceptClickDelete(object sender, RoutedEventArgs e)
        {
            Operations.InsertEmployeeOperation(MainWindow.connectionString, op_title, op_date, op_hours, op_rate, op_sum, MainWindow.EmployeeIndex);
            this.DialogResult = true;
        }
        private void TextBox_OpRate_KeyDown(object sender, MouseButtonEventArgs e)
        {
            float rate;
            float hours;
            //устанавливаем форматтер для исключения культурных различий для записи вещественного типа
            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            bool success = float.TryParse(TextBox_OpRate.Text,NumberStyles.Any, formatter, out rate) 
                 & float.TryParse(TextBox_OpHours.Text, NumberStyles.Any, formatter, out hours);
            if (success)
            {
                double sum = Math.Round(rate * hours, 2);
                op_title = TextBox_OpTitle.Text;
                op_date = TextBox_OpDate.Text;
                op_rate = Math.Round(rate,2);
                op_hours = Math.Round(hours,2);
                op_sum = sum;
                TextBox_OpSumm.Text = sum.ToString(formatter);
            } else
            {
                MessageBox.Show("Не удалось вычислить сумму");
            }
            
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Owner.Show();
        }
        private void DateTextBox_Error(object sender, ValidationErrorEventArgs e)
        {
            //Лютейший костыль
            //когда валидацию не проходит дата, кнопка становится не активна
            //когда проходит, то кнопка активна
            //Когда не проходит, метод вызывается два раза
            //когда проходит, то один раз
            if (OkButton.IsEnabled)
            {
                OkButton.IsEnabled = false;
            } else
            {
                OkButton.IsEnabled = true;
            }
        }
    }
    class OperationModel : Operation, IDataErrorInfo
    {
        //класс, служащий для валидации формы
        public OperationModel(int Op_id, string Op_title, string Op_date, double Op_time, double Op_rate, double Op_sum) : base(Op_id,  Op_title,  Op_date,  Op_time,  Op_rate, Op_sum) { }

        DateTime Result;
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "op_date":
                        if (DateTime.TryParseExact(op_date, "dd.mm.yyyy", null, DateTimeStyles.None,out Result) == false)
                        {
                            error = "Неверный формат даты";
                        }
                            break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
