using Models.ModelFirebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<Account> Login(string username, string password);
    }
}
