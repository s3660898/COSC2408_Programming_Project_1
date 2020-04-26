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

        public async Task<IActionResult> Index(int? page = 1)
        {

            const int pageSize = 50;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Console.WriteLine(userId);

            ViewBag.userId = userId;


            var pagedList1 = await _db.CarHistory.Where(a => a.UserId == userId).OrderBy(a => a.Id).ToPagedListAsync(page, pageSize);

            return View(pagedList1);

           
        }
    }
}