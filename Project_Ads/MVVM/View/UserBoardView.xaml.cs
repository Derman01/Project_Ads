using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Project_Ads.Core;
using Project_Ads.Model;
using System.Collections.ObjectModel;

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

            _advertisements = new ObservableCollection<Advertisement>()
            {
                new Advertisement()
                {
                    RegNum = 1, ImageUrl =$@"{App.PATH}/Icons/dogs.png",
                    AdvertisementTypeAdvertisement = Advertisement.AdvertisementType.Find,
                    TypesAnimal = Animal.Type.Dog,
                    Marks= "Срочно !!!! нашли щеночка После 21:00 некуда деть." +
                                                  "\nХозяева отзовитесь Голубой ошейник. После 21:00 некуда деть",
                    Description = "Черный",
                    Phone = "8 999 586 1516",
                    DateEvent = DateTime.Now,
                    Address = "3 мкр 35 дом"
                },
                new Advertisement()
                {
                    RegNum = 2, ImageUrl = $@"{App.PATH}/Icons/cat.jpg",
                    AdvertisementTypeAdvertisement = Advertisement.AdvertisementType.Lose,
                    TypesAnimal = Animal.Type.Cat, Marks = App.PATH,
                    Phone = "8 800 555 3535", 
                    Description = "Голубой окрас",
                    DateEvent = DateTime.Today,
                    Address = App.PATH
                },
                new Advertisement()
                {
                    RegNum = 3, ImageUrl = $@"{App.PATH}/Icons/cat.jpg",
                    AdvertisementTypeAdvertisement = Advertisement.AdvertisementType.Lose,
                    TypesAnimal = Animal.Type.Cat, Marks = App.PATH,
                    Phone = "8 800 555 3535",
                    Description = "Голубой окрас",
                    DateEvent = DateTime.Today,
                    Address = App.PATH
                },
            };

            advertisementsList.Items.Clear();
            advertisementsList.ItemsSource = _advertisements;

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
