using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EatNearMobile
{
	public class RestService
	{
		HttpClient client;
		public RestService()
		{
			client = new HttpClient();
			client.DefaultRequestHeaders.Add("Authorization", "Bearer 1abf3aa7-3e8d-40b2-a9ab-0dd7cd4727ab");

		}

		public void LoadToken(string token)
		{
			client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
		}

		public async Task<List<Restaurant>> GetRestaurants()
		{
			var Restaurants = new List<Restaurant>();
			var uri = new Uri(string.Format(Constants.RestUrl, "/restaurants"));
			var response = await client.GetAsync(uri);
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				Restaurants = JsonConvert.DeserializeObject<List<Restaurant>>(content);
			}
			return Restaurants;
		}
	}
}
