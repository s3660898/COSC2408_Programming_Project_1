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
<<<<<<< HEAD
            //37.7431, 145.0081 preston
            //Console.WriteLine("Total Cars");
=======
            Console.WriteLine("Total Cars");
>>>>>>> 95b8eb8b3be713384c77911e3e569c7b6ab8c163

            int totalRecord =  cars.Count();
            Double[] Totaldistance = new Double[totalRecord]; ;
            int counter = 0;
            if (cars.Count() > 0) {
                foreach (var Car in cars) {

                    //Origin is harcoded
                    var distance = DistanceTo(37.8290, 144.9570, Car.Latitude, Car.Longitude);
                    Totaldistance[counter] = distance;

                    counter++;
                }
            }

            ViewBag.cars = cars;
            ViewBag.distance = Totaldistance;

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
                    return dist * 1.609344;
                case 'N': //Nautical Miles 
                    return dist * 0.8684;
                case 'M': //Miles
                    return dist;
            }

            return dist;
        }

    }
}
