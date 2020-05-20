using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CarShare.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace CarShare.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

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

        public IActionResult CarHire(int Id)
        {

            return View();
        }
    }
}
