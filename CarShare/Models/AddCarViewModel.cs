using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShare.Models
{
    public class AddCarViewModel
    {
        public string Registration { get; set; }
        public string Description { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public CarStatus Status { get; set; }
        public CarCategory Category { get; set; }
        public int NumSeats { get; set; }
    }
}
