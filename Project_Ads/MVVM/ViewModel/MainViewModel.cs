using Project_Ads.Core;

namespace Project_Ads.MVVM.ViewModel
{
    class MainViewModel: ObservableObject
    {
        public BoardViewModel BoardVm { get; set; }
        public ProfileViewModel ProfileVM { get; set; }
        
        public RelayCommand HomeViewСommand { get; set; }
        public RelayCommand ProfileViewСommand { get; set; }
        
        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            BoardVm = new BoardViewModel();
            ProfileVM = new ProfileViewModel();

            CurrentView = BoardVm;
            
            HomeViewСommand = new RelayCommand((o) =>
            {
                CurrentView = BoardVm;
            });
            ProfileViewСommand = new RelayCommand((o) =>
            {
                CurrentView = ProfileVM;
            });
        }
    }
}
