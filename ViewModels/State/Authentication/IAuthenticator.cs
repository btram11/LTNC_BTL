using Models;
using Models.ModelFirebase;
using Models.Services.AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.State.Authentication
{
    public interface IAuthenticator
    {
        Account CurrentAccount { get; }
        bool _isLogin {  get; }
        event Action StateChanged;

        Task Login(string username, string password);
        Task<RegistrationResult> Register(Account newUser);
        void LogOut();
    }
}
