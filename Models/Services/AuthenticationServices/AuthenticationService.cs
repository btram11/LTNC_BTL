using Google.Cloud.Firestore;
using Models.ModelFirebase;
using Models.Services.Firebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountDataService _accountDataService;

        public AuthenticationService(IAccountDataService accountDataService)
        {
            _accountDataService = accountDataService;
        }
        public async Task<Account> Login(string username, string password)
        {
            List<Account> accounts = await _accountDataService.GetByUsername(username);
            if (accounts.Count == 0 || accounts.Count > 1)
            {
                throw new Exception("Account dont exist");
            }
            if (accounts.First().Password != password)
            {
                throw new Exception("Account dont exist!");
            }
            return accounts.First();
        }
    }
}
