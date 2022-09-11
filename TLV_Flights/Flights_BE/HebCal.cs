using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights_BE
{
    public class HebCal
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

        public List<Item> items { get; set; }

        public class Item
        {
            public string title { get; set; } //erev chag or chag
            public object date { get; set; }
            public string hdate { get; set; }
            public string category { get; set; }
            public string hebrew { get; set; }
            public string memo { get; set; }
            public string link { get; set; }
            public string title_orig { get; set; }
            public string subcat { get; set; }
        }
    }
}
