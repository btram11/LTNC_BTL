using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Maps.Common;
using GoogleApi.Entities.Maps.Common.Enums;
using GoogleApi.Entities.Maps.DistanceMatrix.Request;
using GoogleApi.Entities.Maps.DistanceMatrix.Response;
using GoogleApi.Exceptions;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Models.Vehicles;
using API.Services;
using API;
using Models;
using System.Xml.Linq;

namespace ViewModels
{
    public class Manager
    {
        private FleetManager _manager = new FleetManager();
        public List<Trip> TripList { get; set; }

        private string Key = Environment.GetEnvironmentVariable("GOOGLEAPI_KEY");
        public Manager() { }
        public async void MakeNewTrip(string Origin, string Destination, DateTime DepartureTime)
        {
            try
            {
                Element element = await DistanceService.GetDistance(Origin, Destination);

                double distance = element.Distance.Value;
                distance /= 1000;

                int day = 0, hour = 0, minute = 0, second = 0;
                second = element.Duration.Value;
                day = (int)Math.Floor(second / (Math.Pow(60, 2) * 24));
                hour = (int)Math.Floor(second / Math.Pow(60, 2));
                minute = second / 60;
                second %= 60;
                TimeSpan duration = new TimeSpan(day, hour, minute, second);
                DateTime ArrivingTime = DepartureTime + duration;

                Trip trip = new Trip
                {
                    Origin = Origin,
                    Destination = Destination,
                    DepartureTime = DepartureTime,
                    duration = duration,
                    _Distance = distance,
                };
                _manager.addTrip(trip, TripType.people);
            }
            catch (Exception ex)
            {
                // Xử lý
            }
        }

        public void CreateVehicle()
        {

        }
        public void GetInfoFromVIN(string VIN)
        {
            try
            {
                #region Comment Lỗi
                //string url = $"https://vpic.nhtsa.dot.gov/api/vehicles/decodevinvaluesextended/{VIN}?format=json";
                //var serviceCollection = new ServiceCollection();
                //ConfigureService(serviceCollection);
                //var services = serviceCollection.BuildServiceProvider();
                //var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
                //var client = httpClientFactory.CreateClient();
                //client.BaseAddress = new Uri(url);
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //var temp = client.GetAsync(url);
                //var tmp = temp.Result;
                //if (tmp.IsSuccessStatusCode)
                //{
                //    var result = tmp.Content.ReadAsStringAsync();
                //    string html = result.Result;
                //    VehicleData newObj = JsonConvert.DeserializeObject<VehicleData>(html);
                //    Console.WriteLine(newObj.Results.ElementAt(0).Seats);
                //}
                //else
                //{
                //    throw new Exception("Lỗi rồi sửa đi");
                //}
                #endregion
                //VINDecoderService service = new VINDecoderService();
                //VehicleAPI Vehicle = service.GetVehicleDataByVIN(VIN).Result.Results.ElementAt(0);
                //new VINDecoderService().GetVehicleDataByVIN(VIN);

                //if (Vehicle.ErrorCode.Contains(";") == false)
                //{
                //    int errorCode = int.Parse(Vehicle.ErrorCode);
                //    switch (errorCode)
                //    {
                //        case 0:

                //            break;
                //        case 1:
                //            break;
                //        case 2:
                //            break;
                //        case 3:
                //            break;
                //        case 4:
                //            break;
                //        case 5:
                //            break;
                //        case 6:
                //            break;
                //        case 7:
                //            break;
                //        case 8:
                //            break;
                //        case 9:
                //            break;
                //        case 10:
                //            break;
                //        case 11:
                //            break;
                //        case 12:
                //            break;
                //        case 14:
                //            break;
                //        case 400:
                //            break;
                //    }
                //}


            }
            catch (HttpRequestException e)
            {

            }
            catch (Exception e)
            {

            }
        }

        //private static void ConfigureService(ServiceCollection services)
        //{
        //    services.AddHttpClient();
        //}
    }
}
