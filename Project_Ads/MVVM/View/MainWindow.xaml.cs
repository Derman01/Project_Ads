using System.Windows;
using System.Windows.Input;

namespace Project_Ads
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void Close(object s, RoutedEventArgs e) => Close();

        private void Minimaze(object s, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
