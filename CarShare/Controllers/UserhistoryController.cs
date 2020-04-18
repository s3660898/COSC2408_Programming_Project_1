using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShare.Data;
using CarShare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace CarShare.Controllers
{
    public class UserhistoryController : Controller
    {
        ApplicationDbContext _db;

        public UserhistoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(String userId, int? page = 1)
        {

            const int pageSize = 50;
            //var CarHistory = _db.CarHistory.OrderBy(a => a.Id).ToList();

            //_logger.LogInformation("Total record {0}", CarHistory.Count != null ? CarHistory.Count : 0);

            //ViewBag.CarHistoryList = new List<CarHistory>();

            //foreach (CarHistory c in CarHistory)
            //{
            //    ViewBag.CarHistoryList.Add(c);
            //}


            ViewBag.userId = userId;

            if (userId == null)
            {
                var pagedList = await _db.CarHistory.Where(a => a.UserId == "fbd8b270-41ef-49c5-adbd-ac324c0b1ddb-dummy").OrderBy(a => a.Id).ToPagedListAsync(page, pageSize);
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