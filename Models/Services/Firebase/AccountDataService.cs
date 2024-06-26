﻿using Google.Cloud.Firestore;
using Models.ModelFirebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services.Firebase
{
    public class AccountDataService : IAccountDataService
    {
        private readonly FirestoreDb _firestoreDb;

        public AccountDataService(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }
        public async Task<bool> Update(Account entity)
        {
            DocumentReference doc = _firestoreDb.Collection("Users").Document(entity.Id);
            await doc.SetAsync(entity, SetOptions.MergeAll);
            return true;
        }
        public async Task<List<Account>> GetByEmail(string email)
        {
            Query query = _firestoreDb.Collection("Users").WhereEqualTo("Email", email);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();
            List<Account> accounts = snapshot.Documents.Select(x => x.ConvertTo<Account>()).ToList();
            return accounts;
        }

        public async Task<List<Account>> GetByUsername(string username)
        {
            Query query = _firestoreDb.Collection("Users").WhereEqualTo("Username", username);
            QuerySnapshot snapshot = await query.GetSnapshotAsync();
            List<Account> accounts = snapshot.Documents.Select(x => x.ConvertTo<Account>()).ToList();
            return accounts;
        }

        public async Task Register(Account entity)
        {
            DocumentReference doc = _firestoreDb.Collection("Users").Document(entity.Id);
            await doc.SetAsync(entity);
            await AddDataset(entity.DataUid);
        }

        private async Task AddDataset(string id)
        {
            DocumentReference doc = _firestoreDb.Collection("Data").Document(id);

            await doc.SetAsync(new DatasetFirebase());
        }
    }
}
