using Google.Cloud.Firestore;
using Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelFirebase
{
    public enum VehicleStatus
    {
        Available,          
        NotAvailable,
        Scheduled,
        OnGoing,
        Repairing,
        Returning,
        UnderMaintenance,
        OutOfService,
    }
    public interface IVehicleDataFirebase : IFirebaseEntity
    {
        int OngoingTrip { get; set; }
        string Name { get; set; }
        string VIN { get; set; }
        string LicensePlate { get; set; }
        VehicleStatus? VehicleStatus { get; set; }
        string VehicleType { get; set; }
        string Make { get; set; }
        string Models { get; set; }
        string Year { get; set; }
        float GVWR { get; set; }
        float CurbWeight { get; set; }
        string Color { get; set; }
        string BodyType { get; set; }
        float? Length { get; set; }

        bool IsOperating { get; set; }

        Dictionary<string, object> Driver { get; set; }
        //DocumentReference AttachedVehicle { get; set; }
    }
}
