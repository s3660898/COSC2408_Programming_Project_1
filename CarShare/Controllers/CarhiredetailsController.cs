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

        public IActionResult Addhire(AddCarViewModel model)
        {
            Console.WriteLine("FROM Car hire controller");

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
            return RedirectToAction("Carhiredetails", "Index");
        }


    }
}