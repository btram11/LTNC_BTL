using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Vehicles;
using Models.Services;
using Newtonsoft.Json;

namespace API.Services
{
    public class VINDecoderService: IVINDecoderService
    {
        private readonly VehicleModelingPrepHttpClient _client;
        public VINDecoderService(VehicleModelingPrepHttpClient Client)
        {
            _client = Client;
        }
        public async Task<VehicleInfo> GetVehicleDataByVIN(string VIN)
        {
            string url = $"decodevinvaluesextended/{VIN}?format=json";
            VehicleData data = await _client.GetAsync<VehicleData>(url);

            if (!data.Results.ElementAt(0).ErrorCode.Contains("0"))
            {
                throw new Exception(data.Results.ElementAt(0).ErrorText);
            }
            return data.Results.ElementAt(0);
        }
    }
}
