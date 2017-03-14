using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;

namespace EatNearMobile
{
    public class RestService
    {
        HttpClient client;
        public RestService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer 5913b341-f871-4bf2-9125-d9abb4feac97");

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

        public async void UpdateRating(Restaurant res)
        {
            var uri = new Uri(string.Format(Constants.RestUrl, "/restaurantvote"));

            Dictionary<string, object> RestaurantVote = new Dictionary<string, object>();
            RestaurantVote.Add("vote", res.Rating);
            RestaurantVote.Add("restaurant", new Dictionary<string, object> {
                {"id", res.Id}
            });
            var json = JsonConvert.SerializeObject(RestaurantVote);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await client.PostAsync(uri, content);


            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"             Rating guardado correctamente.");

            }

        }

        public async Task<List<Restaurant>> GetRecommendedRestaurants()
        {
            var Restaurants = new List<Restaurant>();
            var uri = new Uri(string.Format(Constants.RestUrl, "/user/recommendation"));
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
