using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShare.Data;
using CarShare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarShare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiCarHireController : ControllerBase
    {
        ApplicationDbContext _db;

        public ApiCarHireController(ApplicationDbContext db)
        {
            _db = db;
        }

        public CarHireBrowseViewModel[] Get(int? Category = -1, int? NumSeats = -1)
        {
            List<Car> cars = _db.Cars.ToList();

            // if specified category
            if (Category != -1)
                cars = cars.Where(c => c.Category == (CarCategory)Category).ToList();

            // if specified seats number
            if (NumSeats != -1)
                cars = cars.Where(c => c.NumSeats >= NumSeats).ToList();

            List<CarHireBrowseViewModel> carVMs = new List<CarHireBrowseViewModel>();
            foreach(Car c in cars)
            {
                carVMs.Add(new CarHireBrowseViewModel()
                {
                    Id = c.Id,
                    Description = c.Description,
                    Category = c.Category.ToString(),
                    NumSeats = c.NumSeats,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude
                });
            }
            return carVMs.ToArray();
        }
    }
}