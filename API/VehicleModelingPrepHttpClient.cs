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
    public class VehicleModelingPrepHttpClient : HttpClient
    {
        public VehicleModelingPrepHttpClient()
        {
            this.BaseAddress = new Uri("https://vpic.nhtsa.dot.gov/api/vehicles/");
            this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task<T> GetAsync<T>(string url)
        {
            var response = await this.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string jsonRespone = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(jsonRespone);
        }
    }
}
