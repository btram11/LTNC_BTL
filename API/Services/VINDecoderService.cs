using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Vehicles;
using Newtonsoft.Json;

namespace API.Services
{
    public class VINDecoderService
    {
        //private readonly HttpClient _httpClientFactory;
        //public VINDecoderService(HttpClient httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}
        public async Task<VehicleData> GetVehicleDataByVIN(string VIN)
        {
            if (VIN == string.Empty || VIN.Length < 17)
            {
                throw new Exception("VIN IS NOT AT THE LENGHT OF 17 CHARACTER");
            }
            using (VehicleModelingPrepHttpClient client = new VehicleModelingPrepHttpClient())
            {

                string url = $"decodevinvaluesextended/{VIN}?format=json";
                VehicleData data = await client.GetAsync<VehicleData>(url);

                if (data.Results.ElementAt(0).ErrorCode != "0")
                {
                    throw new Exception(data.Results.ElementAt(0).ErrorText);
                }
                return data;

            }
        }
    }
}
