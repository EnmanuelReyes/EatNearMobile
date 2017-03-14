using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace EatNearMobile
{
    public partial class MenuPage : ContentPage
    {


        public MenuPage()
        {
            BindingContext = new MenuPageViewModel();
            Title = "Menu";
            InitializeComponent();
            CallApi();
        }

        async void CallApi()
        {
            var RestService = new RestService();
            List<Restaurant> task = await RestService.GetRestaurants();

            LoadButtons(task);

        }

        void LoadButtons(List<Restaurant> Restaurants)
        {

            foreach (var Restaurant in Restaurants)
            {
                Button button = new Button();
                button.Text = Restaurant.Name;
                var Command = new Command(() =>
                {
                    ViewRestaurant(Restaurant);
                });
                button.Command = Command;

                StackLayout.Children.Add(button);

            }
        }

        void ViewRestaurant(Restaurant Restaurant)
        {
            App.NavigationPage.Navigation.PushAsync(new RestaurantPage(Restaurant));
            App.MenuIsPresented = false;
        }
    }
}
