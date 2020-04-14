using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShare.Data;
using CarShare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarShare.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HistoryController : Controller
    {

        ApplicationDbContext _db;

        public HistoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {


            return View();
        }


    }
}