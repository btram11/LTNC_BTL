﻿using Google.Cloud.Firestore;
using Models;
using Models.ModelFirebase;
using Models.Services;
using Models.Services.AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.State.Accounts;

namespace ViewModels.State.Authentication
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountStore _accountStore;

        //private readonly FirestoreProvider _firestoreProvider;

        public Account CurrentAccount
        {
            get
            {
                return CurrentAccount;
            }
            private set
            {
                _accountStore.CurrentAccount = value;
                StateChanged?.Invoke();
            }
        }
        public bool _isLogin => CurrentAccount != null;
        public event Action StateChanged;

        public Authenticator(IAuthenticationService authenticationService, IAccountStore accountStore/*, FirestoreProvider firestoreProvider*/)
        {
            _authenticationService = authenticationService;
            _accountStore = accountStore;
            //_firestoreProvider = firestoreProvider;
        }

        public async Task Login(string username, string password)
        {
            CurrentAccount = await _authenticationService.Login(username, password);
        }

        public void LogOut()
        {
            CurrentAccount = null;
        }
    }
}
