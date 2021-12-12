using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Project_Ads.Core;
using Project_Ads.Model;

namespace Project_Ads.MVVM.View
{
    public class Filter: ObservableObject
    {
        private bool _isCat = true;
        private bool _isDog = true;
        private bool _isFind = true;
        private bool _isLost = true;
        public bool IsCat
        {
            get => _isCat;
            set
            {
                _isCat = value;
                OnPropertyChanged("IsCat");
            }
        }

        public bool IsDog
        {
            get => _isDog;
            set
            {
                _isDog = value;
                OnPropertyChanged("IsDog");
            }
        }

        public bool IsFind
        {
            get => _isFind;
            set
            {
                _isFind = value;
                OnPropertyChanged("IsFind");
            }
        }

        public bool IsLost
        {
            get => _isLost;
            set
            {
                _isLost = value;
                OnPropertyChanged("IsLost");
            }
        }

        public bool IsItemFilter(Advertisement item)
        {
            return (item.TypeAdvertisement == Advertisement.Type.Find && IsFind
                   || item.TypeAdvertisement == Advertisement.Type.Lose && IsLost)
                   && ( item.TypeAnimal == Animal.Type.Cat && IsCat
                      || item.TypeAnimal == Animal.Type.Dog && IsDog);
        }

        public void CopyTo(Filter f)
        {
            IsCat = f.IsCat;
            IsDog = f.IsDog;
            IsFind = f.IsFind;
            IsLost = f.IsLost;
        }
    }
    
    public partial class BoardView : UserControl
    {
        public Filter Filter { get; set; } = new Filter();
        private Filter _preventFilter = new Filter();
        
        private ObservableCollection<Advertisement> _advertisements { get; set; } =
            new ObservableCollection<Advertisement>();

        private ObservableCollection<Advertisement> _filterAdvertisements { get; set; } =
            new ObservableCollection<Advertisement>();

        public BoardView()
        {
            InitializeComponent();

            menuFilter.DataContext = Filter;

            _advertisements.CollectionChanged += (s, e) => {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    var item = _advertisements[_advertisements.Count - 1];
                    if (Filter.IsItemFilter(item))
                        _filterAdvertisements.Add(item);
                }
            };
            
            _advertisements.Add(new Advertisement() {
                Id = 1, ImageUrl ="/Icons/dogs.png", 
                TypeAdvertisement = Advertisement.Type.Find,
                TypeAnimal = Animal.Type.Dog,
                Marks= "Срочно !!!! нашли щеночка После 21:00 некуда деть." +
                       "\nХозяева отзовитесь Голубой ошейник. После 21:00 некуда деть",
                ColorAnimal = Animal.Color.Black,Phone = "8 999 586 1516",
                DateFind = DateTime.Now,
                LocationFind = "3 мкр 35 дом"
            });
            _advertisements.Add(new Advertisement() {
                Id = 2, ImageUrl = @"\Icons\cat.jpg",
                TypeAdvertisement = Advertisement.Type.Lose,
                TypeAnimal = Animal.Type.Cat, Marks = App.PATH,
                Phone = "8 800 555 3535", ColorAnimal = Animal.Color.Blue,
                DateFind = DateTime.Today,
                LocationFind = App.PATH
            });
            _advertisements.Add(new Advertisement() {
                    Id = 3, ImageUrl = "/Icons/cat.jpg",
                    TypeAdvertisement = Advertisement.Type.Find,
                    TypeAnimal = Animal.Type.Cat, Marks = "Дополнительные приметы",
                    Phone = "8 800 555 3535", ColorAnimal = Animal.Color.Blue,
                    DateFind = DateTime.Today,
                    LocationFind = "На высоких горах"
                });
            _advertisements.Add(new Advertisement() {
                Id = 4, ImageUrl = "/Icons/cat.jpg",
                TypeAdvertisement = Advertisement.Type.Lose,
                TypeAnimal = Animal.Type.Cat, Marks = "Дополнительные приметы",
                Phone = "8 800 555 3535", ColorAnimal = Animal.Color.Blue,
                DateFind = DateTime.Today,
                LocationFind = "На высоких горах"
            });
            _advertisements.Add(new Advertisement() {
                Id = 5, ImageUrl = "/Icons/cat.jpg",
                TypeAdvertisement = Advertisement.Type.Find,
                TypeAnimal = Animal.Type.Cat, Marks = "Дополнительные приметы",
                Phone = "8 800 555 3535", ColorAnimal = Animal.Color.Blue,
                DateFind = DateTime.Today,
                LocationFind = "На высоких горах"
            });
            
            advertisementsList.Items.Clear();
            advertisementsList.ItemsSource = _filterAdvertisements;

            menuFilter_button_cancel.Click += CloseMenuFilter;
            menuFilter_button_apply.Click += ApplyFilters;
            menuFilter_button_apply.Click += CloseMenuFilter;
            
            newAd_cancel.Click += CloseNewAdPopup;
            newAd_apply.Click += CloseMenuFilter;

            upload_new_img.Click += LoadAnimalImage;

            DataContext = this;
        }

        private void FilterListAdvertisment()
        {
            _filterAdvertisements.Clear();
            foreach (var item in _advertisements.Where(a => Filter.IsItemFilter(a)))
                _filterAdvertisements.Add(item);
        }

        private void CloseMenuFilter(object o = null, RoutedEventArgs e = null)
        {
            Filter.CopyTo(_preventFilter);
            menuFilter.IsOpen = false;
        }

        private void OpenMenuFilter(object sender = null, RoutedEventArgs e = null)
        {
            _preventFilter.CopyTo(Filter);
            menuFilter.IsOpen = true;
        }

        private void ApplyFilters(object sender = null, RoutedEventArgs e = null)
        {
            _preventFilter.CopyTo(Filter);
            FilterListAdvertisment();
        }

        private void OpenNewAdPopup(object o = null, RoutedEventArgs e = null) => formAdding.IsOpen = true;

        private void CloseNewAdPopup(object o = null, RoutedEventArgs e = null) => formAdding.IsOpen = false;

        private void LoadAnimalImage(object o, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                newAd_img.Source = new BitmapImage(fileUri);
            }
        }

        private void AdvertisementsList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (Advertisement) advertisementsList.SelectedItem;
            Console.WriteLine(a.Phone);
        }
    }
}