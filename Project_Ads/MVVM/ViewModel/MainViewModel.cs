using Project_Ads.Core;

namespace Project_Ads.MVVM.ViewModel
{
    class MainViewModel: ObservableObject
    {
        public HomeViewModel HomeVM { get; set; }
        public ProfileViewModel ProfileVM { get; set; }
        
        public RelayCommand HomeViewСommand { get; set; }
        public RelayCommand ProfileViewСommand { get; set; }
        
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        
        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            ProfileVM = new ProfileViewModel();
            CurrentView = HomeVM;

            HomeViewСommand = new RelayCommand((o) =>
            {
                CurrentView = HomeVM;
            });
            ProfileViewСommand = new RelayCommand((o) =>
            {
                CurrentView = ProfileVM;
            });
        }
    }
}
