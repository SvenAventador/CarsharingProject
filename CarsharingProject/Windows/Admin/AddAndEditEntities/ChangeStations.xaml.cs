using CarsharingLibrary.Entities;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CarsharingProject.Windows.Admin.AddAndEditEntities
{
    /// <summary>
    /// Логика взаимодействия для ChangeStations.xaml
    /// </summary>
    public partial class ChangeStations : Window
    {
        private readonly Station? _station = new();
        public ChangeStations(Station? station)
        {
            InitializeComponent();

            if (station != null) _station = station;
            DataContext = _station;

            Actions.Text = _station == null ? "Добавить остановку" : "Изменить остановку";
            Create.Content = _station == null ? "Добавить остановку" : "Изменить остановку";
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
            Close();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            var errors = new StringBuilder();

            if (string.IsNullOrEmpty(StationName.Text))
                errors.AppendLine("Пожалуйста, введите корректное название остановки!");
            if (string.IsNullOrEmpty(StationLocation.Text))
                errors.AppendLine("Пожалуйста, введите корректное местонахождение остановки!");

            var existringStationName = CarsharingDbContext.GetContext()
                                                          .Stations
                                                          .FirstOrDefault(x => x.StationName == StationName.Text);
            var existringStationLocation = CarsharingDbContext.GetContext()
                                                              .Stations
                                                              .FirstOrDefault(x => x.Location == StationLocation.Text);

            if (_station?.StationId == 0)
            {
                if (existringStationName != null)
                    errors.AppendLine("Данное название остановки уже существует!");
                else if (existringStationLocation != null)
                    errors.AppendLine("Данное местонахождение уже существует!");
            }
            else
            {
                if (existringStationName != null && existringStationName.StationId != _station?.StationId)
                    errors.AppendLine("Данное название остановки уже существует!");
                if (existringStationLocation != null && existringStationLocation.StationId != _station?.StationId)
                    errors.AppendLine("Данное местонахождение уже существует!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(),
                                "Внимание!",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            if (_station?.StationId == 0)
                CarsharingDbContext.GetContext().Stations.Add(_station);

            try
            {
                CarsharingDbContext.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!",
                                "Внимание!",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),
                                "Внимание!",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
    }
}
