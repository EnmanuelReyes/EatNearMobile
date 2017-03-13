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
			var Restaurant = new Restaurant
			{
				Name = "Kueno",
				PhoneNumber = "8094834309",
				FoodType = "Frita",
				Rating = "3",
				MaxPrice = "250",
				MinPrice = "100",
				RestaurantType = "Expreso"
			};
			Button button = new Button();
			button.Text = Restaurant.Name;
			var Command = new Command(() =>
			{
				ViewRestaurant(Restaurant);
			});
				button.Command = Command;
			      
			StackLayout.Children.Add(button);

		}

		void ViewRestaurant(Restaurant Restaurant)
		{
			App.NavigationPage.Navigation.PushAsync(new RestaurantPage(Restaurant));
			App.MenuIsPresented = false;
		}
	}
}
