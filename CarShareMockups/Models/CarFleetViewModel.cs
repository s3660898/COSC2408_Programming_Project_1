using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShareMockups.Models
{
    public enum Status
    {
        Available,
        InUse,
        Suspended
    }

    public class CarFleetViewModel
    {
        public String Registration { get; set; }
        public String Make { get; set; }
        public Enum Status { get; set; }
    }
}
