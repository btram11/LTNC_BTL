using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelFirebase
{
    [FirestoreData]
    public class TrailerFirebase : IVehicleDataFirebase
    {
        private float _curbWeight;
        private float _GVWR;

        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [FirestoreProperty]
        public string Name { get; set; } 

        [FirestoreProperty]
        public string VIN { get; set; }

        [FirestoreProperty]
        public string LicensePlate { get; set; } 

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
        public float PayloadCapacity { get; set; } 

        [FirestoreProperty]
        public float GVWR
        {
            get => _GVWR;
            set
            {
                _GVWR = value;
                UpdatePayloadCapacity();
            }
        }

        [FirestoreProperty]
        public float CurbWeight
        {
            get => _curbWeight;
            set 
            { 
                _curbWeight = value; 
                UpdatePayloadCapacity();
            }
        }

        [FirestoreProperty]
        public string Color { get; set; } 

        [FirestoreProperty]
        public string BodyType { get; set; } 

        [FirestoreProperty]
        public string TrailerType { get; set; } 

        [FirestoreProperty]
        public float? Length { get; set; } 

        [FirestoreProperty]
        public string RegisState { get; set; } 

        [FirestoreProperty]
        public string Ownership { get; set; }

        [FirestoreProperty]
        public DocumentReference AttachedVehicle { get; set; }

        [FirestoreProperty]
        public int OngoingTrip { get; set; } = 0;

        [FirestoreProperty]
        public List<Dictionary<string, object>> OngoingTripList { get; set; }

        [FirestoreProperty]
        public bool Returned { get; set; }

        [FirestoreProperty]
        public bool IsOperating { get; set; } = false;

        [FirestoreProperty]
        public Dictionary<string, object> Driver { get; set; } = null;

        private void UpdatePayloadCapacity ()
        {
            PayloadCapacity = GVWR - CurbWeight;
        }

    }
}
