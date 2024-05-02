using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using GoogleApi.Entities.Maps.Directions.Response;
using Models.ModelFirebase;
using Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services.Firebase
{
    public class StoringDataManagementService : IStoringDataManagementService
    {
        private readonly FirestoreDb _firestoreDb;
        private string _datauid;

        CollectionReference driversRef => _firestoreDb.Collection("Data").Document(_datauid).Collection("Drivers");
        CollectionReference vehiclesRef => _firestoreDb.Collection("Data").Document(_datauid).Collection("Vehicles");
        CollectionReference tripRef => _firestoreDb.Collection("Data").Document(_datauid).Collection("Trips");

        public string DataUid 
        { 
            get => _datauid; 
            set
            {
                _datauid = value;
            } 
        }

        public StoringDataManagementService(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public async Task AddOrUpdateVehicle(IVehicleDataFirebase vehicle)
        {
            //DocumentReference doc = vehiclesRef.Document(vehicle.Id);
            var doc = _firestoreDb.Collection("Data").Document(vehicle.Id);
            await doc.SetAsync(vehicle);
        }

        public async Task AddToArrayVehicle(string documentPath, string arrayField, object newValue)
        {
            DocumentReference docRef = vehiclesRef.Document(documentPath);

            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                { arrayField, FieldValue.ArrayUnion(newValue) }
            };
            await docRef.UpdateAsync(updates);
        }

        public async Task AddOrUpdateDriver(DriverFirebase driver)
        {
            //DocumentReference doc = driversRef.Document(driver.Id);
            var doc = _firestoreDb.Collection("Data").Document(driver.Id);
            await doc.SetAsync(driver);
        }

        public async Task AddOrUpdateTrip(TripFirebase trip)
        {
            //DocumentReference doc = tripRef.Document(trip.Id);
            var doc = _firestoreDb.Collection("Data").Document(trip.Id);
            await doc.SetAsync(trip);
        }

        public async Task<DriverFirebase> GetDriverById(string id)  
        {
            //DocumentReference doc = driversRef.Document(id);
            var doc = _firestoreDb.Collection("Data").Document(id);
            var snapshot = await doc.GetSnapshotAsync();
            return snapshot.ConvertTo<DriverFirebase>();
        }
        public async Task<IReadOnlyCollection<DriverFirebase>> GetAllDrivers()
        {
            var snapshot = await driversRef.GetSnapshotAsync();
            return snapshot.Documents.Select(x => x.ConvertTo<DriverFirebase>()).ToList();
        }
        public async Task<T> GetVehicleById<T>(string id) where T : IVehicleDataFirebase
        {
            //DocumentReference doc = vehiclesRef.Document(id);
            var doc = _firestoreDb.Collection("Data").Document(id);
            var snapshot = await doc.GetSnapshotAsync();
            return snapshot.ConvertTo<T>();
        }
        public async Task<TripFirebase> GetTripById(string id)
        {
            DocumentReference doc = tripRef.Document(id);
            var snapshot = await doc.GetSnapshotAsync();
            return snapshot.ConvertTo<TripFirebase>();
        }

       
        public async Task<IReadOnlyCollection<DriverFirebase>> WhereEqualToDriver(string fieldPath, object value) 
        {
            //return await GetList<DriverFirebase>(_firestoreDb.Collection("Data").Document(_datauid).Collection("Drivers").WhereEqualTo(fieldPath, value));
            return await GetList<DriverFirebase>(_firestoreDb.Collection("Data").WhereEqualTo(fieldPath, value));
        }
        public async Task<IReadOnlyCollection<VehicleFirebase>> WhereEqualToVehicle(string fieldPath, object value)
        {
            //return await GetList<VehicleFirebase>(_firestoreDb.Collection("Data").Document(_datauid).Collection("Vehicles").WhereEqualTo(fieldPath, value));
            return await GetList<VehicleFirebase>(_firestoreDb.Collection("Data").WhereEqualTo(fieldPath, value));
        }
        public async Task<IReadOnlyCollection<VehicleFirebase>> WhereNotEqualToVehicle(string fieldPath, object value)
        {
            //return await GetList<VehicleFirebase>(_firestoreDb.Collection("Data").Document(_datauid).Collection("Vehicles").WhereNotEqualTo(fieldPath, value));
            return await GetList<VehicleFirebase>(_firestoreDb.Collection("Data").WhereNotEqualTo(fieldPath, value));
        }
        public async Task<IReadOnlyCollection<TrailerFirebase>> WhereEqualToTrailer(string fieldPath, object value)
        {
            //return await GetList<VehicleFirebase>(_firestoreDb.Collection("Data").Document(_datauid).Collection("Vehicles").WhereEqualTo(fieldPath, value));
            return await GetList<TrailerFirebase>(_firestoreDb.Collection("Data").WhereEqualTo("VehicleType", "Trailer").WhereGreaterThanOrEqualTo(fieldPath, value));
        }
        public async Task<IReadOnlyCollection<T>> WhereEqualToVehicle<T>(string fieldPath, object value) where T : IVehicleDataFirebase
        {
            //return await GetList<T>(_firestoreDb.Collection("Data").Document(_datauid).Collection("Vehicles").WhereEqualTo(fieldPath, value));
            return await GetList<T>(_firestoreDb.Collection("Data").WhereEqualTo(fieldPath, value));
        }

        public async Task<IReadOnlyCollection<TripFirebase>> WhereEqualToTrip(string fieldPath, object value)
        {
            return await GetList<TripFirebase>(_firestoreDb.Collection("Data").WhereEqualTo(fieldPath, value));
        }
        public async Task<IReadOnlyCollection<T>> GetList<T>(Query query) where T : IFirebaseEntity
        {
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
        }

        

        

        
    }
}
