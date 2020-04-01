using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShareMockups.Models
{
        public enum UserStatus
        {
            CurrentlyHiring,
            Valid,
            InvalidEmail,
            Banned
        }

        public class UserViewModel
        {
            public String Fullname { get; set; }
            public Enum UserStatus { get; set; }
            public String LastUseDate { get; set; }
            public String LastVehicleUsed { get; set; }
    }
}
