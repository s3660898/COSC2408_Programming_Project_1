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


        public async Task<IActionResult> Index(int carId,  int ? page = 1)
        {
            var cars = _db.Cars.OrderBy(x => x.Id).ToList();
           
            const int pageSize = 50;
            //var CarHistory = _db.CarHistory.OrderBy(a => a.Id).ToList();

            //_logger.LogInformation("Total record {0}", CarHistory.Count != null ? CarHistory.Count : 0);

            //ViewBag.CarHistoryList = new List<CarHistory>();

            //foreach (CarHistory c in CarHistory)
            //{
            //    ViewBag.CarHistoryList.Add(c);
            //}
            ViewBag.carId = carId;
            ViewBag.cars = cars;

            if (carId == null)
            {
                var pagedList = await _db.CarHistory.Where(a => a.CarId == 1).OrderBy(a => a.Id).ToPagedListAsync(page, pageSize);
                return View(pagedList);
            }
            else {
                var pagedList1 = await _db.CarHistory.Where(a => a.CarId == carId).OrderBy(a => a.Id).ToPagedListAsync(page, pageSize);
                return View(pagedList1);
            }


            
        }


        public async Task<IActionResult> Search(String userId, int? page = 1)
        {
            const int pageSize = 5;
            //var CarHistory = _db.CarHistory.OrderBy(a => a.Id).ToList();

            //_logger.LogInformation("Total record {0}", CarHistory.Count != null ? CarHistory.Count : 0);

            //ViewBag.CarHistoryList = new List<CarHistory>();

            //foreach (CarHistory c in CarHistory)
            //{
            //    ViewBag.CarHistoryList.Add(c);
            //}


            if (userId == null)
            {
                var pagedList = await _db.CarHistory.Where(a => a.UserId == "fbd8b270-41ef-49c5-adbd-ac324c0b1ddb").OrderBy(a => a.Id).ToPagedListAsync(page, pageSize);
                return View(pagedList);
            }
            else
            {
                var pagedList1 = await _db.CarHistory.Where(a => a.UserId == userId).OrderBy(a => a.Id).ToPagedListAsync(page, pageSize);
                return View(pagedList1);
            }

            
        }



    }
}