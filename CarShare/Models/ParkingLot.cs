using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShare.Models
{
    public class ParkingLot
    {
        public int Id { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
