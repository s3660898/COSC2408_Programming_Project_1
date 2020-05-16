using CarShare.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShare.Models
{
    // Displayed public fields for users and their getters/setters: Id, Email, Address, UserStatus
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}
