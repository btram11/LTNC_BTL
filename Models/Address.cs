using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Address_Local
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public Address_Local(string street, string city, string state, string country, string PostalCode = "")
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
        }
    }
}
