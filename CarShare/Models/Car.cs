using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string Registration { get; set; }
        [Required]
        public string Description { get; set; }
        public CarStatus Status { get; set; }
        public CarCategory Category { get; set; }
        public int NumSeats { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }

        public ICollection<CarHistory> CarHistories { get; set; }
        [Required]
        public int ImageId { get; set; }
        [Required]
        public Image Image { get; set; }
        public int? ParkingLotId { get; set; }
        public ParkingLot ParkingLot { get; set; }
    }
}
