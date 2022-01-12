using System;
using System.Windows;
using System.Windows.Controls;
using Project_Ads.MVVM.ViewModel;

namespace Project_Ads.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : UserControl
    {
        public RegistrationView()
        {
            InitializeComponent();
        }

        private void OnRegisterUser(object sender, RoutedEventArgs e)
        {
            try
            {
                App.Registration(Login.Text, Password.Text, UserName.Text, Phone.Text);
                MainViewModel.HomeViewСommand.Execute(sender);
                LoginExists.Visibility = Visibility.Hidden;
            }
            catch(Exception exp)
            {
                LoginExists.Visibility = Visibility.Visible;
            }
        }

        private void ProfileViewOpen(object sender, RoutedEventArgs e)
        {
            MainViewModel.ProfileViewСommand.Execute(sender);
        }
    }
}
