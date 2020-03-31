using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShareMockups.Models
{
        public enum UserStatus
        {
            RentingVehicle,
            NotCurrentlyRenting,
            Banned
        }

        public class UserViewModel
        {
            public String Fullname { get; set; }
            public String Password { get; set; }
            public Enum UserStatus { get; set; }
        }
}
