using System;
using System.Collections.Generic;
using System.Linq;
using CarShare.Data;
using CarShare.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarShare.Identity.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarShare.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserManagement()
        {
            // getting ordered list from db
            var UserList = _db.Users.ToList();

            // putting relevant data in ViewBag
            ViewBag.UserList = new List<UserViewModel>();
            foreach (CarShareUser c in UserList)
            {
                ViewBag.UserList.Add(new UserViewModel() { Id = c.Id, Email = c.Email, Address = c.Address, UserStatus = c.UserStatus });
            }

            return View();
        }

        public IActionResult ViewUser(string Id)
        {
            var user = _db.Users.SingleOrDefault(c => c.Id == Id);

            return View(user);
        }
    }
}
