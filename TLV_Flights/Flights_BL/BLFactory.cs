using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights_BL
{
    public class BLFactory
    {
        BAL instance;
        public BLFactory()
        {
            if (this.instance == null)
            {
                this.instance = new BAL();
            }
        }

        public BAL getInstacne()
        {
            return this.instance;

        }
    }
}
