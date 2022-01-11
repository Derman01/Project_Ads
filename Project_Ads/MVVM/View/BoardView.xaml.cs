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
        
        private ObservableCollection<Advertisement> _advertisements { get; set; } =
            new ObservableCollection<Advertisement>();

        private ObservableCollection<Advertisement> _filterAdvertisements { get; set; } =
            new ObservableCollection<Advertisement>();

        public BoardView()
        {
            InitializeComponent();
            
            menuFilter.DataContext = Filter;

            _advertisements.CollectionChanged += (s, e) =>
            {
                _filterAdvertisements.Clear();
                var _list = _advertisements.Where(Filter.IsItemFilter).OrderByDescending(s => s.DateCreate);
                foreach (var add in _list)
                {
                    _filterAdvertisements.Add(add);
                }
               
            };

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
                    Type = formAdding_radio_find.IsChecked.Value ? Advertisement.AdvertisementType.Find : Advertisement.AdvertisementType.Lose,
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