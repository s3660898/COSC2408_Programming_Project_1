using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CarShare.Data;
using CarShare.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace CarShare.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            Console.WriteLine("From Index");
            Console.WriteLine(User.Identity);
            return View();
        }

        public IActionResult CarHireBrowse()
        {
            return View();
        }

        public IActionResult CarHire(int Id)
        {
            Car car = _db.Cars.Where(c => c.Id == Id).FirstOrDefault();
            ViewBag.car = car;

            // Image
            Image img = _db.Images.SingleOrDefault(i => i.Id == car.ImageId);
            ViewBag.ImageTitle = img.Title;
            ViewBag.ImageUrl = string.Format("data:image/jgp;base64,{0}", Convert.ToBase64String(img.Data));

            // Parking lot details
            ParkingLot pl = _db.ParkingLots.Where(p => p.Id == car.ParkingLotId).FirstOrDefault();
            ViewBag.plLatitude = pl.Latitude;
            ViewBag.plLongitude = pl.Longitude;
            ViewBag.plAddress = pl.Address;

            return View();
        }
    }
}
