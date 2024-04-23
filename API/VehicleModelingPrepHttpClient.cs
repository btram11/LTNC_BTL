using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class VehicleModelingPrepHttpClient
    {
        private readonly HttpClient _httpClient;

        public VehicleModelingPrepHttpClient(HttpClient httpClient)
        {
            //this.BaseAddress = new Uri("https://vpic.nhtsa.dot.gov/api/vehicles/");
            //this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string jsonRespone = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(jsonRespone);
        }
    }
}
