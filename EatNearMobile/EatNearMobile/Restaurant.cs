using Newtonsoft.Json;
using System;
namespace EatNearMobile
{
	public class Restaurant
	{
		public Restaurant()
		{
		}

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty("vote")]
        public int Rating { get; set; }
        [JsonProperty("minPrice")]
        public string MinPrice { get; set; }
        [JsonProperty("maxPrice")]
        public string MaxPrice { get; set; }
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
		public string RestaurantType { get; set; }
		public string FoodType { get; set; }



	}
}
