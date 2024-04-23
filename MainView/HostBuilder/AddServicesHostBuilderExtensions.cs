using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Services;
using Models.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MainView.HostBuilder
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IDataFromNHTSAService, DataFromNHTSAService>();
                services.AddSingleton<IVINDecoderService, VINDecoderService>();
            });
            return host;
        }
    }
}
