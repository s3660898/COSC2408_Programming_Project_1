using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CarShare.Identity.Data;
using CarShare.Models;

namespace CarShare.Data
{
    // Builds the data for the CarShareUser database which the AdminController reads from
    public class ApplicationDbContext : IdentityDbContext<CarShareUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Image> Images { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarHistory> CarHistory { get; set; }
        public DbSet<ParkingLot> ParkingLots { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
