using Khamitova4432.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        int roleid;
        medicdbContext _con = new medicdbContext();
        public RegistrationWindow(int role)
        {
            InitializeComponent();
            roleid = role;
            gcb.Items.Add("Женщина");
            gcb.Items.Add("Мужчина");
        }

        private void AuthorizationWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int genid = 0;
                if (gcb.SelectedIndex == 0)
                {
                    genid = 1;
                }
                else if (gcb.SelectedIndex == 1)
                {
                    genid = 2;
                }

                var login = etb.Text;

                if (_con.Patients.Any(p => p.Email == login) || _con.Doctors.Any(d => d.Email == login))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует.");
                    return;
                }

                var newPatient = new Patient
                {
                    Id = _con.Patients.Max(r => r.Id) + 1,
                    Surname = ftb.Text,
                    Name = ntb.Text,
                    LastName = ltb.Text,
                    Email = login,
                    Password = ptb.Text,
                    BirthDate = Convert.ToDateTime(dtb.Text),
                    GenderId = genid
                };
                _con.Patients.Add(newPatient);
                _con.SaveChanges();

                MessageBox.Show("Пациент зарегистрирован");
                if (roleid == 0)
                {
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    Close();
                }
                else
                {
                    var selectedDoctor = _con.Doctors.FirstOrDefault(d => d.Id == roleid);
                    WorkWithPatientsWindow dw = new WorkWithPatientsWindow(selectedDoctor);
                    dw.Show();
                    Close();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}
