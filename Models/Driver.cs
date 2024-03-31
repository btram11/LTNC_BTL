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
        public Address_Local homeAddress { get; set; }
        public string Nationality { get; set; }
        public StatusDriver statusDriver {  get; set; }
        public int leave {  get; set; }
        public string salary { get; set; }
        protected List<Trip> tripList { get; set; }

        public Driver(string Name, string Telephone, Address_Local HomeAddress, string Nationality)
        {
            this.Name = Name;
            this.Telephone = Telephone;
            this.homeAddress = HomeAddress;
            this.Nationality = Nationality;
            this.statusDriver = StatusDriver.Free;
            this.tripList = null;
            this.leave = 0;
        }
   
        public void addTrip(Trip trip)
        {
            this.tripList.Add(trip);
        }
    }
}
