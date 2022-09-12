//using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Flights_GUI.Commands;
using Flights_GUI.UserControls;
using Flights_GUI.Model;
using Flights_BE;


namespace Flights_GUI.ViewModel
{
    public class FlightInfoVM : ViewModelBase
    {
        //model
        private Model.FlightInfoModel model;

#region properties
        private FlightInfo flight;
        public FlightInfo Flight
        {
            get { return flight; }
            set
            {
                flight = value;
                OnPropertyChanged(nameof(Flight));
            }
        }

        #endregion

        public FlightInfoVM()
        {
            model = new FlightInfoModel();
        }


    }
}
