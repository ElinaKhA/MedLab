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

namespace Khamitova4432.Windows
{
    /// <summary>
    /// Логика взаимодействия для RiscCalculateWindow.xaml
    /// </summary>
    public partial class RiscCalculateWindow : Window
    {
        int identifr;
        int idP = 0;
        medicdbContext _con = new medicdbContext();

        public RiscCalculateWindow(int idrole, int id)
        {
            InitializeComponent();
            identifr= idrole;
            idP = id;
            pscvrmCb.Items.Add("Первичный");
            pscvrmCb.Items.Add("Вторичный");
            hypertensionCb.Items.Add("Нет");
            hypertensionCb.Items.Add("Да");
            smokingCb.Items.Add("Нет");
            smokingCb.Items.Add("Раньше курил");
            smokingCb.Items.Add("Да");
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (identifr==0)
            {
                //PatientWindow pw = new PatientWindow();
                //pw.Show();
                //Close();
            }
            else
            {
                PatientProfileForDoctorWindow dw = new PatientProfileForDoctorWindow();
                dw.Show();
                Close();
            }
        }

        private void CalculateBtn_Click(object sender, RoutedEventArgs e)
        {
            float smoke=0;
            float hyper=0;
            float pscvrm=0;
            try
            {

                if (smokingCb.SelectedItem.ToString()!="" && hypertensionCb.SelectedItem.ToString() != "" && pscvrmCb.SelectedItem.ToString()!=""
                    && mdrdTb.Text!="" && glucoseTb.Text != "" && holesterolTb.Text != "" && sistprTb.Text != "" && diastprTb.Text != ""
                    && bmiTb.Text != "")
                {
                    if (smokingCb.SelectedIndex==0)
                    {
                        smoke = 0;
                    }
                    else if (smokingCb.SelectedIndex == 1)
                    {
                        smoke = 1;
                    }
                    else if (smokingCb.SelectedIndex == 2)
                    {
                        smoke = 2;
                    }

                    if (hypertensionCb.SelectedIndex == 0)
                    {
                        hyper = 0;
                    }
                    else if (hypertensionCb.SelectedIndex == 1)
                    {
                        hyper = 1;
                    }
                    if (pscvrmCb.SelectedIndex == 0)
                    {
                        pscvrm = 0;
                    }
                    else if (pscvrmCb.SelectedIndex == 1)
                    {
                        pscvrm = 1;
                    }
                    var selectedPatient = _con.Patients.FirstOrDefault(u => u.Id == idP);
                    DateTime today = DateTime.Today;
                    int ageP = today.Year - selectedPatient.BirthDate.Year;
                    if (selectedPatient.BirthDate.AddYears(ageP) > today)
                    {
                        ageP--;
                    }
                    float gen=0;
                    if (selectedPatient.GenderId == 1)
                    {
                        gen = 0;
                    }
                    else if (selectedPatient.GenderId == 2)
                    {
                        gen = 1;
                    }

                    var sampleData = new MLModel1.ModelInput()
                    {
                        MDRD = Convert.ToSingle(mdrdTb.Text),
                        Glucose_Fasting = Convert.ToSingle(glucoseTb.Text),
                        Total_Cholesterol = Convert.ToSingle(holesterolTb.Text),
                        Systolic_Blood_Pressure = Convert.ToSingle(sistprTb.Text),
                        Diastolic_Blood_Pressure = Convert.ToSingle(diastprTb.Text),
                        BMI = Convert.ToSingle(bmiTb.Text),
                        _1___Secondary_CVRM = pscvrm,
                        Smoking_Status = smoke,
                        Patient_Gender = gen,
                        Age = ageP,
                        Hypertension = hyper
                    };
                    var result = MLModel1.Predict(sampleData);
                    MessageBox.Show($"Риск: {result.Score}");
                }
                else
                {
                    MessageBox.Show("Заполните все поля");
                }

            }
            catch
            {
                MessageBox.Show("Ошибка");
            }

            if (identifr == 0)
            {
                //PatientWindow pw = new PatientWindow();
                //pw.Show();
                //Close();
            }
            else
            {
                PatientProfileForDoctorWindow dw = new PatientProfileForDoctorWindow();
                dw.Show();
                Close();
            }
        }
    }
}
