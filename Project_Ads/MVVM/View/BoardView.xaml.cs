using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using Project_Ads.Core;

namespace Project_Ads.MVVM.View
{
    public partial class BoardView : UserControl {
        
        private ObservableCollection<Advertisement> _advertisements { get; set; }
        
        public BoardView()
        {
            InitializeComponent();
            
            _advertisements = new ObservableCollection<Advertisement>()
            {
                new Advertisement(){Id = 1, ImageUrl ="/Icons/icons8_image_120px_3.png" ,Title="Найдена кошка", Description="Какое-то описание" },
                new Advertisement(){Id = 2, Title="Найдена собака", Description="Какое-то описание" },
            };
            
            advertisementsList.ItemsSource = _advertisements;
            advertisementsList.SelectionChanged += ((sender, args) =>
            {
                ((Advertisement)advertisementsList.SelectedItem).Title = "Изменили название";
            });
        }
    }
    
    public class Advertisement: ObservableObject
    {
        private static string PATH = Environment.CurrentDirectory + "/../../../Properties";
        public int Id { get; set; }

        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        } 
        public string Description { get; set; } 
        
        private string _imageUrl = $"{PATH}/Icons/cat.jpg";
        public string ImageUrl
        {
            get => _imageUrl;
            set {
                _imageUrl = PATH + value; 
                OnPropertyChanged("ImageUrl");
            }
        }
    }
}