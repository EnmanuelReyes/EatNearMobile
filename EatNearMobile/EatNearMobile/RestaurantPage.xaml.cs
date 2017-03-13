using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace EatNearMobile
{
	public partial class RestaurantPage : ContentPage
	{
		public Restaurant Restaurant { get; set; }

		public RestaurantPage(Restaurant restaurant)
		{

			InitializeComponent();
			this.Restaurant = restaurant;
			this.Name.Text = restaurant.Name;
			this.PhoneNumber.Text = restaurant.PhoneNumber;
			this.RestaurantType.Text = restaurant.RestaurantType;
			this.FoodType.Text = restaurant.FoodType;
			this.MinPrice.Text = restaurant.MinPrice;
			this.MaxPrice.Text = restaurant.MaxPrice;
			this.PickerRating.SelectedIndex = restaurant.Rating-1;
}



		void Call(object sender, EventArgs e)
		{
			Device.OpenUri(new Uri("tel:" + Restaurant.PhoneNumber));
		}
		void UpdateRating(object sender, EventArgs e)
		{
			int Rating = PickerRating.SelectedIndex + 1;
            Restaurant.Rating = Rating;
			System.Diagnostics.Debug.WriteLine(Rating);
			System.Diagnostics.Debug.WriteLine("kionda");
            new RestService().UpdateRating(Restaurant);
            DisplayAlert("Votacion", "Se ha enviado su votacion para este restaurante", "OK");

        }
	}
}
