using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
namespace EatNearMobile
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
			var pin = new CustomPin
			{
				Pin = new Pin
				{
					Type = PinType.Place,
					Position = new Position(18.479896, -69.921108),
					Label = "Xamarin San Francisco Office",
					Address = "394 Pacific Ave, San Francisco CA"
				},
				Restaurant = new Restaurant
				{
					Name = "Kueno",
					PhoneNumber = "8094834309",
					FoodType = "Frita",
					Rating = "3",
					MaxPrice = "250",
					MinPrice = "100",
					RestaurantType = "Expreso"
				}
			};

			customMap.CustomPins = new List<CustomPin> { pin };
			customMap.Pins.Add(pin.Pin);
			//CallApi();
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(18.479896, -69.921108), Distance.FromMiles(4.0)));


        }

		async void CallApi()
		{
			var RestService = new RestService();
			List<Restaurant> task = await RestService.GetRestaurants();

			LoadMarkers(task);

		}

		void LoadMarkers(List<Restaurant> restaurants) {
			customMap.CustomPins = new List<CustomPin>();

			foreach (var r in restaurants) { 
				var pin = new CustomPin
				{
					Pin = new Pin
					{
						Type = PinType.Place,
						Position = new Position(r.Latitude, r.Longitude),
						Label = r.Name,
						Address = r.Address
					},
					Restaurant = r
				};
				customMap.Pins.Add(pin.Pin);
				customMap.CustomPins.Add(pin);

			}
		}
    }
}
