using Models;
using Models.ModelFirebase;
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


        Task Login(string username, string password);
        void LogOut();
    }
}
