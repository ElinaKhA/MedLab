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
    /// Логика взаимодействия для PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        public PatientWindow()
        {
            InitializeComponent();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }

        private void RiskWinBtn_Click(object sender, RoutedEventArgs e)
        {
            RiscCalculateWindow rw = new RiscCalculateWindow(0, 1);
            rw.Show();
            Close();
        }

        private void AppointmentWinBtn_Click(object sender, RoutedEventArgs e)
        {
            AppointmentWindow aw = new AppointmentWindow(0,1);
            aw.Show();
            Close();
        }

        private void LabResultsWinBtn_Click(object sender, RoutedEventArgs e)
        {
            LabResultsWindow lw = new LabResultsWindow();
            lw.Show();
            Close();
        }

        private void TreatmentPlanWinBtn_Click(object sender, RoutedEventArgs e)
        {
            TreatmentPlanWindow tw = new TreatmentPlanWindow();
            tw.Show();
            Close();
        }

        private void ChangePatientWinBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
