using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShare.Data;
using CarShare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;


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

        public async Task<IActionResult> ViewCar(int Id)
        {
            var car = _db.Cars.SingleOrDefault(c => c.Id == Id);


            var cars = _db.Cars.OrderBy(x => x.Id).ToList();

            const int pageSize = 50;

            var carHistory = _db.CarHistory.Where(a => a.CarId == Id).OrderBy(a => a.Id).ToList();

            ViewBag.carHistory = carHistory;

            return View(car);

            /*
            ViewBag.car = car;

            if (Id == null)
            {
                var pagedList = await _db.CarHistory.Where(a => a.CarId == 1).OrderBy(a => a.Id).ToPagedListAsync(page, pageSize);
                return View(pagedList);
            }
            else
            {
                var pagedList1 = await _db.CarHistory.Where(a => a.CarId == Id).OrderBy(a => a.Id).ToPagedListAsync(page, pageSize);
                return View(pagedList1);
            }
            */
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
