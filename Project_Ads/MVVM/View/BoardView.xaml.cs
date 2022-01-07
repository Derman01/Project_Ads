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
    public partial class BoardView : UserControl
    {
        public Filter Filter { get; set; } = new Filter();
        private Filter _preventFilter = new Filter();
        private User user = App.User;
        
        private ObservableCollection<Advertisement> _advertisements { get; set; } =
            new ObservableCollection<Advertisement>();

        private ObservableCollection<Advertisement> _filterAdvertisements { get; set; } =
            new ObservableCollection<Advertisement>();

        public BoardView()
        {
            InitializeComponent();
            
            menuFilter.DataContext = Filter;
            userName.DataContext = user;

            _advertisements.CollectionChanged += (s, e) =>
            {
                _filterAdvertisements.Clear();
                var _list = _advertisements.Where(Filter.IsItemFilter).OrderByDescending(s => s.DateCreate);
                foreach (var add in _list)
                {
                    _filterAdvertisements.Add(add);
                }
               
            };

            #region Add
            _advertisements.Add(new Advertisement() {
                RegNum = 1, ImageUrl =$@"{App.PATH}/Icons/dogs.png",
                TypeAdvertisement = Advertisement.Type.Find,
                TypesAnimal = Animal.Type.Dog,
                Marks= "Срочно !!!! нашли щеночка После 21:00 некуда деть." +
                       "\nХозяева отзовитесь Голубой ошейник. После 21:00 некуда деть",
                
                Description = "Рыжий окрас",
                Phone = "8 999 586 1516",
                DateEvent = DateTime.Now,
                DateCreate = DateTime.Now,
                Address = "3 мкр 35 дом"
            });
            _advertisements.Add(new Advertisement() {
                RegNum = 2, ImageUrl = $@"{App.PATH}\Icons\cat.jpg",
                TypeAdvertisement = Advertisement.Type.Lose,
                TypesAnimal = Animal.Type.Cat, Marks = App.PATH,
                Phone = "8 800 555 3535", 
                Description = "Черный окрас",
                DateEvent = DateTime.Today,
                DateCreate = DateTime.Today,
                Address = App.PATH
            });
            _advertisements.Add(new Advertisement() {
                    RegNum = 3, ImageUrl = $@"{App.PATH}/Icons/cat.jpg",
                    TypeAdvertisement = Advertisement.Type.Find,
                    TypesAnimal = Animal.Type.Cat, Marks = "Дополнительные приметы",
                    Phone = "8 800 555 3535",
                    Description = "Черный окрас",
                    DateEvent = DateTime.Today,
                    DateCreate = DateTime.Today,
                    Address = "На высоких горах"
                });
            _advertisements.Add(new Advertisement() {
                RegNum = 4, ImageUrl = $@"{App.PATH}/Icons/cat.jpg",
                TypeAdvertisement = Advertisement.Type.Lose,
                TypesAnimal = Animal.Type.Cat, Marks = "Дополнительные приметы",
                Phone = "8 800 555 3535", 
                Description = "Белый окрас",
                DateEvent = DateTime.Today,
                DateCreate = DateTime.Now,
                Address = "На высоких горах"
            });
            _advertisements.Add(new Advertisement() {
                RegNum = 5, ImageUrl = $@"{App.PATH}/Icons/cat.jpg",
                TypeAdvertisement = Advertisement.Type.Find,
                TypesAnimal = Animal.Type.Cat, Marks = "Дополнительные приметы",
                Phone = "8 800 555 3535", 
                Description = "Голубой окрас",
                DateEvent = DateTime.Today,
                DateCreate = DateTime.Today,
                Address = "На высоких горах"
            });
            #endregion
            advertisementsList.Items.Clear();
            advertisementsList.ItemsSource = _filterAdvertisements;

            menuFilter_button_cancel.Click += CloseMenuFilter;
            menuFilter_button_apply.Click += ApplyFilters;
            menuFilter_button_apply.Click += CloseMenuFilter;
            
            formAdding_button_cancel.Click += CloseFormAdding;
            formAdding_button_apply.Click += CloseMenuFilter;

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


        private void OpenFormAdding(object o = null, RoutedEventArgs e = null) => formAdding.IsOpen = true;

        private void CloseFormAdding(object o = null, RoutedEventArgs e = null) => formAdding.IsOpen = false;

        private void LoadAnimalImage(object o, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                formAdding_image.Source = new BitmapImage(fileUri);
            }
        }

        private void AdvertisementsList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (Advertisement) advertisementsList.SelectedItem;
            Console.WriteLine(a.Phone);
        }

        private void CreateAdvertisment(object sender, RoutedEventArgs e)
        {
            try
            {
                var newAddvertisment = new Advertisement()
                {
                    Marks = formAdding_marks.Text,
                    Description = formAdding_description.Text,
                    DateCreate = DateTime.Now,
                    DateEvent = formAdding_date.SelectedDate.Value,
                    ImageUrl = formAdding_image.Source.ToString(),
                    Address = formAdding_address.Text,
                    TypeAdvertisement = formAdding_radio_find.IsChecked.Value ? Advertisement.Type.Find : Advertisement.Type.Lose,
                    TypesAnimal = formAdding_radio_cat.IsChecked.Value ? Animal.Type.Cat :Animal.Type.Dog,
                    Phone = "8000-999-99-99"
                    // Добавить телефон
                };
                _advertisements.Add( newAddvertisment);
                formAdding.IsOpen = false;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Поля введены неккоректно");
            }
        }
    }
}