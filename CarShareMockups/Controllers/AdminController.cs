using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarShareMockups.Models;
using Microsoft.AspNetCore.Mvc;
using static CarShareMockups.Models.UserViewModel;
using CarFleetViewModel = CarShareMockups.Models.CarFleetViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarShareMockups.Controllers
{
    public class AdminController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.CarList = new List<CarFleetViewModel>
            {
                new CarFleetViewModel{Registration="ABC123", Make="Red Toyota Hilux", Status=Status.InUse},
                new CarFleetViewModel{Registration="ASD223", Make="Blue Toyota RAV4", Status=Status.InUse},
                new CarFleetViewModel{Registration="XYZ523", Make="Green Ford Ranger", Status=Status.InUse},
                new CarFleetViewModel{Registration="AVE312", Make="Silver Toyota Corolla", Status=Status.Available},
                new CarFleetViewModel{Registration="LKJ735", Make="Grey Hyundai i30", Status=Status.Available},
                new CarFleetViewModel{Registration="SOI098", Make="Orange Mazda CX-5", Status=Status.Available},
                new CarFleetViewModel{Registration="CIH384", Make="Yellow Kia Cerato", Status=Status.Available},
                new CarFleetViewModel{Registration="VOA927", Make="White Mitsubishi Triton", Status=Status.Suspended},
                new CarFleetViewModel{Registration="USB308", Make="Blakc Toyota Triton", Status=Status.Suspended}
            };

            ViewBag.UserList = new List<UserViewModel>
            {
                new UserViewModel{Fullname="Michael Weaver", Password="M1&htyM1ke55", UserStatus=UserStatus.RentingVehicle},
                new UserViewModel{Fullname="Jane Wong", Password="lK7>4j", UserStatus=UserStatus.Banned},
                new UserViewModel{Fullname="Penny Green", Password="Bern@dette67", UserStatus=UserStatus.NotCurrentlyRenting},
                new UserViewModel{Fullname="Andy Jones", Password="4t&yA9", UserStatus=UserStatus.RentingVehicle},
                new UserViewModel{Fullname="Freddy Logan", Password="L8th<a?23Tp#", UserStatus=UserStatus.NotCurrentlyRenting},
                new UserViewModel{Fullname="Sandy Dunn", Password="Im:)2b24", UserStatus=UserStatus.RentingVehicle},
                new UserViewModel{Fullname="Martin West", Password="u79?jG5vDC", UserStatus=UserStatus.Banned},
            };
            return View();
        }
    }
}
