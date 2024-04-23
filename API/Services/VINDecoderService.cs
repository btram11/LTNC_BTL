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
        public async Task<VehicleData> GetVehicleDataByVIN(string VIN)
        {
            //if (VIN == string.Empty || VIN.Length < 17)
            //{
            //    throw new Exception("VIN IS NOT AT THE LENGHT OF 17 CHARACTER");
            //}
            string url = $"decodevinvaluesextended/{VIN}?format=json";
            VehicleData data = await _client.GetAsync<VehicleData>(url);

            //if (data.Results.ElementAt(0).ErrorCode != "0")
            //{
            //    throw new Exception(data.Results.ElementAt(0).ErrorText);
            //}
            return data;
        }
    }
}
