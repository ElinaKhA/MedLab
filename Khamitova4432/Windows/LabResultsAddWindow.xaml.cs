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
    /// Логика взаимодействия для LabResultsAddWindow.xaml
    /// </summary>
    public partial class LabResultsAddWindow : Window
    {
        Doctor docw;
        Patient patw;
        medicdbContext _con = new medicdbContext();
        public LabResultsAddWindow(Doctor doctor, Patient patient)
        {
            InitializeComponent();
            docw = doctor;
            patw = patient;
            pscvrmCb.Items.Add("Первичный");
            pscvrmCb.Items.Add("Вторичный");
            hypertensionCb.Items.Add("Нет");
            hypertensionCb.Items.Add("Да");
            smokingCb.Items.Add("Никогда не курил");
            smokingCb.Items.Add("Раньше курил");
            smokingCb.Items.Add("Да");
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            PatientProfileForDoctorWindow pw = new PatientProfileForDoctorWindow(patw, docw);
            pw.Show();
            Close();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            int smoke = 0;
            int hyper = 0;
            try
            {
                if (smokingCb.SelectedItem.ToString() != "" && hypertensionCb.SelectedItem.ToString() != "" && pscvrmCb.SelectedItem.ToString() != ""
                    && mdrdTb.Text != "" && glucoseTb.Text != "" && holesterolTb.Text != "" && sistprTb.Text != "" && diastprTb.Text != ""
                    && bmiTb.Text != "")
                {
                    if (smokingCb.SelectedIndex == 0)
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

                    var newLabResult = new LabResult
                    {
                        Id = _con.LabResults.Max(r => r.Id) + 1,
                        PatientId = patw.Id,
                        DateOfResults = DateTime.Now,
                        SKF = Convert.ToSingle(mdrdTb.Text),
                        Glucose = Convert.ToSingle(glucoseTb.Text),
                        SystolicBloodPressure = Convert.ToSingle(sistprTb.Text),
                        DiastolicBloodPressure = Convert.ToSingle(diastprTb.Text),
                        BMI = Convert.ToSingle(bmiTb.Text),
                        Hypertension = hyper,
                        Smoking = smoke,
                        Cholesterol = Convert.ToSingle(holesterolTb.Text)
                    };
                    _con.LabResults.Add(newLabResult);
                    _con.SaveChanges();
                    MessageBox.Show("Анализы успешно добавлены");
                    PatientProfileForDoctorWindow pw = new PatientProfileForDoctorWindow(patw, docw);
                    pw.Show();
                    Close();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }
    }
}
