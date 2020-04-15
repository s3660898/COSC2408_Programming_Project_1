using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShare.Data;
using CarShare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarShare.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HistoryController : Controller
    {

        private ApplicationDbContext _db;

        private ILogger<HistoryController> _logger;

        public HistoryController(ApplicationDbContext db, ILogger<HistoryController> _logger)
        {
            _db = db;
            _logger = _logger;
        }

        public IActionResult Index()
        {
            var CarHistory = _db.CarHistory.OrderBy(a => a.Id).ToList();

            //_logger.LogInformation("Total record {0}", CarHistory.Count != null ? CarHistory.Count : 0);

            ViewBag.CarHistoryList = new List<CarHistory>();


            foreach (CarHistory c in CarHistory)
            {
                ViewBag.CarHistoryList.Add(c);
            }

            return View();
        }


        public IActionResult Search()
        {
            /*
            var CarHistory = _db.CarHistory.OrderBy(a => a.Id).ToList();

            //_logger.LogInformation("Total record {0}", CarHistory.Count != null ? CarHistory.Count : 0);

            ViewBag.CarHistoryList = new List<CarHistory>();


            foreach (CarHistory c in CarHistory)
            {
                ViewBag.CarHistoryList.Add(c);
            }*/

            return View();
        }



    }
}