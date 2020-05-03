using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShare.Data;
using CarShare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using X.PagedList;
using System.Linq;
using System.Globalization;

namespace CarShare.Controllers
{
    public class CarhiredetailsController : Controller
    {
        private ApplicationDbContext _db;

        private ILogger<HistoryController> _logger;

        public CarhiredetailsController(ApplicationDbContext db, ILogger<HistoryController> _logger)
        {
            _db = db;
            _logger = _logger;
        }
        public IActionResult Index(int id)
        {
           
             
            var Car = _db.Cars.Where(x => x.Id == id).OrderBy(x => x.Id).ToList();

            ViewBag.car = Car;
            return View();
        }

        //public IActionResult Addhire(AddCarViewModel model)
        public IActionResult Addhire(int id, string datepicker, string timepicker, string datepicker1, string timepicker1, string drop_off)
        {
            Console.WriteLine("FROM Car hire controller");
            Console.WriteLine(datepicker);
            Console.WriteLine(timepicker);
            Console.WriteLine(datepicker1);
            Console.WriteLine(timepicker1);
            Console.WriteLine(drop_off);

            
            string one = datepicker;
            string two = timepicker;

            DateTime dt = Convert.ToDateTime(one + " " + two);
            
            Console.WriteLine(dt);

            DateTime dt1 = Convert.ToDateTime(datepicker1 + " " + timepicker1);

            Console.WriteLine(dt1);

            TimeSpan ts = new TimeSpan(0, 2, 8);
            string s = new DateTime(ts.Ticks).ToString("mm:ss");

            CarHistory CH = new CarHistory()
            {
                HireTime = dt,
                HireDuration = TimeSpan.Parse(s),
                InitialLatitude = 37.74,
                InitialLongitude = 145.00,
                ReturnedLatitude = 37.74,
                ReturnedLongitude = 145.00,
                Status = 0,
                ReturnedTime = dt1,
                UserId = "fbd8b270-41ef-49c5-adbd-ac324c0b1ddb",
                CarId = id
            };


            try
            {
                _db.CarHistory.Add(CH);
                _db.SaveChanges();
            } catch (Exception e) {
                Console.WriteLine("ERROR while saving");
            }



            /*
            Car c = new Car()
            {
                Registration = model.Registration,
                Description = model.Description,
                Category = model.Category,
                Status = CarStatus.Available,
                Latitude = 0,
                Longitude = 0
            };

            _db.Cars.Add(c);
            _db.SaveChanges();

            return RedirectToAction("FleetManagement", "Admin");
            */


            return RedirectToAction( "Index", "Carhiredetails");
            //return View("Index");
        }


    }
}