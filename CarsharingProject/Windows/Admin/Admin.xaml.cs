using System.Windows;
using System.Windows.Input;
using CarsharingLibrary.Functions;

namespace CarsharingProject.Windows.Admin
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
            MainFrame.Navigate(new Pages.StationPage());
            Manager.MainFrame = MainFrame;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Stations_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.StationPage());
        }

        private void Cars_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
