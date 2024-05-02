
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelFirebase
{
    public enum Test
    {
        Approved,
        Disapproved,
        Check
    }

    [FirestoreData]
    public class VehicleFirebase : IVehicleDataFirebase
    {
        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public string VIN {  get; set; }

        [FirestoreProperty]
        public string LicensePlate { get; set; }

        [FirestoreProperty]
        public string VehicleStatus { get; set; }

        [FirestoreProperty]
        public string VehicleType { get; set; }

        [FirestoreProperty]
        public string Make { get; set; }

        [FirestoreProperty]
        public string Models { get; set; }

        [FirestoreProperty]
        public string Year { get; set; }

        [FirestoreProperty]
        public string Trim { get; set; }

        [FirestoreProperty]
        public string RegisState { get; set; }

        [FirestoreProperty]
        public string Ownership { get; set; }




        [FirestoreProperty]
        public double FuelEfficiency { get; set; }

        [FirestoreProperty]
        public int FuelCapacity { get; set; }

        [FirestoreProperty]
        public string EnginePower { get; set; }

        [FirestoreProperty]
        public string EngineSize { get; set; }

        [FirestoreProperty]
        public string EngineCylinders { get; set; }

        [FirestoreProperty]
        public float? GCWR { get; set; } //Gross Combined Weight Rating

        [FirestoreProperty]
        public float GVWR { get; set; } //Gross Vehicle Weight Rating

        [FirestoreProperty]
        public float CurbWeight { get; set; }

        [FirestoreProperty]
        public string Color { get; set; }

        [FirestoreProperty]
        public string BodyType { get; set; }

        [FirestoreProperty]
        public string BusType { get; set; }

        [FirestoreProperty]
        public string DisplacementCC { get; set; }

        [FirestoreProperty]
        public string DisplacementCI { get; set; }

        [FirestoreProperty]
        public string DisplacementL {  get; set; }

        [FirestoreProperty]
        public float? Length { get; set; }

        [FirestoreProperty]
        public int TotalSeats { get; set; }

        [FirestoreProperty]
        public string FuelTypePrimary { get; set; }


        [FirestoreProperty]
        public DocumentReference AttachedVehicle { get; set; }

        [FirestoreProperty]
        public int OngoingTrip { get; set; } = 0;

        [FirestoreProperty]
        public List<Dictionary<string, object>> OngoingTripList { get; set; }

        [FirestoreProperty]
        public Test test { get; set; }
    }
}
