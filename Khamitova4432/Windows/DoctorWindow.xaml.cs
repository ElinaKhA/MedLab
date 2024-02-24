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
    /// Логика взаимодействия для DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        public DoctorWindow()
        {
            InitializeComponent();
        }

        private void WorkWithPatientsWinBtn_Click(object sender, RoutedEventArgs e)
        {
            WorkWithPatientsWindow pw = new WorkWithPatientsWindow(); 
            pw.Show();
            Close();
        }

        private void AppointmentsBtn_Click(object sender, RoutedEventArgs e)
        {
            //тут откроется грид со списком записей
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }
    }
}
