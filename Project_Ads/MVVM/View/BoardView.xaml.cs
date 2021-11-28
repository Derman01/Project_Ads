using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Project_Ads.Core;
using Project_Ads.Model;

namespace Project_Ads.MVVM.View
{
    public partial class BoardView : UserControl
    {
        
        /*имеется 4 радиокноки
         rd_cat rd_dog
         rd_lost - пропавшие
         rd_found - найденные
         */       
        
        private ObservableCollection<Advertisement> _advertisements { get; set; }
        
        public BoardView()
        {
            InitializeComponent();
            
            _advertisements = new ObservableCollection<Advertisement>()
            {
                new Advertisement()
                {
                    Id = 1, ImageUrl ="/Icons/dogs.png", 
                    TypeAdvertisement = Advertisement.Type.Find,
                    TypeAnimal = Animal.Type.Dog,
                    Marks= "Срочно !!!! нашли щеночка После 21:00 некуда деть." +
                                                  "\nХозяева отзовитесь Голубой ошейник. После 21:00 некуда деть",
                    ColorAnimal = Animal.Color.Black,Phone = "8 999 586 1516",
                    DateFind = DateTime.Now,
                    LocationFind = "3 мкр 35 дом"
                },
                new Advertisement()
                {
                    Id = 2, ImageUrl = @"\Icons\cat.jpg",
                    TypeAdvertisement = Advertisement.Type.Lose,
                    TypeAnimal = Animal.Type.Cat, Marks = App.PATH,
                    Phone = "8 800 555 3535", ColorAnimal = Animal.Color.Blue,
                    DateFind = DateTime.Today,
                    LocationFind = App.PATH
                },
                new Advertisement()
                {
                    Id = 2, ImageUrl = "/Icons/cat.jpg",
                    TypeAdvertisement = Advertisement.Type.Lose,
                    TypeAnimal = Animal.Type.Cat, Marks = "Дополнительные приметы",
                    Phone = "8 800 555 3535", ColorAnimal = Animal.Color.Blue,
                    DateFind = DateTime.Today,
                    LocationFind = "На высоких горах"
                },
                new Advertisement()
                {
                    Id = 2, ImageUrl = "/Icons/cat.jpg",
                    TypeAdvertisement = Advertisement.Type.Lose,
                    TypeAnimal = Animal.Type.Cat, Marks = "Дополнительные приметы",
                    Phone = "8 800 555 3535", ColorAnimal = Animal.Color.Blue,
                    DateFind = DateTime.Today,
                    LocationFind = "На высоких горах"
                },
                new Advertisement()
                {
                    Id = 2, ImageUrl = "/Icons/cat.jpg",
                    TypeAdvertisement = Advertisement.Type.Lose,
                    TypeAnimal = Animal.Type.Cat, Marks = "Дополнительные приметы",
                    Phone = "8 800 555 3535", ColorAnimal = Animal.Color.Blue,
                    DateFind = DateTime.Today,
                    LocationFind = "На высоких горах"
                }
            };

            advertisementsList.Items.Clear();
            advertisementsList.ItemsSource = _advertisements;

            btn_cancel.Click += ClosePopup;
            btn_apply.Click += (sender, args) => ApplyFilters();
            btn_apply.Click += ClosePopup;
            newAd_cancel.Click += CloseNewAdPopup;
            newAd_apply.Click += ClosePopup;

            upload_new_img.Click += LoadAnimalImage;
        }

        private void ClosePopup(object o, RoutedEventArgs e) => popup.IsOpen = false;
        
        private void OpenPopup(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
        }

        private void ApplyFilters()
        {
            
        }

        private void OpenNewAdPopup(object o, RoutedEventArgs e) => newAd.IsOpen = true;

        private void CloseNewAdPopup(object o, RoutedEventArgs e) => newAd.IsOpen = false;

        private void LoadAnimalImage(object o, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                newAd_img.Source = new BitmapImage(fileUri);
            }
            newAd.IsOpen = true;
        }
    }
}