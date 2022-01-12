using Project_Ads.Core;

namespace Project_Ads.MVVM.ViewModel
{
    class MainViewModel: ObservableObject
    {
        public BoardViewModel BoardVm { get; set; }
        public UserBoardViewModel UserBoardVm { get; set; }
        public ProfileViewModel ProfileVM { get; set; }
        public RegistrationViewModel RegistrationVM { get; set; }


        public static RelayCommand HomeViewСommand { get; set; }
        public RelayCommand UserBoardViewСommand { get; set; }
        public static RelayCommand ProfileViewСommand { get; set; }
        public static RelayCommand RegistrationViewСommand { get; set; }

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
            RegistrationVM = new RegistrationViewModel();
            UserBoardVm = new UserBoardViewModel();

            CurrentView = BoardVm;
            
            HomeViewСommand = new RelayCommand((o) =>
            {
                CurrentView = BoardVm;
            });
            ProfileViewСommand = new RelayCommand((o) =>
            {
                CurrentView = ProfileVM;

            });
            RegistrationViewСommand = new RelayCommand((o) =>
            {
                CurrentView = RegistrationVM;
            });
            UserBoardViewСommand = new RelayCommand((o) =>
            {
                CurrentView = UserBoardVm;
            });
        }
    }
}
