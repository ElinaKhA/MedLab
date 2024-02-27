using Khamitova4432.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Khamitova4432.DataBase;


namespace Khamitova4432
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        medicdbContext _con = new medicdbContext();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void RegWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow rw = new RegistrationWindow(0);
            rw.Show();
            Close();
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_con.Patients.Any(u => u.Email == loginTb.Text && u.Password == passTb.Text))
                {
                    LogInUser(1);
                }
                else if (_con.Doctors.Any(u => u.Email == loginTb.Text && u.Password == passTb.Text))
                {
                    LogInUser(2);
                }
                else
                {
                    MessageBox.Show("Такого пользователя не существует");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
        private void LogInUser(int roleId)
        {
            try
            {
                if (roleId == 1)
                {
                    var SelectedUser = _con.Patients.FirstOrDefault(u => u.Email == loginTb.Text && u.Password == passTb.Text);
                    PatientWindow pw = new PatientWindow(SelectedUser);
                    pw.Show();
                    Close();
                }
                else if (roleId == 2)
                {
                    var SelectedDoct = _con.Doctors.FirstOrDefault(u => u.Email == loginTb.Text && u.Password == passTb.Text);
                    DoctorWindow dw = new DoctorWindow(SelectedDoct);
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
