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
using UserControl = System.Windows.Controls.UserControl;

namespace Flights_GUI.UserControls
{
    /// <summary>
    /// Interaction logic for LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        private readonly LoginViewModel _viewModel = new();
        public LoginUserControl()
        {
            InitializeComponent();
        }
    }
        
}
