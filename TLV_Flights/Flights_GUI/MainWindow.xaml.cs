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
using Flights_GUI.ViewModel;

namespace Flights_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Flights_GUI.ViewModel.AllFlightsViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            this.viewModel = new Flights_GUI.ViewModel.AllFlightsViewModel(this);
            this.DataContext = viewModel;
        }

    }
}
