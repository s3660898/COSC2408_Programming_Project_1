using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShare.Models
{
    public enum CarStatus
    {
        InUse,
        Available,
        Suspended
    }

    public enum CarCategory
    {
        Passenger,
        Van,
        Pickup,
        Luxury,
        Sport
    }

    public class Car
    {
        public int Id { get; set; }
        public string Registration { get; set; }
        public string Description { get; set; }
        public CarStatus Status { get; set; }
        public CarCategory Category { get; set; }
        public int NumSeats { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
    }
}
