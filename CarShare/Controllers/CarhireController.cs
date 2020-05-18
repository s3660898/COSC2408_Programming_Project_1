/*
 @Autor: Aishah Wali
 @purpose: Controller for car hire
 */
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShare.Data;
using CarShare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using X.PagedList;

namespace CarShare.Controllers
{
    public class CarhireController : Controller
    {
        private ApplicationDbContext _db;

        private ILogger<HistoryController> _logger;

        public CarhireController(ApplicationDbContext db, ILogger<HistoryController> _logger)
        {
            _db = db;
            _logger = _logger;
        }

        public IActionResult Index()
        {
            var cars = _db.Cars.OrderBy(x => x.Id).ToList();
            //37.8290° S, 144.9570° E Southebank
            //37.8098° S, 144.9652° E central library

            //37.7431, 145.0081 preston

            //Console.WriteLine("Total Cars");

            int totalRecord =  cars.Count();
            Double[] Totaldistance = new Double[totalRecord];
            String[] CarImages = new String[totalRecord];
            String[] ImageTitle = new String[totalRecord];

            int counter = 0;
            if (cars.Count() > 0) {
                foreach (var Car in cars) {

                    //Origin is harcoded
                    var distance = DistanceTo(37.8290, 144.9570, Car.Latitude, Car.Longitude);
                    Totaldistance[counter] = distance;
                    Console.WriteLine(Car.Latitude );
                    Console.WriteLine(Car.Longitude);

                    Image img = _db.Images.SingleOrDefault(i => i.Id == Car.ImageId);
                    CarImages[counter] = img.Title;
                    ImageTitle[counter] = string.Format("data:image/jgp;base64,{0}", Convert.ToBase64String(img.Data));

                    counter++;
                }
            }

            ViewBag.cars = cars;
            ViewBag.distance = Totaldistance;
            ViewBag.imageUrl = ImageTitle;
            ViewBag.imageTitle = CarImages;

            return View();
        }


        public IActionResult filter(int vehicle_type, int num_seats, String from_date, String until_date)
        {
            Console.WriteLine("FROM FILTER");
            Console.WriteLine(vehicle_type);
            Console.WriteLine(num_seats);
            Console.WriteLine(from_date);
            Console.WriteLine(until_date);

            var cars = _db.Cars.OrderBy(x => x.Id).ToList();

            if (vehicle_type != 0) {
                vehicle_type = vehicle_type - 1;

                cars = _db.Cars.Where(x => x.Category == (CarCategory)vehicle_type).OrderBy(x => x.Id).ToList();

            }

            if (num_seats != 0)
            {
                vehicle_type = vehicle_type - 1;

                cars = _db.Cars.Where(x => x.NumSeats == num_seats).OrderBy(x => x.Id).ToList();

            }

            int totalRecord = cars.Count();
            Double[] Totaldistance = new Double[totalRecord];
            String[] CarImages = new String[totalRecord];
            String[] ImageTitle = new String[totalRecord];

            int counter = 0;
            if (cars.Count() > 0)
            {
                foreach (var Car in cars)
                {

                    //Origin is harcoded
                    var distance = DistanceTo(37.8290, 144.9570, Car.Latitude, Car.Longitude);
                    Totaldistance[counter] = distance;
                    Console.WriteLine(Car.Latitude);
                    Console.WriteLine(Car.Longitude);

                    Image img = _db.Images.SingleOrDefault(i => i.Id == Car.ImageId);
                    CarImages[counter] = img.Title;
                    ImageTitle[counter] = string.Format("data:image/jgp;base64,{0}", Convert.ToBase64String(img.Data));

                    counter++;
                }
            }

            
            ViewBag.cars = cars;
            ViewBag.distance = Totaldistance;
            ViewBag.imageUrl = ImageTitle;
            ViewBag.imageTitle = CarImages;

            ViewBag.vehicle_type = vehicle_type;
            ViewBag.num_seats = num_seats;
            ViewBag.from_date = from_date;
            ViewBag.until_date = until_date;




            return View();
        }



            public static double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            switch (unit)
            {
                case 'K': //Kilometers -> default
                    return Math.Round(dist * 1.609344, 2);
                case 'N': //Nautical Miles 
                    return Math.Round(dist * 0.8684, 2);
                case 'M': //Miles
                    return Math.Round(dist, 2);
            }

            return Math.Round(dist, 2);
        }

    }
}
