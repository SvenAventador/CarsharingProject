using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarsharingProject.Windows.Admin.Pages
{
    /// <summary>
    /// Логика взаимодействия для Stations.xaml
    /// </summary>
    public partial class Stations : Page
    {
        public Stations()
        {
            InitializeComponent();
            Station.ItemsSource = CarsharingLibrary.Entities
                                                   .CarsharingDbContext
                                                   .GetContext()
                                                   .Stations
                                                   .OrderBy(x => x.StationId)
                                                   .ToList();
        }
    }
}
