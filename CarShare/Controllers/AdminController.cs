using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShare.Data;
using CarShare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarShare.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FleetManagement()
        {
            // getting ordered list from db
            var CarList = _db.Cars.ToList().OrderBy(x => (int)(x.Status)).ToList();

            // putting relevant data in ViewBag
            ViewBag.CarList = new List<CarFleetViewModel>();
            foreach (Car c in CarList)
            {
                ViewBag.CarList.Add(new CarFleetViewModel() {Id=c.Id, Registration = c.Registration, Description = c.Description, Status = c.Status});
            }

            return View();
        }

        public IActionResult ViewCar(int Id)
        {
            var car = _db.Cars.SingleOrDefault(c => c.Id == Id);

            return View(car);
        }

        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddCar(AddCarViewModel model)
        {
            Car c = new Car()
            {
                Registration = model.Registration,
                Description = model.Description,
                Status = model.Status,
                Category = model.Category,
                NumSeats = model.NumSeats,
                Latitude = 0,
                Longitude = 0
            };

            _db.Cars.Add(c);
            _db.SaveChanges();

            return RedirectToAction("FleetManagement", "Admin");
        }

        public IActionResult EditCar(int Id)
        {
            var car = _db.Cars.SingleOrDefault(c => c.Id == Id);
            return View(car);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditCar(Car model)
        {
            var car = _db.Cars.SingleOrDefault(c => c.Id == model.Id);

            car.Description = model.Description;
            car.Registration = model.Registration;
            car.Status = model.Status;
            car.Category = model.Category;
            car.NumSeats = model.NumSeats;

            _db.Cars.Update(car);
            _db.SaveChanges();

            return RedirectToAction("FleetManagement", "Admin");
        }

        public IActionResult DeleteCarConfirmation(int Id)
        {
            var car = _db.Cars.SingleOrDefault(c => c.Id == Id);
            return View(car);
        }

        public IActionResult DeleteCar(int Id)
        {
            var car = _db.Cars.SingleOrDefault(c => c.Id == Id);

            _db.Cars.Remove(car);
            _db.SaveChanges();

            return RedirectToAction("FleetManagement", "Admin");
        }
    }
}
