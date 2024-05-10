
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
        public string LicensePlate { get; set; } =  string.Empty;

        [FirestoreProperty]
        public VehicleStatus VehicleStatus { get; set; }

        [FirestoreProperty]
        public string VehicleType { get; set; }

        [FirestoreProperty]
        public string Make { get; set; }

        [FirestoreProperty]
        public string Models { get; set; }

        [FirestoreProperty]
        public string Year { get; set; }

        [FirestoreProperty]
        public string Trim { get; set; } = string.Empty;

        [FirestoreProperty]
        public string RegisState { get; set; } =  string.Empty;

        [FirestoreProperty]
        public string Ownership { get; set; } = string.Empty;




        [FirestoreProperty]
        public double FuelEfficiency { get; set; }

        [FirestoreProperty]
        public int FuelCapacity { get; set; }

        [FirestoreProperty]
        public string EnginePower { get; set; } = string.Empty;

        [FirestoreProperty]
        public string EngineSize { get; set; } = string.Empty;

        [FirestoreProperty]
        public string EngineCylinders { get; set; } = string.Empty;

        [FirestoreProperty]
        public float? GCWR { get; set; } //Gross Combined Weight Rating 

        [FirestoreProperty]
        public float GVWR { get; set; } //Gross Vehicle Weight Rating

        [FirestoreProperty]
        public float CurbWeight { get; set; }

        [FirestoreProperty]
        public string Color { get; set; } = string.Empty;

        [FirestoreProperty]
        public string BodyType { get; set; } = string.Empty;

        [FirestoreProperty]
        public string BusType { get; set; } = string.Empty;

        [FirestoreProperty]
        public string DisplacementCC { get; set; } = string.Empty;

        [FirestoreProperty]
        public string DisplacementCI { get; set; } = string.Empty;

        [FirestoreProperty]
        public string DisplacementL {  get; set; } = string.Empty;

        [FirestoreProperty]
        public float? Length { get; set; }

        [FirestoreProperty]
        public int TotalSeats { get; set; } = 0;

        [FirestoreProperty]
        public string FuelTypePrimary { get; set; } = string.Empty;


        [FirestoreProperty]
        public DocumentReference AttachedVehicle { get; set; } = null;

        [FirestoreProperty]
        public int OngoingTrip { get; set; } = 0;

        [FirestoreProperty]
        public List<Dictionary<string, object>> OngoingTripList { get; set; } = null;

        [FirestoreProperty]
        public bool IsOperating { get; set; } = false;

        [FirestoreProperty]
        public Dictionary<string, object> Driver { get; set; } = null;
    }
}
