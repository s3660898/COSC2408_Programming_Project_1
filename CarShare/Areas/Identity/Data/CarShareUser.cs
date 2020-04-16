using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CarShare.Identity.Data
{
    // Add profile data for application users by adding properties to the CarShareUser class
    public class CarShareUser : IdentityUser
    {
        public string CustomTag { get; set; }
    }
}
