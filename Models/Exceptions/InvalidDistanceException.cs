using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Exceptions
{
    public class InvalidDistanceException : Exception
    {
        public InvalidDistanceException(string message) : base(message)
        {
            
        }
    }
}
