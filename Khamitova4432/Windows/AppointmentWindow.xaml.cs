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
    /// Логика взаимодействия для AppointmentWindow.xaml
    /// </summary>
    public partial class AppointmentWindow : Window
    {
        int identifr;
        public AppointmentWindow(int idrole, int id)
        {
            InitializeComponent();
            identifr = idrole;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (identifr == 0)
            {
                PatientWindow pw = new PatientWindow();
                pw.Show();
                Close();
            }
            else
            {
                PatientProfileForDoctorWindow dw = new PatientProfileForDoctorWindow();
                dw.Show();
                Close();
            }
           
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Успешно");
            PatientWindow pw = new PatientWindow();
            pw.Show();
            Close();
        }
    }
}
