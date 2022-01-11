using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Project_Ads.MVVM.Model;

namespace Project_Ads.MVVM.View
{
    public partial class BoardView : UserControl
    {
        public Filter Filter { get; set; } = new Filter();
        private Filter _preventFilter = new Filter();

        public string UserName => "User";

        private ObservableCollection<Advertisement> _advertisements => App.GetAdvertisementList;

        private ObservableCollection<Advertisement>
            _filteringAdvertisements = new ObservableCollection<Advertisement>();

        public BoardView()
        {
            InitializeComponent();
            _listAdvertisment.Items.Clear();
            _listAdvertisment.ItemsSource = _filteringAdvertisements;

            menuFilter.DataContext = Filter;
            Loaded += (sender, args) =>
            {
                UpdateFilteringListAdvertisement();
            };
        }

        private void UpdateFilteringListAdvertisement()
        {
            if (_filteringAdvertisements.Count != 0) _filteringAdvertisements.Clear();

            foreach (var item in _advertisements)
            {
                if (Filter.IsItemFilter(item))
                    _filteringAdvertisements.Add(item);
            }
        }


        private void LoadAnimalImage(object o, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                formAdding_image.Source = new BitmapImage(fileUri);
            }
        }
        
        private void OpenMenuFilter(object sender, RoutedEventArgs e)
        {
            _preventFilter.CopyTo(Filter);
            menuFilter.IsOpen = true;
        }
        
        private void OpenMenuAdv(object sender, RoutedEventArgs e)
        {
            formAdding.IsOpen = true;
        }

        private void CloseMenuFilter(object sender, RoutedEventArgs e)
        {
            Filter.CopyTo(_preventFilter);
            menuFilter.IsOpen = false;
        }

        private void ApplyFilter(object sender, RoutedEventArgs e)
        {
            _preventFilter.CopyTo(Filter);
            menuFilter.IsOpen = false;
            UpdateFilteringListAdvertisement();
        }

        private void CreateAdvertisement(object sender, RoutedEventArgs e)
        {
            try
            {
                var color = formAdding_color.Text;
                var description = formAdding_desc.Text;
                var typeAdv = formAdding_radio_find.IsChecked.Value
                    ? Advertisement.AdvertisementType.Find
                    : Advertisement.AdvertisementType.Lose;
                var address = formAdding_address.Text;
                var dataEvent = formAdding_date.SelectedDate.Value;
                var typeAnimal = formAdding_radio_cat.IsChecked.Value
                    ? Animal.Types.Cat
                    : Animal.Types.Dog;
                var picUrl = formAdding_image.Source.ToString();

                App.CreateAdvertisements(typeAdv, address, description, dataEvent, typeAnimal, color, picUrl);
                UpdateFilteringListAdvertisement();

            }
            catch (Exception exception)
            {
                MessageBox.Show("Поля введены неккоректно");
            }
            finally
            {
                formAdding_color.Text = String.Empty;
                formAdding_desc.Text = String.Empty;
                formAdding_address.Text = String.Empty;
                formAdding_date.SelectedDate = DateTime.Now;
                formAdding_radio_cat.IsChecked = true;

                formAdding.IsOpen = false;
            }
        }

        private void CloseFormForAdding(object sender, RoutedEventArgs e)
        {
            formAdding.IsOpen = false;
        }
    }
}