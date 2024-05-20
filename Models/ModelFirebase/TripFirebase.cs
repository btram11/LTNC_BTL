using Google.Cloud.Firestore;
using Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelFirebase
{
    enum TripStatus
    {
        Scheduled,
        Cancelled,
        EnRoute,
        Completed,
    }

    [FirestoreData]
    public class TripFirebase : IFirebaseEntity
    {
        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [FirestoreProperty]
        public string Status { get; set; }

        [FirestoreProperty]
        public string Origin { get; set; }

        [FirestoreProperty]
        public string Destination { get; set; }

        [FirestoreProperty]
        public double Distance { get; set; }

        //[FirestoreProperty]
        //public int EstimateDistance { get; set; }

        [FirestoreProperty]
        public double EstimatedCost { get; set; }

        [FirestoreProperty]
        public int WeightPassenger { get; set; }


        [FirestoreProperty]
        public double Duration { get; set; }

        [FirestoreProperty]
        public string ScheduledDepartureTime { get; set; }

        [FirestoreProperty]
        public string DepartureTime { get; set; }

        [FirestoreProperty]
        public string ScheduledArrivalTime { get; set; }

        [FirestoreProperty]
        public string ArrivalTime { get; set; }

        [FirestoreProperty]
        public bool HasTrailer { get; set; }

        [FirestoreProperty]
        public bool Returned { get; set; }

        [FirestoreProperty]
        public string TransportationType { get; set; }

        [FirestoreProperty]
        public Dictionary<string, string> Driver { get; set; }

        [FirestoreProperty]
        public Dictionary<string, string> Vehicle { get; set; }

        [FirestoreProperty]
        public Dictionary<string, string> Trailer { get; set; }

    }
}
