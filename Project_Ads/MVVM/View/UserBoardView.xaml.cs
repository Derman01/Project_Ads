using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Project_Ads.MVVM.Model;

namespace Project_Ads.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для UserBoardView.xaml
    /// </summary>
    public partial class UserBoardView : UserControl
    {
        private ObservableCollection<Advertisement> _advertisements { get; set; }

        public UserBoardView()
        {
            InitializeComponent();
        }
        
        private void AdConfirm_Click(object sender, RoutedEventArgs e)
        {
            //запись в бд об изменениях
            openAdvertisement.IsOpen = false;
        }

        private void AdClose_Click(object sender, RoutedEventArgs e)
        {
            openAdvertisement.IsOpen = false;
        }

        private void upload_new_img_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AdvertisementsList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            openAdvertisement.IsOpen = true;
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.OriginalSource; //определение родителя кнопки
            var data = (Advertisement)btn.DataContext; 
            MessageBox.Show($"{data.RegNum}");
        }
    }
}
