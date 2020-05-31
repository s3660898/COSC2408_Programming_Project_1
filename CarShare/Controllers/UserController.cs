using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CarShare.Data;
using CarShare.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace CarShare.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        ApplicationDbContext _db;
        private IHttpContextAccessor _hca;

        public UserController(ApplicationDbContext db, IHttpContextAccessor hca)
        {
            _db = db;
            _hca = hca;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            //Console.WriteLine("From Index");
            //Console.WriteLine(User.Identity);
            return View();
        }

        public IActionResult CarHireBrowse()
        {
            return View();
        }

        public IActionResult CarHire(int Id, string PickUpTime, string DropOffTime)
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

            // misc
            ViewBag.PickUpTime = PickUpTime;
            ViewBag.DropOffTime = DropOffTime;

            return View();
        }

        public IActionResult HireCarSubmit(int Id, string PickUpTime, string DropOffTime)
        {
            Car car = _db.Cars.Where(c => c.Id == Id).FirstOrDefault();
            ParkingLot pl = _db.ParkingLots.Where(p => p.Id == car.ParkingLotId).FirstOrDefault();
            DateTime pickUpDateTime = DateTime.Parse(PickUpTime);
            DateTime dropOffDateTime = DateTime.Parse(DropOffTime);
            TimeSpan duration = dropOffDateTime.Subtract(pickUpDateTime);

            CarHistory history = new CarHistory()
            {
                HireTime = pickUpDateTime,
                InitialLongitude = pl.Longitude,
                InitialLatitude = pl.Latitude,
                ReturnedLongitude = -1,
                ReturnedLatitude = -1,
                ReturnedTime = dropOffDateTime,
                Status = HireStatus.Planned,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                CarId = car.Id
            };

            _db.CarHistory.Add(history);
            _db.SaveChanges();

            return View("CarHireConfirmation");
        }
    }
}
