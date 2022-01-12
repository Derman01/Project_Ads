using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Project_Ads.MVVM.Model;

namespace Project_Ads.MVVM.View
{
    public partial class BoardView : UserControl
    {
        public Filter Filter { get; set; } = new Filter();
        private Filter _preventFilter = new Filter();
        private string userName => App.GetUserName();

        private List<Dictionary<string, object>> _advertisements;

        private ObservableCollection<AdvertisementData>
            _filteringAdvertisements = new ObservableCollection<AdvertisementData>();

        public BoardView()
        {
            InitializeComponent();
            _listAdvertisment.Items.Clear();
            _listAdvertisment.ItemsSource = _filteringAdvertisements;

            UserNameTextBlock.Text = userName;
            menuFilter.DataContext = Filter;
            if (Session.GetUser().UserRole == User.Role.NotAuthorizedUser)
                AddAdvButton.IsEnabled = false;
            
            Loaded += (sender, args) => UpdateFilteringListAdvertisement();
        }

        private void UpdateFilteringListAdvertisement()
        {
            if (_filteringAdvertisements.Count != 0) _filteringAdvertisements.Clear();
            _advertisements = App.GetAdvertisementList;
            foreach (var item in _advertisements)
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
                if (Filter.IsItemFilter(adv))
                    _filteringAdvertisements.Add(adv);
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
            formAdding.IsOpen = false;
            var color = formAdding_color.Text;
            var description = formAdding_desc.Text;
            var typeAdv = formAdding_radio_find.IsChecked.Value
                ? AdvertisementData.AdvertisementType.Find
                : AdvertisementData.AdvertisementType.Lose;
            var address = formAdding_address.Text;
            var dataEvent = formAdding_date.SelectedDate.Value;
            var typeAnimal = formAdding_radio_cat.IsChecked.Value
                ? AdvertisementData.AnimalType.Cat
                : AdvertisementData.AnimalType.Dog;
            var picUrl = formAdding_image.Source.ToString();
            try
            {
                App.CreateAdvertisements(
                    Convert.ToInt32(typeAdv), address, description, dataEvent, 
                    Convert.ToInt32(typeAnimal), color, picUrl);
                _advertisements = App.GetAdvertisementList;
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