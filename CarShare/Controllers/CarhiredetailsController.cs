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
using System.Security.Claims;


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


            var carObj = _db.Cars.SingleOrDefault(c => c.Id == id);
            Image img = _db.Images.SingleOrDefault(i => i.Id == carObj.ImageId);
            ViewBag.ImageTitle = img.Title;
            ViewBag.ImageUrl   = string.Format("data:image/jgp;base64,{0}", Convert.ToBase64String(img.Data));

            ViewBag.car = Car;
            return View();
        }



        public IActionResult Addhire(int id, string datepicker, string timepicker, string datepicker1, string timepicker1, string drop_off, string pickup_coor)
        {
            ViewBag.Id = id;
            ViewBag.datepicker = datepicker;
            ViewBag.timepicker = timepicker;
            ViewBag.datepicker1 = datepicker1;
            ViewBag.timepicker1 = timepicker1;
            ViewBag.drop_off = drop_off;
            ViewBag.pickup_coor = pickup_coor;


            return View();
        }

            public IActionResult AddhireConfirmation(int id, string datepicker, string timepicker, string datepicker1, string timepicker1, string drop_off, string pickup_coor)
        {
            string[] retCoordinate = drop_off.Split(',');
            string[] iniCoordinate = pickup_coor.Split(',');

            DateTime dt  = Convert.ToDateTime(datepicker + " " + timepicker);
            DateTime dt1 = Convert.ToDateTime(datepicker1 + " " + timepicker1);
            var hireDuration = (dt1 - dt).TotalSeconds;

            var hirDuration = makeTimeSpan(hireDuration);

            CarHistory CH = new CarHistory()
            {
                HireTime = dt,
                HireDuration      = TimeSpan.Parse(hirDuration),
                InitialLatitude   = Convert.ToSingle(iniCoordinate[1]),
                InitialLongitude  = Convert.ToSingle(iniCoordinate[0]),
                ReturnedLatitude  = Convert.ToSingle(retCoordinate[1]),
                ReturnedLongitude = Convert.ToSingle(retCoordinate[0]),
                Status = 0,
                ReturnedTime = dt1,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                CarId = id
            };

            try
            {
                _db.CarHistory.Add(CH);
                _db.SaveChanges();
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine("ERROR while saving");
            }
            
            return RedirectToAction( "Index", "Carhire");
        }


        public static string makeTimeSpan(double timespan)
        {
            var seconds = timespan % 60;
            
            var remSeconds = timespan - seconds;

            var minutes = remSeconds / 60;
            Console.WriteLine(minutes);

            var remMinutes = minutes % 60;

            var remHours = minutes - remMinutes;

            remHours = remHours / 60;

            if(remMinutes == 0)
                 return remHours + ":00:00";
            else
                 return remHours +":"+remMinutes+":00";
        }


    }
}