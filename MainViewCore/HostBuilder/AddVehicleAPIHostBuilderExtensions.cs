using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using GoogleApi.Extensions;

namespace MainView.HostBuilder
{
    public static class AddVehicleAPIHostBuilderExtensions
    {
        public static IHostBuilder AddVehicleAPI(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddHttpClient<VehicleModelingPrepHttpClient>(c =>
                {
                    c.BaseAddress = new Uri("https://vpic.nhtsa.dot.gov/api/vehicles/");
                    c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                });
                services.AddHttpClient<FuelModelingHttpClient>(c =>
                {
                    c.BaseAddress = new Uri("https://luatvietnam.vn/bang-gia-xang-dau-hom-nay.html");
                });
                services.AddGoogleApiClients();
            });
            return host;
        }
    }
}
