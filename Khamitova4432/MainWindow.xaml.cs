﻿using Khamitova4432.Windows;
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


namespace Khamitova4432
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void RegWindowBtn_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow rw = new RegistrationWindow();
            rw.Show();
            Close();
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (loginTb.Text == "0")
            {
                PatientWindow pw = new PatientWindow();
                pw.Show();
                Close();
            }
            else if (loginTb.Text == "1")
            {
                DoctorWindow dw = new DoctorWindow();
                dw.Show();
                Close();
            }
           
        }
    }
}
