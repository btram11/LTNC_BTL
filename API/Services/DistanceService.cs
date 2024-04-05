using GoogleApi.Entities.Maps.Common;
using GoogleApi.Entities.Maps.DistanceMatrix.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Maps.Common.Enums;
using GoogleApi.Entities.Maps.DistanceMatrix.Response;
using GoogleApi.Exceptions;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Xml.Linq;

namespace API.Services
{
    public class DistanceService
    {
        public static async Task<Element> GetDistance(string Origin, string Destination)
        {
            var origin = new Address(Origin);
            var destination = new Address(Destination);
            var request = new DistanceMatrixRequest
            {
                Key = Environment.GetEnvironmentVariable("GOOGLEAPI_KEY"),
                Origins = new[]
                {
                    new LocationEx(origin)
                },
                Destinations = new[]
                {
                    new LocationEx(destination)
                }
            };
            var response = await GoogleApi.GoogleMaps.DistanceMatrix.QueryAsync(request);

            if (response.Status == Status.Ok)
            {
                return response.Rows.ElementAt(0).Elements.ElementAt(0);
            }
            throw new Exception("Ble Ble Ble Ble");
        }
    }
}
