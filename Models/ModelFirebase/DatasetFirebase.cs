using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelFirebase
{
    [FirestoreData]
    public class DatasetFirebase : IFirebaseEntity
    {
        public string Id { get; set; }

        [FirestoreProperty]
        public int CountVehicles { get; set; } = 0;

        [FirestoreProperty]
        public int CountDrivers { get; set; } = 0;

        [FirestoreProperty]
        public int CountTrips { get; set; } = 0;

    }
}
