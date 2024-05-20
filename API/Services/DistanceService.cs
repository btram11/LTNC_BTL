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
using GoogleApi;
using GoogleApi.Interfaces.Maps;
using Models.Services;
using GoogleApi.Entities.Maps.Directions.Request;
using GoogleApi.Entities.Maps.Directions.Response;
using Microsoft.Extensions.Configuration;
using Models.Exceptions;

namespace API.Services
{
    public class DistanceService: IDistanceService
    {
        private IDirectionsApi _directionsApi;
        private IConfiguration _configuration;

        public DistanceService(IDirectionsApi distanceMatrixApi, IConfiguration configuration)
        {
            _directionsApi = distanceMatrixApi;
            _configuration = configuration;
        }
        public async Task<Leg> GetDistance(string Origin, string Destination)
        {
            var origin = new Address(Origin);
            var destination = new Address(Destination);
            var request = new DirectionsRequest
            {
                Key = _configuration["GoogleMapsAPI"],
                Origin = new LocationEx(origin),
                Destination =  new LocationEx(destination)
            };
            DirectionsResponse response = await _directionsApi.QueryAsync(request);
            
            if (response.Status == Status.Ok)
            {
                return response.Routes.First().Legs.First();
            }
            throw new InvalidDistanceException("Invalid distances");
        }
    }
}
