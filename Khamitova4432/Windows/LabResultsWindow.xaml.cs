using Khamitova4432.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для LabResultsWindow.xaml
    /// </summary>
    public partial class LabResultsWindow : Window
    {
        Patient pforwin;
        public LabResultsWindow(Patient patient)
        {
            InitializeComponent();
            pforwin = patient;
            fiolb.Content = $"{patient.Surname} {patient.Name} {patient.LastName}";
            var lastRisk = patient.Risks.OrderByDescending(r => r.DateOfCalculated).FirstOrDefault();
            if (lastRisk != null)
            {
                risklb.Content = $"Риск: {lastRisk.CalculatedRisk}";
            }
            else
            {
                risklb.Content = "Риск не вычислен";
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            PatientWindow pw = new PatientWindow(pforwin);
            pw.Show();
            Close();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }
    }
}
