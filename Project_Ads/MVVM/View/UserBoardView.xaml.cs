using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Project_Ads.MVVM.Model;

namespace Project_Ads.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для UserBoardView.xaml
    /// </summary>
    public partial class UserBoardView : UserControl
    {
        private ObservableCollection<Advertisement> _advertisements => App.GetAdvertisementListByUser;

        public UserBoardView()
        {
            InitializeComponent();
            advertisementsList.Items.Clear();
            advertisementsList.ItemsSource = _advertisements;
        }
        
        private void AdConfirm_Click(object sender, RoutedEventArgs e)
        {
            var advertisement = (Advertisement) advertisementsList.SelectedItem;
            App.EditAdvertisement(advertisement.RegNum, adLocation.Text, adMarks.Text, adDate.SelectedDate.Value, adColor.Text, adImg.Source.ToString());
            openAdvertisement.IsOpen = false;
        }

        private void AdClose_Click(object sender, RoutedEventArgs e)
        {
            openAdvertisement.IsOpen = false;
        }

        private void upload_new_img_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                adImg.Source = new BitmapImage(fileUri);
            }
        }

        private void AdvertisementsList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var advertisement = (Advertisement) advertisementsList.SelectedItem;
            adImg.Source = new BitmapImage(new Uri(advertisement.Animal.Pic));
            animalType.Text = advertisement.Animal.GetStringType;
            adType.Text = advertisement.GetStringTypeAdvertisement;
            adColor.Text = advertisement.Animal.Color;
            adLocation.Text = advertisement.Address;
            adDate.SelectedDate = advertisement.DateEvent;
            adMarks.Text = advertisement.Description;
            openAdvertisement.IsOpen = true;
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.OriginalSource; //определение родителя кнопки
            var advertisement = (Advertisement)btn.DataContext;
            App.DeleteAdvertisement(advertisement.RegNum);
            advertisementsList.Items.Clear();
            advertisementsList.ItemsSource = _advertisements;
        }
    }
}
