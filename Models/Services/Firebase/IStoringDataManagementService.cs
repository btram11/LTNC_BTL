using Google.Cloud.Firestore;
using Models.ModelFirebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services.Firebase
{
    public interface IStoringDataManagementService
    {
        string DataUid { get; set; }
        Task<IReadOnlyCollection<T>> GetList<T>(Query query) where T : IFirebaseEntity;

        Task AddOrUpdateVehicle(IVehicleDataFirebase vehicle);
        Task AddOrUpdateDriver(DriverFirebase vehicle);
        Task AddOrUpdateTrip(TripFirebase trip);

        Task<DriverFirebase> GetDriverById(string id);
        Task<IReadOnlyCollection<DriverFirebase>> GetAllDrivers();
        Task<T> GetVehicleById<T>(string id) where T : IVehicleDataFirebase;
        Task<List<IVehicleDataFirebase>> GetAllVehicles();
        Task<TripFirebase> GetTripById(string id);
        Task<IReadOnlyCollection<TripFirebase>> GetAllTrips();


        Task<IReadOnlyCollection<DriverFirebase>> WhereEqualToDriver(string fieldPath, object value);
        Task<IReadOnlyCollection<T>> WhereEqualToVehicle<T>(string fieldPath, object value) where T : IVehicleDataFirebase;
        Task<IReadOnlyCollection<VehicleFirebase>> WhereNotEqualToVehicle(string fieldPath, object value);
        Task<IReadOnlyCollection<TrailerFirebase>> WhereEqualToTrailer(string fieldPath, object value);

        Task<IReadOnlyCollection<TripFirebase>> WhereEqualToTrip(string fieldPath, object value);
    }
}
