﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace CarShare.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            Console.WriteLine("From Index");
            Console.WriteLine(User.Identity);
            return View();
        }

        public IActionResult CarHireBrowse()
        {
            return View();
        }
    }
}
