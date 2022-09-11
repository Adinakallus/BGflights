using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Flights_GUI.Model;
using Flights_GUI.ViewModel;
using Flights_BE;

namespace Flights_GUI.Commands
{
    public class InitializeMapCommand:CommandBase
    {
        private readonly ViewModel.AllFlightsViewModel viewModel;

        public InitializeMapCommand(AllFlightsViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

            public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        
        public override void Execute(object paramater)//!!!
        { 
            throw new NotImplementedException();
        }

    }
}
