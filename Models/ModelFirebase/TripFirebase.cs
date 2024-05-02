using Google.Cloud.Firestore;
using Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelFirebase
{
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
        public double EstimatedCost { get; set; }

        [FirestoreProperty]
        public double Duration { get; set; }

        [FirestoreProperty]
        public Timestamp ScheduledDepartureTime { get; set; }

        [FirestoreProperty]
        public Timestamp DepartureTime { get; set; }

        [FirestoreProperty]
        public Timestamp ScheduledArrivalTime { get; set; }

        [FirestoreProperty]
        public Timestamp ArrivalTime { get; set; }

        [FirestoreProperty]
        public bool HasTrailer { get; set; }

        [FirestoreProperty]
        public bool Returned { get; set; }

        [FirestoreProperty]
        public Dictionary<string, object> Driver { get; set; }

        [FirestoreProperty]
        public Dictionary<string, object> Vehicle { get; set; }

        [FirestoreProperty]
        public Dictionary<string, object> Trailer { get; set; }

    }
}
