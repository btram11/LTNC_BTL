using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Maps.Common;
using GoogleApi.Entities.Maps.Common.Enums;
using GoogleApi.Entities.Maps.DistanceMatrix.Request;
using GoogleApi.Entities.Maps.DistanceMatrix.Response;
using GoogleApi.Exceptions;

namespace Models
{
    public class Manager
    {
        private FleetManager _manager = new FleetManager();
        public List<Trip> TripList { get; set; }

        private string Key = Environment.GetEnvironmentVariable("GOOGLEAPI_KEY");
        public Manager() { }
        public async Task MakeNewTrip(string Origin, string Destination, DateTime DepartureTime)
        {
            
           
            var origin = new Address(Origin);
            var destination = new Address(Destination);
            string name = "minion";
            Console.Write("hello world", Key);
            var request = new DistanceMatrixRequest
            {
                Key = this.Key,
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
            double distance = response.Rows.ElementAt(0).Elements.ElementAt(0).Distance.Value;
            distance /= 1000;

            int day = 0, hour = 0, minute = 0, second = 0;
            second = response.Rows.ElementAt(0).Elements.ElementAt(0).Duration.Value;
            day = (int)Math.Floor(second / (Math.Pow(60, 2) * 24));
            hour = (int)Math.Floor(second / Math.Pow(60, 2));
            minute = second / 60;
            second %= 60;
            TimeSpan duration = new TimeSpan(day, hour, minute, second);
            DateTime ArrivingTime = DepartureTime + duration;

            Trip trip = new Trip {
                Origin = Origin, 
                Destination = Destination, 
                DepartureTime = DepartureTime,
                ArrivalTime = ArrivingTime 
            };



        }
    }
}
