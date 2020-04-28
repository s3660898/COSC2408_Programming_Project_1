using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShare.Models
{
    public class CarFleetViewModel
    {
        public int Id { get; set; }
        public string Registration { get; set; }
        public string Description { get; set; }
        public CarStatus Status { get; set; }
    }
}
