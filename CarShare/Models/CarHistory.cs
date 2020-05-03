using CarShare.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarShare.Models
{
    public enum HireStatus
    {
        Planned,
        Active,
        SuccesfullyReturned,
        NotReturned
    }

    public class CarHistory
    {
        public int Id { get; set; }

        // when the car hire begins
        public DateTime HireTime { get; set; }

        // how long the car is hired for
        public TimeSpan HireDuration { get; set; }

        // the geo position of the car at the start of the hire
        // (just set to any number for now)
        public double InitialLatitude { get; set; }
        public double InitialLongitude { get; set; }

        // the geo position of the car when the hire finished
        public double ReturnedLatitude { get; set; }
        public double ReturnedLongitude { get; set; }

        // when the car was returned
        public DateTime ReturnedTime { get; set; }

        // the status of the hire session
        public HireStatus Status { get; set; }

        //the user that hired the car
        public string UserId { get; set; }
        public CarShareUser User { get; set; }

        // the car that was hired
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
