using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShare.Models
{
    public class CarHireBrowseViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int NumSeats { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string ImageUrl { get; set; }
    }
}
