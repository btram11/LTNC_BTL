using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Vehicles;

namespace Models
{
    public enum TripStatus
    {
        Scheduled,
        InProgress,
        Completed
    }
    public enum TripType
    {
        freight,
        people
    }
    public class Trip
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime => DepartureTime + duration;
        public TimeSpan duration { get; set; }
        public decimal EstimatedCost { get; set; }
        public TripStatus Status { get; set; } // Scheduled, In Progress, Completed
        public Driver driver { get; set; }
        public Vehicle vehicle { get; set; }

        public double _Distance { get; set; }

        public int _Load { get; set; }


        //public Trip(string origin, string destination, DateTime departureTime, DateTime arrivalTime)
        //{
        //    Origin = origin;
        //    Destination = destination;
        //    DepartureTime = departureTime;
        //    ArrivalTime = arrivalTime;
        //    //EstimatedCost = estimatedCost;
        //    Status = 0; // Default status
        //}
        public Trip()
        {
            Status = TripStatus.Scheduled;
        }
        public void addVehicleDriver(Driver curDriver, Vehicle curVehicle)
        {
            driver = curDriver;
            vehicle = curVehicle;
        }

        public void updateStatus(TripStatus newStatus)
        {
            this.Status = newStatus;
        }

        public void updateDuration(TimeSpan duration)
        {
            this.duration = duration;
        }
    }
}
