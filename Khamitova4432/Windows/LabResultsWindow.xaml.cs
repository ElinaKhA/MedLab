using Khamitova4432.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
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
        medicdbContext _con = new medicdbContext();
        public LabResultsWindow(Patient patient)
        {
            InitializeComponent();
            try
            {
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
                var lastLabResult = _con.LabResults
                        .Where(lr => lr.PatientId == patient.Id)
                        .OrderByDescending(lr => lr.DateOfResults)
                        .FirstOrDefault();
                if (lastLabResult != null)
                {
                    mdrdlb.Content = lastLabResult.SKF;
                    glucoselb.Content = lastLabResult.Glucose;
                    sistlb.Content = lastLabResult.SystolicBloodPressure;
                    diastlb.Content = lastLabResult.DiastolicBloodPressure;
                    bmilb.Content = lastLabResult.BMI;
                    holesterollb.Content = lastLabResult.Cholesterol;
                    if (lastLabResult.Smoking == 0)
                    {
                        smolb.Content = "Никогда не курил";
                    }
                    else if (lastLabResult.Smoking == 1)
                    {
                        smolb.Content = "Раньше курил";
                    }       
                    else if (lastLabResult.Smoking == 2)
                    {
                        smolb.Content = "Да";
                    }
                    if (lastLabResult.Hypertension == 0)
                    {
                        hyperlb.Content = "Нет";
                    } 
                    else if (lastLabResult.Hypertension == 1)
                    {
                        hyperlb.Content = "Да";
                    }
                }
            }
            catch { MessageBox.Show("Ошибка"); }
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
