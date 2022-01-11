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
    }
}