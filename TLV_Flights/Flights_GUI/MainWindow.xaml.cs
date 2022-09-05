using Microsoft.Toolkit.Wpf.UI.Controls;
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
using Windows.Services.Maps;
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;


namespace Flights_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            var mapControl = new MapControl();
            mapControl.Loaded += MapControl_Loaded;
            mapControl.MapServiceToken = App.token;
            MapGrid.Children.Add(mapControl);
        }

        private async void MapControl_Loaded(object sender, RoutedEventArgs e)
        {
            //by Israel latitude and longitude
            BasicGeoposition geoposition = new BasicGeoposition() { Latitude = 31.0461, Longitude = 34.8516 };
            var center = new Geopoint(geoposition);

            ((MapControl)sender).TrySetViewAsync(center , 4);
        }
    }
}
