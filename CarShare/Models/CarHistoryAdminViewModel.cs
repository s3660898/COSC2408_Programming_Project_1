using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShare.Models
{
    public class CarHistoryAdminViewModel
    {
        public string Email { get; set; }
        public DateTime HireTime { get; set; }
        public TimeSpan HireDuration { get; set; }
        public float InitialLongitude { get; set; }
        public float InitialLatitude { get; set; }
        public float ReturnedLongitude { get; set; }
        public float ReturnedLatitude { get; set; }
        public HireStatus Status { get; set; }

    }
}
