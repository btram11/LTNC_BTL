using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelFirebase
{
    public enum Gender
    {
        Male,
        Female
    }

    public enum DriverStatus
    {
        Available,      // Sẵn sàng
        OnDuty,         // Đang làm việc
        OffDuty,        // Nghỉ ngơi
        InTransit,      // Đang vận chuyển
        OnLeave,
    }


    [FirestoreData]
    public class DriverFirebase : IFirebaseEntity
    {
        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [FirestoreProperty]
        public string FirstName { get; set; }

        [FirestoreProperty]
        public string LastName { get; set; }

        [FirestoreProperty]
        public int Age { get; set; } 

        [FirestoreProperty]
        public string DateOfBirth { get; set; }

        [FirestoreProperty]
        public Gender Gender { get; set; }

        [FirestoreProperty]
        public DriverStatus Status { get; set; }

        [FirestoreProperty]
        public string ID { get; set; }
        
        [FirestoreProperty]
        public string PlaceOfIssue { get; set; }

        [FirestoreProperty]
        public string Phone { get; set; }

        [FirestoreProperty]
        public string Email { get; set; }

        [FirestoreProperty]
        public string Address { get; set; }

        [FirestoreProperty]
        public string City { get; set; }

        [FirestoreProperty]
        public string Country { get; set; }

        [FirestoreProperty]
        public string DrivingLicenseClass { get; set; }

        [FirestoreProperty]
        public string DrivingLicenseNumber { get; set; }

        [FirestoreProperty]
        public string Education { get; set; }

        [FirestoreProperty]
        public string CriminalRecord { get; set; }

        [FirestoreProperty]
        public string Health { get; set; }

        [FirestoreProperty]
        public string ExYear{ get; set; }

        [FirestoreProperty]
        public int CompletedTrip { get; set; } = 0;

        [FirestoreProperty]
        public int OngoingTrip { get; set; } = 0;

        [FirestoreProperty]
        public List<Dictionary<string, object>> OngoingTripList { get; set; }

        [FirestoreProperty]
        public Dictionary<string, object> Vehicle { get; set; } = null;


    }
}
