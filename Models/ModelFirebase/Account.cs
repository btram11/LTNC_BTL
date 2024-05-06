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
    public class Account: IFirebaseEntity
    {
        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [FirestoreProperty]
        public string FirstName { get; set; }

        [FirestoreProperty]
        public string LastName { get; set; }

        [FirestoreProperty]
        public string Email { get; set; }

        [FirestoreProperty]
        public string Password { get; set; }

        [FirestoreProperty]
        public string Username { get; set; }

        [FirestoreProperty]
        public string DataUid { get; set; } = Guid.NewGuid().ToString("N");
    }
}
