using System;
using System.Collections.Generic;
using System.Linq;
using CarShare.Data;
using CarShare.Models;
using System.Threading.Tasks;
using CarShare.Data;
using CarShare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarShare.Identity.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// Creates the controller for the Admin side of the program, and the database the UserList will read from
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

        // Initiates the UserManagement page and sets the data to be displayed
        public IActionResult UserManagement()
        {
            // getting ordered list from db
            var UserList = _db.Users.ToList();

            // putting relevant data in ViewBag
            ViewBag.UserList = new List<UserViewModel>();
            foreach (CarShareUser c in UserList)


            // Adds the UserList data in a new UserViewModel; Id, Email, Address, UserStatus
            {
                ViewBag.UserList.Add(new UserViewModel() { Id = c.Id, Email = c.Email, Address = c.Address, UserStatus = c.UserStatus });
            }
            // Returns the UserManagement page view
            return View();
        }

        // Initiates ViewUser via their numerical Id's and returns the View
        public IActionResult ViewUser(string Id)
        {
            var user = _db.Users.SingleOrDefault(c => c.Id == Id);
            return View(user);
        }

        public IActionResult EditUserDetails(string Id)
        {
            var user = _db.Users.SingleOrDefault(c => c.Id == Id);
            return View(user);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult EditUserDetails(CarShareUser model)
        {
            var user = _db.Users.SingleOrDefault(c => c.Id == model.Id);
            if (model.Email != null)
                user.Email = model.Email;
            if (model.Address != null)
                user.Address = model.Address;
            user.UserStatus = model.UserStatus;

            _db.Users.Update(user);
            _db.SaveChanges();
            return RedirectToAction("UserManagement", "Admin");
        }

        public IActionResult DeleteUser(string Id)
        {
            var user = _db.Users.SingleOrDefault(c => c.Id == Id);
            return View(user);
        }

        public IActionResult DeletingUser(string Id)
        {
            var user = _db.Users.SingleOrDefault(c => c.Id == Id);

            _db.Users.Remove(user);
            _db.SaveChanges();
            return RedirectToAction("UserManagement", "Admin");
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
                Category = model.Category,
                Status = CarStatus.Available,
                Latitude = 0,
                Longitude = 0
            };

            _db.Cars.Add(c);
            _db.SaveChanges();

            return RedirectToAction("FleetManagement", "Admin");
        }
    }
}
