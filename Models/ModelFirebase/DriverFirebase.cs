﻿using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelFirebase
{
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
        public string DateOfBirth { get; set; }

        [FirestoreProperty]
        public string Gender { get; set; }

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
        public int CompletedTrip { get; set; } = 0;

        [FirestoreProperty]
        public int OngoingTrip { get; set; } = 0;

        [FirestoreProperty]
        public List<Dictionary<string, object>> OngoingTripList { get; set; }
    }
}