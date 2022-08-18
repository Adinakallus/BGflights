using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Json.Net;
using Flights_BE;

namespace Flights_DAL
{
    public class Dal
    {
        private const String APIkey = "464586a1b635bd8df1683892c3b27dd6"; // <= API key 

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

        public async Task<dynamic> GetFromApi<dynamic>(String url)
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

        public List<User> GetUsersAndPasswords()
        {
            var ctx = new FlightsDB();
            return ctx.UsersAndPasswords.ToList();
        }

        public async Task<List<FlightInfoPartial>> GetFlightsFromAPI()
        {
            //link to the list of ALL flights
            String uri =
                $"https://data-cloud.flightradar24.com/zones/fcgi/feed.js?faa=1&bounds=55.428%2C47.202%2C-8.085%2C7.845&satellite=1&mlat=1&flarm=1&adsb=1&gnd=1&air=1&vehicles=1&estimated=1&maxage=14400&gliders=1&stats=1";
            try
            {
                var client = new RestClient(uri)
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
            catch(Exception)
            {
                throw new JsonErrorException();
            }
        }

        public void CreateUser(String userName, String password) //make sure to check username duplicates in BAL
        {
            FlightsDB dbContext = new FlightsDB();
            Dictionary<DateTime, FlightInfoPartial> flightsHistory = new();
            dbContext.UsersAndPasswords.Add(new User()
            {
                Username = userName,
                Password = password,
                FlightsHistory = flightsHistory
            });
        }

        public List<User> GetAllUsers()
        {
            using var ctx = new FlightsDB();
            return ctx.UsersAndPasswords.ToList();
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
        public async Task<FlightInfo> GetFlightInfo(FlightInfoPartial flight)
        {
            String fLightURL = $"https://data-live.flightradar24.com/clickhandler/?version=1.5&flight={flight.Id}";
            FlightInfo flightInfo = await GetFromApi<dynamic>(fLightURL);
            return flightInfo;
        }

        public async Task<OpenWeather.Weather> GetWeather(FlightInfo.Airport airport) //by current date
        {
            double lat = airport.destination.position.latitude;
            double lon = airport.destination.position.longitude;
            String weatherURL = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={APIkey}";
            OpenWeather.Weather weather = await GetFromApi<dynamic>(weatherURL);
            return weather;
        }

        public async Task<HebCal.Item> GetHebDate(DateTime date) //by current date?
        {
            String hebCalURL = $"https://www.hebcal.com/hebcal?v=1&cfg=json&maj=on&min=on&mod=on&nx=on&year={date.Year}&month={date.Month}&ss=on&mf=on&c=on&geo=geoname&geonameid=3448439&M=on&s=on&start={date.Day}&end={date.AddDays(7)}";
            HebCal.Item hebDate = await GetFromApi<dynamic>(hebCalURL);
            return hebDate;
        }
    }
}
