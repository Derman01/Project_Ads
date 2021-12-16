﻿using System.Windows.Controls;
using Project_Ads.Model;
using Project_Ads.MVVM.ViewModel;

namespace Project_Ads.MVVM.View
{
    public partial class ProfileView : UserControl
    {
        private User user = App.User;

        public ProfileView()
        {
            InitializeComponent();
        }

        private void RegistrationViewOpen(object sender, System.Windows.RoutedEventArgs e)
        {
            MainViewModel.RegistrationViewСommand.Execute();
        }
    }
}