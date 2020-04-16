using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CarShare.Identity.Data
{
    public enum UserStatus
    {
        CurrentlyHiring,
        AwaitingHire,
        InvalidEmail,
        Banned
    }
    // Add profile data for application users by adding properties to the CarShareUser class
    public class CarShareUser : IdentityUser
    {
        public string Address { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}
