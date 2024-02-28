using Khamitova4432.DataBase;
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
using System.Xml.Linq;

namespace Khamitova4432.Windows
{
    /// <summary>
    /// Логика взаимодействия для PatientWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {
        int idP = 0;
        medicdbContext _con = new medicdbContext();
        Patient patw;
        public PatientWindow(Patient patient)
        {
            InitializeComponent();
            fiolb.Content = $"{patient.Surname} {patient.Name} {patient.LastName}";
            idP = patient.Id;
            var lastRisk = patient.Risks.OrderByDescending(r => r.DateOfCalculated).FirstOrDefault();
            if (lastRisk != null)
            {
                risklb.Content = $"Риск: {lastRisk.CalculatedRisk}";
            }
            else
            {
                risklb.Content = "Риск не вычислен";
            }
            patw = patient;
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }

        private void RiskWinBtn_Click(object sender, RoutedEventArgs e)
        {
            RiscCalculateWindow rw = new RiscCalculateWindow(0, patw);
            rw.Show();
            Close();
        }

        private void AppointmentWinBtn_Click(object sender, RoutedEventArgs e)
        {
            AppointmentWindow aw = new AppointmentWindow(0,patw);
            aw.Show();
            Close();
        }

        private void LabResultsWinBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedPatient = _con.Patients.FirstOrDefault(u => u.Id == idP);
            LabResultsWindow lw = new LabResultsWindow(selectedPatient);
            lw.Show();
            Close();
        }

        private void TreatmentPlanWinBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedPatient = _con.Patients.FirstOrDefault(u => u.Id == idP);
            TreatmentPlanWindow tw = new TreatmentPlanWindow(selectedPatient);
            tw.Show();
            Close();
        }

        private void ChangePatientWinBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
