using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShare.Models
{
    public class CarHistoryUserViewModel
    {
        public string Registration { get; set; }
        public string Description { get; set; }
        public DateTime HireTime { get; set; }
        public TimeSpan HireDuration { get; set; }
        public float InitialLongitude { get; set; }
        public float InitialLatitude { get; set; }
        public string ReturnAddress { get; set; }
        public DateTime ReturnedTime { get; set; }
        public float ReturnedLongitude { get; set; }
        public float ReturnedLatitude { get; set; }
        public HireStatus Status { get; set; }

    }
}
