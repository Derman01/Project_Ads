using System;
using System.Collections.Generic;
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
using Project_Ads.MVVM.ViewModel;

namespace Project_Ads.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для UserBoardView.xaml
    /// </summary>
    public partial class UserBoardView : UserControl
    {
        private List<Dictionary<string, object>> _advertisementsDictionary;

        private ObservableCollection<AdvertisementData> _userAdvertisements =
            new ObservableCollection<AdvertisementData>();

        public UserBoardView()
        {
            InitializeComponent();
            advertisementsList.Items.Clear();
            advertisementsList.ItemsSource = _userAdvertisements;
            
            Loaded += (sender, args) => UpdateListAdvertisement();
        }
        
        private void UpdateListAdvertisement()
        {
            if (_userAdvertisements.Count != 0) _userAdvertisements.Clear();
            _advertisementsDictionary = App.GetAdvertisementListByUser;
            foreach (var item in _advertisementsDictionary)
            {
                var adv = new AdvertisementData(
                    Convert.ToInt32(item["regNum"]),
                    Convert.ToInt32(item["advType"]),
                    item["address"].ToString(),
                    item["description"].ToString(),
                    Convert.ToDateTime(item["dateEvent"]),
                    Convert.ToDateTime(item["dateCreate"]),
                    Convert.ToInt32(item["anNum"]),
                    item["pic"].ToString(),
                    Convert.ToInt32(item["anType"]),
                    item["anColor"].ToString(),
                    item["phone"].ToString()
                );
                _userAdvertisements.Add(adv);
            }
        }

        private void AdConfirm_Click(object sender, RoutedEventArgs e)
        {
            var advertisement = (AdvertisementData) advertisementsList.SelectedItem;
            App.EditAdvertisement(
                advertisement.RegNum, adLocation.Text, adMarks.Text, adDate.SelectedDate.Value,
                adColor.Text, adImg.Source.ToString());
            openAdvertisement.IsOpen = false;
            UpdateListAdvertisement();
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
            var advertisement = (AdvertisementData) advertisementsList.SelectedItem;
            adImg.Source = new BitmapImage(new Uri(advertisement.AnPic));
            animalType.Text = advertisement.GetStringTypeAnimal;
            adType.Text = advertisement.GetStringTypeAdvertisement;
            adColor.Text = advertisement.AnColor;
            adLocation.Text = advertisement.Address;
            adDate.SelectedDate = advertisement.DateEvent;
            adMarks.Text = advertisement.Description;
            openAdvertisement.IsOpen = true;
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var advertisement = (AdvertisementData) advertisementsList.SelectedItem;
            App.DeleteAdvertisement(advertisement.RegNum);
            openAdvertisement.IsOpen = false;
            UpdateListAdvertisement();
        }
    }
}
