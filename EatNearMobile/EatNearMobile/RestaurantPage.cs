using System;

using Xamarin.Forms;

namespace EatNearMobile
{
	public class RestaurantPage : ContentPage
	{
		public RestaurantPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}

