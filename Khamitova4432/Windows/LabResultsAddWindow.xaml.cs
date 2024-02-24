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

namespace Khamitova4432.Windows
{
    /// <summary>
    /// Логика взаимодействия для LabResultsAddWindow.xaml
    /// </summary>
    public partial class LabResultsAddWindow : Window
    {
        public LabResultsAddWindow()
        {
            InitializeComponent();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            PatientProfileForDoctorWindow pw = new PatientProfileForDoctorWindow();
            pw.Show();
            Close();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }
    }
}
