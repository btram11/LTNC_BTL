using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services.PasswordHash
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string HashedPassword, string inputPassword);
    }
}
