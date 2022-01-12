using System;
using System.Windows;
using System.Windows.Controls;
using Project_Ads.MVVM.ViewModel;

namespace Project_Ads.MVVM.View
{
    public partial class ProfileView : UserControl
    {

        public ProfileView()
        {
            InitializeComponent();
        }
        

        private void RegistrationViewOpen(object sender, System.Windows.RoutedEventArgs e)
        {
            MainViewModel.RegistrationViewСommand.Execute();
        }

        private void Authorize(object sender, RoutedEventArgs e)
        {
            try
            {
                App.Authorization(Login.Text, Password.Text);
                Message.Text = "Вы авторизованы!";
                Message.Visibility = Visibility.Visible;
            }
            catch (Exception exception)
            {
                Message.Text = "Пользователь не найден!";
                Message.Visibility = Visibility.Visible;
            }
            
        }
    }
}