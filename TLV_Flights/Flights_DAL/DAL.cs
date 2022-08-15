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
            var client = new RestClient(uri)
            {
                Timeout = -1
            };

            var request = new RestRequest(Method.GET);
            var response = client.ExecuteAsync<List<FlightInfoPartial>>(request); // using the async method caused an error due to synchronization issues
           

         


       
            List<FlightInfoPartial> flightsList = response.
            foreach (var flight in response.Result)
            { 
                if(flight)
            }
            return await;
        }//need to figure out

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

        public User GetUser(String userName)
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
            if(GetUser(userName).FlightsHistory != null)
                return GetUser(userName).FlightsHistory;
            throw new NoFlightsException(userName);
        }

        public FlightInfo GetFlightInfo(String flightID)
        {

        }

        public double GetWeather(String location) //by current date
        {

        }

        public bool GetHebDate() //by current date
        {
            //לבדוק מה אמור להחזיר
        }

    }
}
