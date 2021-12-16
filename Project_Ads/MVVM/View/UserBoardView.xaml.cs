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
                    Id = 1, ImageUrl ="/Icons/dogs.png",
                    TypeAdvertisement = Advertisement.Type.Find,
                    TypeAnimal = Animal.Type.Dog,
                    Marks= "Срочно !!!! нашли щеночка После 21:00 некуда деть." +
                                                  "\nХозяева отзовитесь Голубой ошейник. После 21:00 некуда деть",
                    ColorAnimal = Animal.Color.Black,Phone = "8 999 586 1516",
                    DateType = DateTime.Now,
                    LocationFind = "3 мкр 35 дом"
                },
                new Advertisement()
                {
                    Id = 2, ImageUrl = @"\Icons\cat.jpg",
                    TypeAdvertisement = Advertisement.Type.Lose,
                    TypeAnimal = Animal.Type.Cat, Marks = App.PATH,
                    Phone = "8 800 555 3535", ColorAnimal = Animal.Color.Blue,
                    DateType = DateTime.Today,
                    LocationFind = App.PATH
                },
                new Advertisement()
                {
                    Id = 2, ImageUrl = @"\Icons\cat.jpg",
                    TypeAdvertisement = Advertisement.Type.Lose,
                    TypeAnimal = Animal.Type.Cat, Marks = App.PATH,
                    Phone = "8 800 555 3535", ColorAnimal = Animal.Color.Blue,
                    DateType = DateTime.Today,
                    LocationFind = App.PATH
                },
            };

            advertisementsList.Items.Clear();
            advertisementsList.ItemsSource = _advertisements;
        }
    }
}
