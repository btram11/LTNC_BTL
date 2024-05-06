using Google.Cloud.Firestore;
using Models.Exceptions;
using Models.ModelFirebase;
using Models.Services.Firebase;
using Models.Services.PasswordHash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Models.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountDataService _accountDataService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenticationService(IAccountDataService accountDataService, IPasswordHasher passwordHasher)
        {
            _accountDataService = accountDataService;
            _passwordHasher = passwordHasher;
        }
        public async Task<Account> Login(string username, string password)
        {
            var pattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                              @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                              @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            var regex = new Regex(pattern);

            List<Account> accounts = null;
            if (regex.IsMatch(username))
            {
                accounts = await _accountDataService.GetByEmail(username);
            }
            else accounts = await _accountDataService.GetByUsername(username);
            if (accounts == null || accounts.Count == 0 || accounts.Count > 1)
            {
                throw new UserNotFoundException(username);
            }
            if (!_passwordHasher.Verify(accounts.FirstOrDefault().Password, password))
            {
                throw new InvalidPasswordException(username, password);
            }
            return accounts.First();
        }

        public async Task<RegistrationResult> Register(Account newUser)
        {
            List<Account> accounts = await _accountDataService.GetByUsername(newUser.Username);
           
            if (accounts.Count >= 1)
            {
                return RegistrationResult.UsernameAlreadyExists;
            }
            accounts = await _accountDataService.GetByEmail(newUser.Email);
            if (accounts.Count >= 1)
            {
                throw new Exception("This Username is not available");
            }
            newUser.Password = _passwordHasher.Hash(newUser.Password);
            await _accountDataService.Register(newUser);
            return RegistrationResult.Success;
        }
    }
}
