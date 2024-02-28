using Khamitova4432.DataBase;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для TreatmentPlanDoctorWindow.xaml
    /// </summary>
    public partial class TreatmentPlanDoctorWindow : Window
    {
        Doctor docw;
        Patient patw;
        medicdbContext _con = new medicdbContext();
        public TreatmentPlanDoctorWindow(Doctor doctor, Patient patient)
        {
            InitializeComponent();
            try
            {
                docw = doctor;
                patw = patient;
                fiolb.Content = $"{patient.Surname} {patient.Name} {patient.LastName}";
                var patientfl = _con.Patients.SingleOrDefault(p => p.Id == patient.Id);
                var lastRisk = patientfl.Risks.OrderByDescending(r => r.DateOfCalculated).FirstOrDefault();
                if (lastRisk != null)
                {
                    rlb.Content = $"Риск: {lastRisk.CalculatedRisk}";
                }
                else
                {
                    rlb.Content = "Риск не вычислен";
                }
            }
            catch { MessageBox.Show("Ошибка"); }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            PatientProfileForDoctorWindow pw = new PatientProfileForDoctorWindow(patw, docw);
            pw.Show();
            Close();
        }

        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            //функция печати
        }

        private void EditPlanBtn_Click(object sender, RoutedEventArgs e)
        {
            //текстбокс становится открытым для редактирования, затем изменяется текст, открывается кнопка сохранить 
        }
    }
}
