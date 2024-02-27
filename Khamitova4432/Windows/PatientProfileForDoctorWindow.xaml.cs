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
    /// Логика взаимодействия для PatientProfileForDoctorWindow.xaml
    /// </summary>
    public partial class PatientProfileForDoctorWindow : Window
    {
        public PatientProfileForDoctorWindow()
        {
            InitializeComponent();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            //WorkWithPatientsWindow pw = new WorkWithPatientsWindow();
            //pw.Show();
            //Close();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }

        private void RiskWinBtn_Click(object sender, RoutedEventArgs e)
        {
            RiscCalculateWindow rw = new RiscCalculateWindow(1, 1);
            rw.Show();
            Close();
        }

        private void TreatPlanWinBtn_Click(object sender, RoutedEventArgs e)
        {
            TreatmentPlanDoctorWindow tw = new TreatmentPlanDoctorWindow();
            tw.Show();
            Close();
        }

        private void AddLabResultsWinBtn_Click(object sender, RoutedEventArgs e)
        {
            LabResultsAddWindow lw = new LabResultsAddWindow();
            lw.Show();
            Close();
        }

        private void AppointmentWinBtn_Click(object sender, RoutedEventArgs e)
        {
            AppointmentWindow aw = new AppointmentWindow(1,1);
            aw.Show();
            Close();
        }
    }
}
