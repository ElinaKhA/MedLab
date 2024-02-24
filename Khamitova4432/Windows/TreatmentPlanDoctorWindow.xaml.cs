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
    /// Логика взаимодействия для TreatmentPlanDoctorWindow.xaml
    /// </summary>
    public partial class TreatmentPlanDoctorWindow : Window
    {
        public TreatmentPlanDoctorWindow()
        {
            InitializeComponent();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Close();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            PatientProfileForDoctorWindow pw = new PatientProfileForDoctorWindow();
            pw.Show();
            Close();
        }

        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            //функция печати
        }

        private void EditPlanBtn_Click(object sender, RoutedEventArgs e)
        {
            //текстбокс становится открытым для редактирования, затем изменыяется текст, открывается кнопка сохранить 
        }
    }
}
