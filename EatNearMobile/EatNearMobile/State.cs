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
            get
            {
                if (Restaurants == null)
                {
                    CallApi();
                }
                return Restaurants;
            }
            set { Restaurants = value; }
        }
        public static RestService _RestService = new RestService();
        static State()
        {
            CallApi();
        }
        public static Restaurant Get(int id)
        {
            return Restaurants.Find(x => x.Id == id);
        }

        static async void CallApi()
        {

            Restaurants = await _RestService.GetRestaurants();

        }
    }
}
