
using Microsoft.Maps.MapControl.WPF;
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
    public class AllFlightsViewModel:ViewModelBase
    {
        #region Properties
        private ObservableCollection<Flights_BE.FlightInfoPartial> allFlights;
        public ObservableCollection<Flights_BE.FlightInfoPartial> AllFlights
        {
            get {return allFlights;} 
            set {
                allFlights = value;
                OnPropertyChanged(nameof(allFlights));
                }
        }

        private ObservableCollection<Pushpin> pushpins;
        public ObservableCollection<Pushpin> Pushpins
        {
            get {return pushpins;}
            set
            {
                pushpins = value;
                OnPropertyChanged(nameof(pushpins));
            }
        }
        #endregion

        #region models
        private MainWindow mainWindow { get; set; }

        private Model.AllFlightsModel _allFlightsModel;
        #endregion

        #region threads
        //set thread
        BackgroundWorker backgroundThread { get; set; }
        private void BackgroundThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // ???
        }

        private void BackgroundThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //refresh list of flights and list of pushpins

            //Task.Run(refreshLists);
            this.AllFlights=_allFlightsModel.UpdateFlights();
            refreshPushpins();
            //set all flights:
            // this.allFlights = new ObservableCollection<FlightInfoPartial>((IEnumerable<FlightInfoPartial>)_allFlightsModel.GetFlights());

            // mainWindow.myMap.Children.Clear();

            //Excute commands to set pushpins on map
            //initializeMapCommand.Execute(this.allFlights);
            // showMapCommand.Execute(OutgoingFlights);
            //   if (this.polyline != null) //need to update poline to.
            //     updateTrial();
        }

        private void BackgroundThread_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                this.backgroundThread.ReportProgress(1);
                Thread.Sleep(1000); // Sleep for a second.  

            }
        }
        #endregion
       
        public InitializeMapCommand initializeMapCommand { get; set; }


      //  public AllFlightsViewModel(MainWindow _mainWindow)
        public  AllFlightsViewModel()
        {
            //this.mainWindow = _mainWindow;
            _allFlightsModel = new Model.AllFlightsModel();
            this.AllFlights = _allFlightsModel.UpdateFlights();
            refreshPushpins();
            //initialize allFlights and pushpins

            //Task.Run(refreshLists);

            //set thread
            this.backgroundThread = new BackgroundWorker();
            this.backgroundThread.DoWork += BackgroundThread_DoWork;
            this.backgroundThread.ProgressChanged += BackgroundThread_ProgressChanged;
            this.backgroundThread.WorkerReportsProgress = true;
            this.backgroundThread.RunWorkerCompleted += BackgroundThread_RunWorkerCompleted;


            //set command
            initializeMapCommand = new InitializeMapCommand(this);


            // set function to start when the command is execute
            //showMapCommand.addFlightsToMap += ShowMapCommand_addFlightsToMap;

            //start thread that update the map every second.
            this.backgroundThread.RunWorkerAsync();


        }

        #region showMapCommand

        private async Task refreshLists()
        {
            this.allFlights = await _allFlightsModel.UpdateFlightsAsync();
            this.refreshPushpins();

        }
        private void refreshPushpins()
        {

            if (this.allFlights != null)
            {
           
                this.Pushpins = new ObservableCollection<Pushpin>();
                foreach (var flight in this.allFlights)
                {
                   var longitude= flight.Longitude;
                    var latitude= flight.Latitude;
                    Pushpin pin = new Pushpin { ToolTip = flight.FlightCode };
                    pin.Location = new Location(latitude, longitude);
                    pin.MouseDown += PinCurrent_MouseDown;
                    this.Pushpins.Add(pin);
                   /* PinCurrent.DataContext = flight;
                    if (flight.Source == "TLV")
                        PinCurrent.Style = (Style)mainWindow.Resources["fromIsrael"];
                    else
                        PinCurrent.Style = (Style)mainWindow.Resources["ToIsrael"];
                    var PlaneLocation = new Location { Latitude = flight.Lat, Longitude = flight.Long };
                  
                    mainWindow.myMap.Children.Add(PinCurrent);
                   */

                }
               }
           // return this.pushpins;
        }
        private void PinCurrent_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)// NEED TO FINISH
        {
            Flights_BE.FlightInfoPartial flight = ((sender as Pushpin).DataContext) as Flights_BE.FlightInfoPartial;
            //openFlightDetailsCommand.Execute(flight);
        }

        #endregion

    }
}
