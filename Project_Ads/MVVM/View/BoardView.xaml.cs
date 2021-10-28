using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Project_Ads.MVVM.View
{
    public partial class BoardView : UserControl
    {
        
        private ObservableCollection <Advertisement> _advertisements { get; set; }
        
        public BoardView()
        {
            InitializeComponent();
            
            _advertisements = new ObservableCollection<Advertisement>()
            {
                new Advertisement(){Id = 1, Title="Найден кот", Description="Какое-то описание" },
                new Advertisement(){Id = 2, Title="Найдена собака", Description="Какое-то описание" },
            };

            
            advertisementsList.ItemsSource = _advertisements;
        }
    }
    
    public class Advertisement
    {
        public int Id { get; set; }
        public string Title { get; set; } // название
        public string Description { get; set; } // описание
        public string ImageUrl { get; set; } = "/Icons/icons8_image_120px_3.png"; // ссылка на фото
    }
}