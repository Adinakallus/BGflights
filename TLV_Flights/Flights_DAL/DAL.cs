﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json.Linq;
using Json.Net;
using Flights_BE;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Flights_DAL
{
    public class Dal
    {
        private const String APIkey = "464586a1b635bd8df1683892c3b27dd6"; // <= API key 
        private const string allURL = @"https://data-cloud.flightradar24.com/zones/fcgi/feed.js?faa=1&bounds=41.13,29.993,25.002,36.383&satellite=1&mlat=1&flarm=1&adsb=1&gnd=1&air=1&vehicles=1&estimated=1&maxage=14400&gliders=1&selected=2d1e1f33&ems=1&stats=1";
          private const string uri = @"https://data-cloud.flightradar24.com/zones/fcgi/feed.js?faa=1&bounds=41.13,29.993,25.002,36.383&satellite=1&mlat=1&flarm=1&adsb=1&gnd=1&air=1&vehicles=1&estimated=1&maxage=14400&gliders=1&selected=2d1e1f33&ems=1&stats=1";
        private const string fLightURL = $"https://data-live.flightradar24.com/clickhandler/?version=1.5&flight=";


        //ctor
        public Dal()
        {
            FlightsDB dbContext = new FlightsDB();
            SeedDataBaseIfEmpty(dbContext).GetAwaiter().GetResult();
        }

        public async Task SeedDataBaseIfEmpty(FlightsDB dbcontext)
        {

            if (dbcontext.UsersAndPasswords.ToList().Count == 0)
            {
                #region add users

                dbcontext.UsersAndPasswords.Add(new User()
                {
                    Id = 1,
                    Username = "User1",
                    Password = "Password1"
                });

                dbcontext.UsersAndPasswords.Add(new User()
                {
                    Id = 2,
                    Username = "User2",
                    Password = "RelayCommander"
                });

                dbcontext.UsersAndPasswords.Add(new User()
                {
                    Id = 3,
                    Username = "Yossi",
                    Password = "Zaguri"
                });
                dbcontext.UsersAndPasswords.Add(new User()
                {
                    Id = 4,
                    Username = "admin",
                    Password = "123"
                });
                #endregion
                await dbcontext.SaveChangesAsync();
            }

        }

        #region get data
        private async Task<string> RequestData(string uri)
        {
            using (var webClient = new System.Net.WebClient())
            {
                return await webClient.DownloadStringTaskAsync(allURL).ConfigureAwait(false);
            }
        }

        private string RequestDataSync(string uri)
        {
            using (var webClient = new System.Net.WebClient())
            {
                return webClient.DownloadString(uri);
            }
        }

        private async Task<dynamic> GetFromApi<dynamic>(String url)
        {
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.GET);
                var response = await client.ExecuteAsync<dynamic>(request);
                return JsonConvert.DeserializeObject<dynamic>(response.Content);
            }
            catch (Exception)
            {
                return JsonConvert.DeserializeObject<dynamic>(string.Empty);
            }
        }
        #endregion

        #region User
        public User CreateUser(String userName, String password) //make sure to check username duplicates in BAL
        {
            //////
            //add serial number for the user id
            /////
            FlightsDB dbContext = new FlightsDB();
            Dictionary<DateTime, FlightInfoPartial> flightsHistory = new();
            User user = new User()
            {
                Username = userName,
                Password = password,
                FlightsHistory = flightsHistory
            };
            dbContext.UsersAndPasswords.Add(user);
            return user;
        }

        public void UpdatePassword(String userName, String password)
        {
            List<User> allUsers = GetAllUsers();
            try
            {
                foreach (User user in allUsers)
                {
                    if (user.Username == userName)
                        user.Password = password;
                }
            }
            catch (Exception)
            {
                throw new NoUserException(userName);
            }
        }

        public User GetUserByUsername(String userName)
        {
            List<User> allUsers = GetAllUsers();
            try
            {
                return allUsers.Find(u => u.Username == userName);
            }
            catch (Exception)
            {
                throw new NoUserException(userName);
            }
        }

        public List<User> GetAllUsers()
        {
            using var ctx = new FlightsDB();
            return ctx.UsersAndPasswords.ToList();
        }

        public List<User> GetUsersAndPasswords()
        {
            var ctx = new FlightsDB();
            return ctx.UsersAndPasswords.ToList();
        }

        public void AddFlightToHistory(User user, FlightInfoPartial flight)
        {
            List<User> users = GetAllUsers();
            foreach (var _user in users)
            {
                if (_user.Id == user.Id)
                    _user.FlightsHistory.Add(DateTime.Today, flight);
            }
        }

        #endregion

        #region Flights

        public List<FlightInfoPartial> GetCurrentFlights()
        {
            JObject AllFlightsData = null;
            //IList<string> keys = null;
            // IList<Object> values = null;


            List<FlightInfoPartial> AllFlights = new List<FlightInfoPartial>();

            using (var webClient = new System.Net.WebClient())
            {
                //async

                //var json = RequestData(allURL); //download  data from url
                //AllFlightsData = JObject.Parse(json.Result);

                //sync
                var json = RequestDataSync(uri);
                AllFlightsData = JObject.Parse(json);
                try
                {
                    foreach (var item in AllFlightsData)
                    {
                        var key = item.Key;
                        if (key == "full_count" || key == "version")
                            continue;
                        if (item.Value[11].ToString() == "TLV" || item.Value[12].ToString() == "TLV")
                            AllFlights.Add(new FlightInfoPartial
                            {
                                Id = -1,
                                Source = item.Value[11].ToString(),
                                Destination = item.Value[12].ToString(),
                                SourceId = key,
                                Longitude = Convert.ToDouble(item.Value[2]),
                                Latitude = Convert.ToDouble(item.Value[1]),
                                DateAndTime = EpochToDateTime.GetDateTimeFromeEpoch(Convert.ToDouble(item.Value[10])),
                                FlightCode = item.Value[13].ToString(),
                                // Location = new GeoCoordinate(Convert.ToDouble(item.Value[2]),
                                //Convert.ToDouble(item.Value[1]))
                            });

                    }
                }
                catch (Exception e)
                {
                    Debug.Print(e.Message);
                }
            }
            return AllFlights;
        }


        /// <summary>
        /// get information about a specific flight when cliking on the flight
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public FlightInfo getSingleFlight(string flightCode)
        {
            FlightInfo flightData = null;
            try
            {
                string url = fLightURL + flightCode;
                var json = RequestDataSync(url); //download  data from url
                flightData = JsonConvert.DeserializeObject<FlightInfo>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });


            }
            catch (Exception e)
            {
                if (flightData == null)
                    flightData = null;
                Debug.Print(e.Message);
            }
            return flightData;
        }
        /*
        public async Task<List<FlightInfoPartial>> GetFlightsFromAPI()
        {
            //link to the list of ALL flights
         //  String uri = @"https://data-cloud.flightradar24.com/zones/fcgi/feed.js?faa=1&bounds=41.13,29.993,25.002,36.383&satellite=1&mlat=1&flarm=1&adsb=1&gnd=1&air=1&vehicles=1&estimated=1&maxage=14400&gliders=1&selected=2d1e1f33&ems=1&stats=1";
// private const string allURL = @"https://data-cloud.flightradar24.com/zones/fcgi/feed.js?faa=1&bounds=41.13,29.993,25.002,36.383&satellite=1&mlat=1&flarm=1&adsb=1&gnd=1&air=1&vehicles=1&estimated=1&maxage=14400&gliders=1&selected=2d1e1f33&ems=1&stats=1";

            try
            {
                var client = new RestClient(allURL)
                {
                    Timeout = -1
                };
                var request = new RestRequest(Method.GET);
                var response = client.ExecuteAsync<List<FlightInfoPartial>>(request); // using the async method caused an error due to synchronization issues
                List<FlightInfoPartial> flightsList = response.Result.Data.ToList();
                foreach (var flight in flightsList)
                {
                    if (flight.Destination != "TLV" && flight.Source != "TLV")
                        flightsList.Remove(flight);
                }
                return flightsList;
            }
            catch (Exception)
            {
                throw new JsonErrorException();
            }
        }
        */
        public Dictionary<DateTime, FlightInfoPartial> GetFlightsHistory(String userName) //make sure the BAL is checking the dates
        {
            if (GetUserByUsername(userName).FlightsHistory != null)
                return GetUserByUsername(userName).FlightsHistory;
            throw new NoFlightsException(userName);
        }

        /// <summary>
        /// get information about a specific flight when cliking on the flight
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        /* public async Task<FlightInfo> GetFlightInfo(FlightInfoPartial flight)
         {
             String fLightURL = $"https://data-live.flightradar24.com/clickhandler/?version=1.5&flight={flight.Id}";
             FlightInfo flightInfo = await GetFromApi<dynamic>(fLightURL);
             return flightInfo;
         }
        */
        #endregion

        #region weather
        public async Task<OpenWeather.Weather> GetWeather(FlightInfo.Airport airport) //by current date
        {
            double lat = airport.destination.position.latitude;
            double lon = airport.destination.position.longitude;
            String weatherURL = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={APIkey}";
            OpenWeather.Weather weather = await GetFromApi<dynamic>(weatherURL);
            return weather;
        }
        #endregion

        #region HebDate
        public async Task<HebCal.Item> GetHebDate(DateTime date) //by current date?
        {
            String hebCalURL = $"https://www.hebcal.com/hebcal?v=1&cfg=json&maj=on&min=on&mod=on&nx=on&year={date.Year}&month={date.Month}&ss=on&mf=on&c=on&geo=geoname&geonameid=3448439&M=on&s=on&start={date.Day}&end={date.AddDays(7)}";
            HebCal.Item hebDate = await GetFromApi<dynamic>(hebCalURL);
            return hebDate;
        }
        #endregion
    }
}
