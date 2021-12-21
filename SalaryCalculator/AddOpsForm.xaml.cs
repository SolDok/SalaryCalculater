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
    /// Логика взаимодействия для AddOpsForm.xaml
    /// </summary>
    public partial class AddOpsForm : Window
    {
        public AddOpsForm()
        {
            InitializeComponent();
        }

        int result = 0;
        public string op_Title
        {
            get { return TextBox_OpTitle.Text; }
        }
        public string op_Date
        {
            get { return TextBox_OpDate.Text; }
        }
        public double op_Hours
        {
            get { return double.Parse(TextBox_OpHours.Text); }
        }
        public double op_Rate
        {
            get { return double.Parse(TextBox_OpRate.Text); }
        }
        public double op_Summ
        {
            get { return double.Parse(TextBox_OpSumm.Text); }
        }
        public int Result { get { return result; } }
        private void AcceptClickDelete(object sender, RoutedEventArgs e)
        {
            //Проблема с вещественными, Parse() пропускает только через запятую, в этом случае в sqlExperssion возникает баг
            this.result = Operations.InsertEmployeeOperation(MainWindow.connectionString, op_Title, op_Date, op_Hours, op_Rate, op_Summ, MainWindow.EmployeeIndex);
            this.DialogResult = true;
        }
    }
}
