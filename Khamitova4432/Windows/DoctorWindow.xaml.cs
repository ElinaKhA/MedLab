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
    /// Логика взаимодействия для DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        Doctor doct;
        int dId;
        medicdbContext _con = new medicdbContext();
        public DoctorWindow(Doctor doctor)
        {
            InitializeComponent();
            doct = doctor;
            dId = doctor.Id;
            fiolb.Content = $"{doctor.Surname} {doctor.Name} {doctor.LastName}";
            appointmentsdg.Visibility = Visibility.Hidden;
            try
            {
                using (var context = new medicdbContext()) 
                {
                    DateTime currentDateTime = DateTime.Now;
                    var query = from appointment in _con.Appointments
                                join freeAppointment in _con.FreeAppointments on appointment.FreeAppointmentId equals freeAppointment.Id
                                join patient in _con.Patients on appointment.PatientId equals patient.Id
                                where appointment.StatusId == 1 && freeAppointment.DoctorId == dId
                                select new
                                {
                                    Date = freeAppointment.DateTimeOfAppointment,
                                    PatientName = patient.Surname
                                };

                    var nextAppointment = query.FirstOrDefault();
                    if (nextAppointment != null)
                    {
                        nextapplb.Content = $"Предстоящий прием: {nextAppointment.Date.ToShortDateString()} в {nextAppointment.Date.TimeOfDay} пациент: {nextAppointment.PatientName}";
                    }
                    else
                    {
                        nextapplb.Content = "Нет предстоящей записи";
                    }
                }
            }
            catch { MessageBox.Show("Ошибка"); }
        }

        private void WorkWithPatientsWinBtn_Click(object sender, RoutedEventArgs e)
        {
            WorkWithPatientsWindow pw = new WorkWithPatientsWindow(doct); 
            pw.Show();
            Close();
        }

        private void AppointmentsBtn_Click(object sender, RoutedEventArgs e)
        {
         
            var query = from appointment in _con.Appointments
                        join freeAppointment in _con.FreeAppointments on appointment.FreeAppointmentId equals freeAppointment.Id
                        join patient in _con.Patients on appointment.PatientId equals patient.Id
                        where appointment.StatusId == 1 && freeAppointment.DoctorId == dId
                        select new
                        {
                            Date = freeAppointment.DateTimeOfAppointment,
                            PatientName = patient.Surname + " " + patient.Name + " " + patient.LastName
                        };

            appointmentsdg.ItemsSource = query.ToList();
            appointmentsdg.Visibility = Visibility.Visible;
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }
    }
}
