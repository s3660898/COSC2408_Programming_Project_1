using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CarShare.Models;

namespace CarShare.Identity.Data
{
    public enum UserStatus
    {
        AwaitingHire,
        CurrentlyHiring,
        InvalidEmail,
        Banned
    }
    // Add profile data for application users by adding properties to the CarShareUser class
    public class CarShareUser : IdentityUser
    {
        public ICollection<CarHistory> CarHistories { get; set; }
        public string Address { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}
