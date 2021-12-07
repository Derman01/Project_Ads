﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Project_Ads.MVVM.ViewModel;

namespace Project_Ads
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void Close(object s, RoutedEventArgs e) => Close();

        private void Minimaze(object s, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
