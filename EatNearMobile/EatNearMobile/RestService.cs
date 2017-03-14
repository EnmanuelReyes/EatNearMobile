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
        public static string token { get; set; }
        public RestService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

        }

        public async Task<string> Login(string user, string password)
        {
            string token = null;
            Dictionary<string, string> map = new Dictionary<string, string>();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Basic YWNtZTphY21lc2VjcmV0");
            string payload = string.Format("username={0}&password={1}&grant_type=password", user, password);
            var content = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");

            var uri = new Uri(string.Format(Constants.Url, "/oauth/token"));
            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                map = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);
                return map["access_token"];
            }
            return null;
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
            try
            {
                var uri = new Uri(string.Format(Constants.RestUrl, "/user/recommendation"));
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Restaurants = JsonConvert.DeserializeObject<List<Restaurant>>(content);
                }
            }
            catch (Exception ignored)
            {

            }

            return Restaurants;

        }
    }
}
