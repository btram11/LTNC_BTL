using Models.ModelFirebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services.Firebase
{
    public interface IAccountDataService
    {
        Task<List<Account>> GetByUsername(string username);
        Task<List<Account>> GetByEmail(string email);
    }
}
