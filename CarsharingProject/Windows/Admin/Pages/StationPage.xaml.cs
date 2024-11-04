using CarsharingLibrary.Entities;
using System.Windows;
using System.Windows.Controls;

namespace CarsharingProject.Windows.Admin.Pages
{
    /// <summary>
    /// Логика взаимодействия для StationPage.xaml
    /// </summary>
    public partial class StationPage : Page
    {
        public StationPage()
        {
            InitializeComponent();
            StationsModel.ItemsSource = CarsharingDbContext.GetContext()
                                                          .Stations
                                                          .OrderBy(x => x.StationId)
                                                          .ToList();
        }

        private void AddStation_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new AddAndEditEntities.ChangeStations(null).Show();
        }

        private void DeleteStation_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var deleteElements = StationsModel.SelectedItems.Cast<Station>().ToList();
            if (MessageBox.Show($"Вы уверены, что хотите удалить {deleteElements.Count} элемента(-ов)?",
                                 "Внимание!",
                                 MessageBoxButton.YesNo,
                                 MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            CarsharingDbContext.GetContext().Stations.RemoveRange(deleteElements);
            CarsharingDbContext.GetContext().SaveChanges();
            MessageBox.Show("Информация успешно сохранена.",
                            "Внимание!",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
            StationsModel.ItemsSource = CarsharingDbContext.GetContext()
                                                           .Stations
                                                           .OrderBy(x => x.StationId)
                                                           .ToList();
        }

        private void RefreshStation_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            StationsModel.ItemsSource = CarsharingDbContext.GetContext()
                                                           .Stations
                                                           .OrderBy(x => x.StationId)
                                                           .ToList();
        }

        private void EditStation_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new AddAndEditEntities.ChangeStations((sender as Button)!.DataContext as Station).Show();
        }
    }
}
