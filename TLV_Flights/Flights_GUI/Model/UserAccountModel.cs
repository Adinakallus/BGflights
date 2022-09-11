using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flights_BE;
using Flights_BL;

namespace Flights_GUI.Model
{
    public class UserAccountModel
    {
        public class accountModel
        {
            #region properties
            IBL iBL; //poiter to get data from bl
            public Flights_BE.User user;
            public string userName
            {
                get
                {
                    return this.user.Username;
                }
                set
                {
                    this.user.Username = value;
                }
            }
            public string password
            {
                get
                {
                    return this.user.Password;
                }
                set
                {
                    this.user.Password = value;
                }
            }
            #endregion

            #region constractor
            public accountModel()
            {
                BLFactory factory = new BLFactory();
                iBL = factory.getInstacne(); //get from factory
                this.user = new Flights_BE.User();
                this.user.FlightsHistory = new Dictionary<DateTime,FlightInfoPartial>();
            }

            #endregion

            #region checks
            public bool alreadyExist()
            {
                //check in data if user already exist.
                //return true if user exist.
                if (this.iBL.GetUserByUsername(this.user.Username))
                    return true;
                return false;
            }
            #endregion

            #region signUp
            public bool signUp()
            {
                //TODO
                // if saving data sucssfull ->return true
                if (this.iBL.CreateUser(user.Username, user.Password))
                    return true;
                return false;
            }
            #endregion

            #region logIn
            public bool logIn()
            {
                User loginUser = this.iBL.UserLogin(this.user);
                if (loginUser == null)
                    return false;
                this.user = loginUser;
                return true;
            }
            #endregion

            #region save
            public void saveChanges(List<Flights_BE.FlightInfo> listToSave)
            {

                this.user.FlightsHistory = new Dictionary<DateTime, FlightInfoPartial>();

                foreach (var item in listToSave)
                {
                    this.user.FlightsHistory.Add(DateTime.Now, new FlightInfoPartial { FlightCode = item.identification.id });
                }
                this.iBL.saveChanges(this.user);
            }
            #endregion

            #region history
            public List<FlightInfo> GetUserHistory()
            {
                List<FlightInfo> flights = new List<FlightInfo>();

                foreach (var item in this.user.FlightsHistory)
                {
                    flights.Add(iBL.GetSingleFlight(item.Value.FlightCode));
                }
                return flights;
            }
            #endregion
        }

    }
}
