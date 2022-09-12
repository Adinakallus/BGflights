using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flights_GUI.ViewModel;
using Flights_GUI.Model;
using System.Windows.Input;

namespace Flights_GUI.Commands
{
    public class FlightsHistoryCmnd : CommandBase
    {
        private FlightsHistoryVM VM;

        public FlightsHistoryCmnd(FlightsHistoryVM flightsHistoryVM)
        {
            this.VM = flightsHistoryVM;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object paramater)
        {
            this.VM.Search = true;
            this.VM.ShowHistory();
        }
    }
}
