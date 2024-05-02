using Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services
{
    public interface IVINDecoderService
    {
        Task<VehicleInfo> GetVehicleDataByVIN(string VIN);
    }
}
