using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EatNearMobile
{
    public class State
    {
        public static List<Restaurant> Restaurants
        {
            get; set;
        }

        public static Restaurant Get(int id)
        {
            return Restaurants.Find(x => x.Id == id);
        }

    }
}
