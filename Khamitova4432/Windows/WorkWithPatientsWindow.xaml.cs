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
    /// Логика взаимодействия для WorkWithPatientsWindow.xaml
    /// </summary>
    public partial class WorkWithPatientsWindow : Window
    {
        Doctor doct;
        public WorkWithPatientsWindow(Doctor doctor)
        {
            InitializeComponent();
            doct = doctor;
            try
            {
                using (var context = new medicdbContext())
                {
                    var patients = context.Patients.ToList(); 
                    patientsdg.ItemsSource = patients;
                }
            }
            catch { MessageBox.Show("Ошибка"); }
        }

        private void RegPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow rw = new RegistrationWindow(doct.Id);
            rw.Show();
            Close();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            DoctorWindow dw = new DoctorWindow(doct);
            dw.Show();
            Close();
        }

        private void patientsdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Patient selectedPatient = (Patient)patientsdg.SelectedItem;
                if (selectedPatient != null)
                {
                    PatientProfileForDoctorWindow pw = new PatientProfileForDoctorWindow(selectedPatient, doct);
                    pw.Show();
                    Close();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void findpatienttb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = findpatienttb.Text.ToLower(); 
            using (var context = new medicdbContext())
            {
                var patients = context.Patients.Where(p =>
                    p.Surname.ToLower().Contains(searchText) || 
                    p.Name.ToLower().Contains(searchText) ||    
                    p.LastName.ToLower().Contains(searchText)   
                ).ToList();
                patientsdg.ItemsSource = patients; 
            }
        }
    }
}
