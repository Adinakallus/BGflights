using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maps.MapControl.WPF;
using Flights_GUI.Commands;
using Flights_GUI.UserControls;
using Flights_GUI.Model;
using Flights_GUI.ViewModel;
using Flights_BE;
using Flights_BL;
using Flights_DAL;
using Flights_GUI;

namespace Flights_GUI.ViewModel
{
    public class FlightsVM : INotifyPropertyChanged, IVM
    {
        #region propeties
        private bool _IsHome;
        public bool IsHome
        {
            get
            {
                return this._IsHome;
            }
            set
            {
                this._IsHome = value;
                OnPropertyChanged();
            }
        }
        private bool _IsSignIn;
        public bool IsSignIn
        {
            get
            {
                return this._IsSignIn;
            }
            set
            {
                this._IsSignIn = value;
                OnPropertyChanged();
            }
        }
        private bool _islogIn;
        public bool islogIn
        {
            get
            {
                return this._islogIn;
            }
            set
            {
                this._islogIn = value;
                OnPropertyChanged();
            }
        }
        private bool _usesrIslogin;
        public bool usesrIslogin
        {
            get
            {
                return this._usesrIslogin;
            }
            set
            {
                this._usesrIslogin = value;
                OnPropertyChanged();
            }
        }

        private bool _IsHistory;
        public bool IsHistory
        {
            get
            {
                return this._IsHistory;
            }
            set
            {
                this._IsHistory = value;
                OnPropertyChanged();
            }
        }
        private bool _isFlightDetailsUC;
        public bool isFlightDetailsUC
        {
            get
            {
                return this._isFlightDetailsUC;
            }
            set
            {
                this._isFlightDetailsUC = value;
                OnPropertyChanged();
            }
        }

        public string userName
        {
            get
            {
                return this.accountModel.userName;
            }
            set
            {
                this.accountModel.userName = value;
                OnPropertyChanged();
            }
        }
        public string password
        {
            get
            {
                return this.accountModel.password;
            }
            set
            {
                this.accountModel.password = value;
                OnPropertyChanged();

            }
        }

        private string _sendMessage;
        public string sendMessage
        {
            get
            {
                return this._sendMessage;
            }
            set
            {
                this._sendMessage = value;
                OnPropertyChanged();
            }
        }
        private bool _isHoliday;

        public bool isHoliday
        {
            get
            {
                return this._isHoliday;
            }
            set
            {
                this._isHoliday = value;
                OnPropertyChanged();
            }
        }
        private bool _messageVisibility;

        public bool messageVisibility
        {
            get
            {
                return this._messageVisibility;
            }
            set
            {
                this._messageVisibility = value;
                OnPropertyChanged();
            }
        }

        private bool _startSearch;
        public bool startSearch
        {
            get
            {
                return this._startSearch;
            }
            set
            {
                this._startSearch = value;
                OnPropertyChanged();
            }
        }
        public string Holiday
        {
            get
            {
                this.isHoliday = true;
                return this.currentflightModel.getholiday;
            }
        }

        //
        #endregion

        #region models
        private MainWindow mainWindow { get; set; }
        private MapPolyline polyline { get; set; }
        private Pushpin PinAirport { get; set; }

        private Model.FlightsModel currentflightModel;
        public Model.UserAccountModel userAccountModel;
        public Model.FlightHistoryModel flightHistoryModel;
        public Model.WeatherModel weatherModel;
        #endregion models

        #region Thread
        //set thread
        BackgroundWorker backgroundTread { get; set; }
        private void BackgroundTread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void BackgroundTread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //set all flights:
            this.IncomingFlights = new ObservableCollection<Flights_BE.FlightInfoPartial>(currentflightModel.IncomingFlights);
            this.OutgoingFlights = new ObservableCollection<Flights_BE.FlightInfoPartial>(currentflightModel.OutgoingFlights);

            mainWindow.myMap.Children.Clear();
            //Excute commands to set pushpins on map
            showMapCommand.Execute(IncomingFlights);
            showMapCommand.Execute(OutgoingFlights);
            if (this.polyline != null) //need to update poline to.
                updateTrial();
        }

        private void BackgroundTread_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                this.backgroundTread.ReportProgress(1);
                Thread.Sleep(1000); // Sleep for a second.  

            }
        }

        #endregion

        #region flights
        private ObservableCollection<FlightInfoPartial> _IncomingFlights { get; set; }

        private ObservableCollection<FlightInfoPartial> _OutgoingFlights { get; set; }
        public ObservableCollection<FlightInfoPartial> IncomingFlights
        {
            get
            {
                return this._IncomingFlights;
            }
            set
            {
                this._IncomingFlights = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<FlightInfoPartial> OutgoingFlights
        {
            get
            {
                return this._OutgoingFlights;
            }
            set
            {
                this._OutgoingFlights = value;
                OnPropertyChanged();
            }
        }

        private FlightInfo _singleFlight { get; set; }
        public FlightInfo singleFlight
        {
            get
            {
                return this._singleFlight;
            }
            set
            {
                this._singleFlight = value;
                OnPropertyChanged();
            }
        }
        #endregion flights

        #region history
        public ObservableCollection<FlightInfo> flightsHistoryPerUser
        {
            get
            {
                return this.flightHistoryModel.flightsHistoryPerUser;
            }
            set
            {
                this.flightHistoryModel.flightsHistoryPerUser = value;
                OnPropertyChanged();
            }
        }
        public void showHistory()
        {
            DateTime from = DateTime.Today;
            DateTime to = DateTime.Today;
            try
            {
                from = this.mainWindow.HistoryUC.dpFrom.SelectedDate.Value;
                to = this.mainWindow.HistoryUC.dpTo.SelectedDate.Value;
            }
            catch
            {

            }
            //update dates on history model
            this.flightHistoryModel.setDates(from, to);
            this.flightsHistoryPerUser = this.flightHistoryModel.flightsHistoryPerUser;
        }

        #endregion

        #region weather
        private cityWeather _originWeather;
        public cityWeather originWeather
        {
            get
            {
                return this._originWeather;
            }
            set
            {
                this._originWeather = value;
                OnPropertyChanged();
            }
        }

        private cityWeather _destinationWeather;
        public cityWeather destinationWeather
        {
            get
            {
                return this._destinationWeather;
            }
            set
            {
                this._destinationWeather = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region commands
        public showMapCommand showMapCommand { get; set; }
        public openFlightDetailsCommand openFlightDetailsCommand { get; set; }
        public SignUPCmnd signUPCmnd { get; set; }
        public LoginCmnd loginCmnd { get; set; }
        public showHistoryPerUser showHistoryPerUser { get; set; }
        public closeMessageCommand closeMessageCommand { get; set; }
        public openTimeWIndowCommand openTimeWIndowCommand { get; set; }

        #endregion

        #region constractor
        public FlightsVM(MainWindow _mainWindow)
        {
            this.mainWindow = _mainWindow;
            //set modles
            currentflightModel = new Model.FlightsModel();
            userAccountModel = new Model.UserAccountModel();
            flightHistoryModel = new Model.FlightHistoryModel();
            weatherModel = new Model.WeatherModel();

            //place only for now
            //this.flightsHistoryPerUser = new ObservableCollection<flightModel.flightInfo>(this.historyModel.flightsHistoryPerUser);

            //set viewModel
            this.mainWindow.NavigationBar.DataContext = new NavigationVM(this);


            //set thread
            this.backgroundTread = new BackgroundWorker();
            this.backgroundTread.DoWork += BackgroundTread_DoWork;
            this.backgroundTread.ProgressChanged += BackgroundTread_ProgressChanged;
            this.backgroundTread.WorkerReportsProgress = true;
            this.backgroundTread.RunWorkerCompleted += BackgroundTread_RunWorkerCompleted;

            //set propeties
            this.usesrIslogin = false; //means that no user is login
            this.IsHistory = false;
            this.islogIn = false;
            this.IsSignIn = false;
            this.isFlightDetailsUC = false;
            this.IsHome = true;
            this.isHoliday = false;
            this.messageVisibility = false;

            //set command
            showMapCommand = new showMapCommand(this);
            FlightDetailsCmnd = new openFlightDetailsCommand(this);
            signUPCmnd = new SignUPCmnd(this);
            loginCmnd = new LoginCmnd(this);
            showHistoryPerUser = new showHistoryPerUser(this);
            closeMessageCommand = new closeMessageCommand(this);
            openTimeWIndowCommand = new openTimeWIndowCommand(this);

            // set function to start when the command is execute
            showMapCommand.addFlightsToMap += ShowMapCommand_addFlightsToMap;
            openFlightDetailsCommand.openwndFlightDetails += OpenFlightDetailsCommand_openwndFlightDetails;

            //start thread that update the map every second.
            this.backgroundTread.RunWorkerAsync();

        }
        #endregion

        #region openDetailsCommand
        private void OpenFlightDetailsCommand_openwndFlightDetails(flightInfoPartial flight)
        {
            if (this.usesrIslogin)
            {
                this.isFlightDetailsUC = true;
                this.currentflightModel.setCodeFlight(flight.SourceId);
                this.singleFlight = this.currentflightModel.singleFlightInfo;

                this.WeatherModel.setOriginCode(this.singleFlight.airport.origin.position.country.code);
                this.WeatherModel.setDestinationCode(this.singleFlight.airport.origin.position.region.city, this.singleFlight.airport.destination.position.region.city);
                this.originWeather = this.WeatherModel.originWeather;
                this.destinationWeather = this.WeatherModel.destinationWeather;

                addTrialToMap(this.singleFlight);

                //TODO -  save data to db using  by currentflightModel for this
                //only for now:
                this.flightsHistoryPerUser.Add(this.singleFlight);
                this.accountModel.saveChanges(this.flightsHistoryPerUser.ToList());
            }
            else
            {
                this.sendMessage = "You are not logged in, please log in first.";
                this.messageVisibility = true;
            }
        }

        private void updateTrial()
        {
            if (this.singleFlight != null)
            {
                this.currentflightModel.setCodeFlight(this.singleFlight.identification.id);
                this.singleFlight = this.currentflightModel.singleFlightInfo;
                this.addTrialToMap(this.singleFlight);
            }
        }

        private void addTrialToMap(flightInfo flight)
        {
            if (flight != null)
            {
                if (this.polyline != null)
                {
                    mainWindow.myMap.Children.Remove(this.polyline);
                    mainWindow.myMap.Children.Remove(this.PinAirport);
                }
                this.polyline = new MapPolyline();
                polyline.Tag = "polyline";
                polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LightBlue);
                polyline.StrokeThickness = 4;
                polyline.Opacity = 1;
                polyline.Locations = new LocationCollection();
                if (flight != null)
                {
                    foreach (var position in flight.trail)
                    {
                        polyline.Locations.Add(new Location(position.lat, position.lng));
                    }
                    mainWindow.myMap.Children.Add(polyline);
                }
                // set airport location
                this.PinAirport = new Pushpin
                {
                    ToolTip = flight.airport.origin.position.region.city + ": " +
                    flight.airport.origin.name
                };
                this.PinAirport.Style = (Style)mainWindow.Resources["Airport"];
                var PlaneLocation = new Location { Latitude = flight.airport.origin.position.latitude, Longitude = flight.airport.origin.position.longitude };
                this.PinAirport.Location = PlaneLocation;

                mainWindow.myMap.Children.Add(PinAirport);
            }
        }
        #endregion

        #region showMapCommand
        private void ShowMapCommand_addFlightsToMap(ObservableCollection<flightInfoPartial> listOfFlights)
        {
            if (listOfFlights != null)
            {
                foreach (var flight in listOfFlights)
                {
                    Pushpin PinCurrent = new Pushpin { ToolTip = flight.flightCode };
                    PinCurrent.MouseDown += PinCurrent_MouseDown;
                    PinCurrent.DataContext = flight;
                    if (flight.Source == "TLV")
                        PinCurrent.Style = (Style)mainWindow.Resources["fromIsrael"];
                    else
                        PinCurrent.Style = (Style)mainWindow.Resources["ToIsrael"];
                    var PlaneLocation = new Location { Latitude = flight.Lat, Longitude = flight.Long };
                    PinCurrent.Location = PlaneLocation;
                    mainWindow.myMap.Children.Add(PinCurrent);
                }
            }
        }
        private void PinCurrent_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            flightInfoPartial flight = ((sender as Pushpin).DataContext) as flightInfoPartial;
            openFlightDetailsCommand.Execute(flight);
        }

        #endregion

        #region onPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
