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
        List<CustomPin> customPins;
        RestService _RestService;

        public MapPage()
        {
            InitializeComponent();
			_RestService = new RestService();
            CallApi();
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(18.479896, -69.921108), Distance.FromMiles(4.0)));
        }

        async void CallApi()
        {
            List<Restaurant> task = await _RestService.GetRestaurants();

            LoadMarkers(task);

        }

        async void LoadMarkers(List<Restaurant> restaurants)
        {
            customMap.CustomPins = new List<CustomPin>();
            foreach (var r in restaurants)
            {
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
        public async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            List<Restaurant> restaurants = await _RestService.GetRecommendedRestaurants();
            if (restaurants.Count <= 0)
            {
                await DisplayAlert("Sin Recomendaciones", "Actualmente no tenemos ninguna recomendacion para usted."
                    + " Intente mas tarde", "OK");
            }
            else
            {
                await App.NavigationPage.Navigation.PushAsync(new RestaurantPage(restaurants[0]));
                App.MenuIsPresented = false;
            }

        }
    }
}
