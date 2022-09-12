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
        private Flights_BE.FlightInfo selectedFlight;
        public Flights_BE.FlightInfo SelectedFlight
        {
            get { return selectedFlight; }
            set
            {
                selectedFlight = value;
                OnPropertyChanged(nameof(selectedFlight));
            }
        }

        #endregion

        public FlightInfoVM(FlightInfoPartial flight)
        {
            model = new FlightInfoModel();

            this.SelectedFlight= model.GetFlightInfo(flight.SourceId);


        }


    }
}
