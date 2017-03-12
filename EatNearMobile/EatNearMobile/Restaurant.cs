using System;
namespace EatNearMobile
{
	public class Restaurant
	{
		public Restaurant()
		{
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public string Rating { get; set; }
		public string MinPrice { get; set; }
		public string MaxPrice { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string RestaurantType { get; set; }
		public string FoodType { get; set; }



	}
}
