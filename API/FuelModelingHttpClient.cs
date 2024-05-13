using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class FuelModelingHttpClient
    {
        private readonly HttpClient _httpClient;
        public FuelModelingHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string jsonRespone = await response.Content.ReadAsStringAsync();

            return jsonRespone;
        }
    }
}
