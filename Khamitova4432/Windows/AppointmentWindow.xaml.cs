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
    /// Логика взаимодействия для AppointmentWindow.xaml
    /// </summary>
    public partial class AppointmentWindow : Window
    {
        int identifr;
        Patient patw;
        medicdbContext _con = new medicdbContext();
        public AppointmentWindow(int idrole, Patient patient)
        {
            InitializeComponent();
            try
            {
                identifr = idrole;
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
                var query = from fa in _con.FreeAppointments
                            join d in _con.Doctors on fa.DoctorId equals d.Id
                            join a in _con.Appointments on fa.Id equals a.FreeAppointmentId into appointments
                            from appointment in appointments.DefaultIfEmpty()
                            where appointment == null
                            select new
                            {
                                FreeAppointmentId = fa.Id,
                                DateTimeOfAppointment = fa.DateTimeOfAppointment,
                                DoctorFullName = d.Surname + " " + d.Name + " " + d.LastName
                            };
                freeappdg.ItemsSource = query.ToList();
            }
            catch { MessageBox.Show("Ошибка"); }
          
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (identifr == 0)
            {
                PatientWindow pw = new PatientWindow(patw);
                pw.Show();
                Close();
            }
            else
            {
                var selectedDoctor = _con.Doctors.FirstOrDefault(d=>d.Id == identifr);
                PatientProfileForDoctorWindow dw = new PatientProfileForDoctorWindow(patw, selectedDoctor);
                dw.Show();
                Close();
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dynamic selectedAppointment = freeappdg.SelectedItem;
                if (selectedAppointment != null)
                {
                    Appointment newAppointment = new Appointment
                    {
                        FreeAppointmentId = selectedAppointment.FreeAppointmentId,
                        Id = _con.Appointments.Max(r => r.Id) + 1,
                        PatientId = patw.Id,
                        StatusId = 1
                    };

                    _con.Appointments.Add(newAppointment);
                    _con.SaveChanges();
                    MessageBox.Show("Вы успешно записались на прием");
                    if (identifr == 0)
                    {
                        PatientWindow pw = new PatientWindow(patw);
                        pw.Show();
                        Close();
                    }
                    else
                    {
                        var selectedDoctor = _con.Doctors.FirstOrDefault(d => d.Id == identifr);
                        PatientProfileForDoctorWindow dw = new PatientProfileForDoctorWindow(patw, selectedDoctor);
                        dw.Show();
                        Close();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}
