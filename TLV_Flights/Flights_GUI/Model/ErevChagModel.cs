using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flights_BE;
using Flights_BL;

namespace Flights_PL.Model
{
    public class ErevChagModel
    {
        private readonly BAL _bal;
        public ErevChagModel()
        {
            _bal = new BAL();
        }
        public async Task<bool> isErevChag()
        {
            return await _bal.isErevChag();
        }

    }
}
