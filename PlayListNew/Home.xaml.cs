﻿using PlaylistNew.View;
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

namespace PlayListNew
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }

        private void loginClicked(object sender, RoutedEventArgs e)
        {
            login loginS = new login();
            loginS.Show();
        }

        private void registerClicked(object sender, RoutedEventArgs e)
        {
            register registerS = new register();
            registerS.Show();
        }

    }
}
