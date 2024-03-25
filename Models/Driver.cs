using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum StatusDriver
    {
        Free,
        Occupied,
        Offwork
    }
    public class Driver
    {
        public string Name { get; set; }
        public string Telephone { get; set; }
        public DriverLicense license { get; }
        public string HomeAddress { get; set; }
        public string Nationality { get; set; }
        public StatusDriver statusDriver {  get; set; }

        public List<Trip> tripList { get; set; }

        public Driver(string Name, string Telephone, string HomeAddress, string Nationality)
        {
            this.Name = Name;
            this.Telephone = Telephone;
            this.HomeAddress = HomeAddress;
            this.Nationality = Nationality;
            this.statusDriver = StatusDriver.Free;
            this.tripList = new List<Trip>();
        }

        public void addTrip(Trip trip)
        {
            this.tripList.Add(trip);
        }
    }
}
