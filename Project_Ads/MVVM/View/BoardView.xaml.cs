using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using Project_Ads.Core;
using Project_Ads.Model;

namespace Project_Ads.MVVM.View
{
    public partial class BoardView : UserControl {
        
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
            
        }
    }
}