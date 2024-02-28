﻿using Khamitova4432.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
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
        Patient selectedPatient;
        medicdbContext _con = new medicdbContext();

        public RiscCalculateWindow(int idrole, Patient patient)
        {
            InitializeComponent();
            identifr= idrole;
            selectedPatient = patient;
            pscvrmCb.Items.Add("Первичный");
            pscvrmCb.Items.Add("Вторичный");
            hypertensionCb.Items.Add("Нет");
            hypertensionCb.Items.Add("Да");
            smokingCb.Items.Add("Никогда не курил");
            smokingCb.Items.Add("Раньше курил");
            smokingCb.Items.Add("Да");
            try
            {
                var lastLabResult = _con.LabResults.Where(lr => lr.PatientId == selectedPatient.Id).OrderByDescending(lr => lr.DateOfResults).FirstOrDefault();
                if (lastLabResult != null)
                {
                    mdrdTb.Text = lastLabResult.SKF.ToString();
                    glucoseTb.Text = lastLabResult.Glucose.ToString();
                    holesterolTb.Text = lastLabResult.Cholesterol.ToString();
                    sistprTb.Text = lastLabResult.SystolicBloodPressure.ToString();
                    diastprTb.Text = lastLabResult.DiastolicBloodPressure.ToString();
                    bmiTb.Text = lastLabResult.BMI.ToString();

                    hypertensionCb.SelectedItem = lastLabResult.Hypertension == 1 ? "Да" : "Нет";
                    switch (lastLabResult.Smoking)
                    {
                        case 0:
                            smokingCb.SelectedItem = "Никогда не курил";
                            break;
                        case 1:
                            smokingCb.SelectedItem = "Раньше курил";
                            break;
                        case 2:
                            smokingCb.SelectedItem = "Да";
                            break;
                        default:
                            smokingCb.SelectedItem = null;
                            break;
                    }
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

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (identifr==0)
            {
                PatientWindow pw = new PatientWindow(selectedPatient);
                pw.Show();
                Close();
            }
            else
            {
                var selectedDoctor = _con.Doctors.FirstOrDefault(d => d.Id == identifr);
                PatientProfileForDoctorWindow dw = new PatientProfileForDoctorWindow(selectedPatient, selectedDoctor);
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
                    try
                    {
                        var newRisk = new Risk
                        {
                            Id = _con.Risks.Max(r => r.Id)+1,
                            PatientId = selectedPatient.Id, 
                            CalculatedRisk = Convert.ToInt32(result.Score), 
                            DateOfCalculated = DateTime.Now
                        };
                        _con.Risks.Add(newRisk);
                        _con.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка при занесении риска в БД");
                    }
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
                PatientWindow pw = new PatientWindow(selectedPatient);
                pw.Show();
                Close();
            }
            else
            {
                var selectedDoctor = _con.Doctors.FirstOrDefault(d => d.Id == identifr);
                PatientProfileForDoctorWindow dw = new PatientProfileForDoctorWindow(selectedPatient, selectedDoctor);
                dw.Show();
                Close();
            }
        }
    }
}
