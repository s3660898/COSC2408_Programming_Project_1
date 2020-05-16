using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShare.Data;
using CarShare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System.Security.Claims;

namespace CarShare.Controllers
{
    public class UserhistoryController : Controller
    {
        ApplicationDbContext _db;

        public UserhistoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var carHistories = _db.CarHistory.Where(a => a.UserId == userId).OrderBy(a => a.HireTime);
            var cars = _db.Cars.ToList<Car>();

            var query = from car in cars
                        join history in carHistories on car equals history.Car
                        select new CarHistoryUserViewModel()
                        {
                            Registration = car.Registration,
                            Description = car.Description,
                            HireTime = history.HireTime,
                            HireDuration = history.HireDuration,
                            InitialLongitude = history.InitialLongitude,
                            InitialLatitude = history.InitialLatitude,
                            ReturnedLongitude = history.ReturnedLongitude,
                            ReturnedLatitude = history.ReturnedLatitude,
                            Status = history.Status
                        };

            const int pageSize = 50;
            ViewBag.userId = userId;
            var pagedList1 = await query.ToPagedListAsync(page, pageSize);

            return View(pagedList1);

           
        }
    }
}