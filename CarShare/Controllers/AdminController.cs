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
using X.PagedList;
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
                ViewBag.CarList.Add(new CarFleetViewModel() { Id = c.Id, Registration = c.Registration, Description = c.Description, Status = c.Status });
            }

            return View();
        }

        public async Task<IActionResult> ViewCar(int Id)
        {
            var car = _db.Cars.SingleOrDefault(c => c.Id == Id);

            var carHistories = _db.CarHistory.Where(a => a.CarId == Id).OrderBy(a => a.Id).ToList();
            var users = _db.Users.ToList<CarShareUser>();

            var query = from user in users
                        join history in carHistories on user equals history.User
                        select new CarHistoryAdminViewModel()
                        {
                            Email = user.Email,
                            HireTime = history.HireTime,
                            HireDuration = history.HireDuration,
                            InitialLongitude = history.InitialLongitude,
                            InitialLatitude = history.InitialLatitude,
                            ReturnedLongitude = history.ReturnedLongitude,
                            ReturnedLatitude = history.ReturnedLatitude,
                            Status = history.Status
                        };

            ViewBag.carHistory = query.ToList();

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

            if (model.Description != null)
                car.Description = model.Description;
            if (model.Registration != null)
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
        public IActionResult ParkingLotManagement()
        {
            // getting ordered list from db
            var ParkingLotList = _db.ParkingLots.ToList();

            // putting relevant data in ParkingLot ViewBag
            ViewBag.ParkingLotList = new List<ParkingLot>();
            foreach (ParkingLot c in ParkingLotList)


            // Adds the ParkingLotList data in a new ParkingLot Model; Id, Latitude, Longitude, Address, Description
            {
                ViewBag.ParkingLotList.Add(new ParkingLot() { Id = c.Id, Latitude = c.Latitude, Longitude = c.Longitude, Address = c.Address, Description = c.Description });
            }
            // Returns the ParkingLotManagement page view
            return View();
        }
        // Initiates ViewParkingLot via their numerical Id's and returns the View
        public IActionResult ViewParkingLot(int Id)
        {
            var ParkingLot = _db.ParkingLots.SingleOrDefault(c => c.Id == Id);
            return View(ParkingLot);
        }
        /* Initiates the AddParkingLot page, and allows for Parking Lot data, Latitude, Longitude, Address and Description, to be entered,
        adds the data to the ParkingLots database and then returns the Admin to the ParkingLotManagementPage*/
        public IActionResult AddParkingLot()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddParkingLot(ParkingLot model)
        {
            ParkingLot c = new ParkingLot()
            {
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Address = model.Address,
                Description = model.Description,
                
            };
            _db.ParkingLots.Add(c);
            _db.SaveChanges();

            return RedirectToAction("ParkingLotManagement", "Admin");
        }
        // Initiates the EditParkingLot page, and allows for the ParkingLot data to be displayed
        public IActionResult EditParkingLot(int Id)
        {
            var ParkingLot = _db.ParkingLots.SingleOrDefault(c => c.Id == Id);
            return View(ParkingLot);
        }

        [HttpPost, ValidateAntiForgeryToken]
        /* Initiates the AddParkingLot page, and allows for Parking Lot data, Latitude, Longitude, Address and Description, to be entered,
        adds the data to the ParkingLots database and then returns the Admin to the ParkingLotManagementPage*/
        public IActionResult EditParkingLot(ParkingLot model)
        {
            var ParkingLot = _db.ParkingLots.SingleOrDefault(c => c.Id == model.Id);
            ParkingLot.Latitude = model.Latitude;
            ParkingLot.Longitude = model.Longitude;
            if (model.Address != null)
                ParkingLot.Address = model.Address;
            if (model.Description != null)
                ParkingLot.Description = model.Description;

            _db.ParkingLots.Update(ParkingLot);
            _db.SaveChanges();
            return RedirectToAction("ParkingLotManagement", "Admin");
        }


        // Initiates the DeleteParkingLot page, and allows for the ParkingLot data to be displayed
        public IActionResult DeleteParkingLot(int Id)
        {
            var ParkingLot = _db.ParkingLots.SingleOrDefault(c => c.Id == Id);
            return View(ParkingLot);
        }
    }
}