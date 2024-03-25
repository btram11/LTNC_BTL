using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum TripStatus
    {
        Scheduled,
        InProgress,
        Completed
    }
    public class Trip
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; }
        public decimal EstimatedCost { get; set; }
        public TripStatus Status { get; set; } // Scheduled, In Progress, Completed
        public Driver driver { get; set; }
        public Vehicle vehicle { get; set; }

        public Trip(string origin, string destination, DateTime departureTime, DateTime arrivalTime, decimal estimatedCost)
        {
            Origin = origin;
            Destination = destination;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            EstimatedCost = estimatedCost;
            Status = 0; // Default status
        }
    }
}
