using CarShare.Controllers;
using CarShare.Data;
using CarShare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IgnoreAttribute = Microsoft.VisualStudio.TestTools.UnitTesting.IgnoreAttribute;

namespace CarShare.UnitTests
{
    [TestClass]
    public class UserControllerTests
    {
        private ApplicationDbContext _dbContext;
        private UserController _uc;

        [TestInitialize]
        public void Initialize()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase<ApplicationDbContext>("UserControllerTestDb");
            _dbContext = new ApplicationDbContext(optionsBuilder.Options);
            _uc = new UserController(_dbContext, new HttpContextAccessor());
        }

        [TestMethod]
        public void TestIndex()
        {
            var r = _uc.Index() as ViewResult;
            Assert.AreEqual(null, r.ViewName);
        }

        [TestMethod]
        public void TestCarHireBrowse()
        {
            var r = _uc.CarHireBrowse() as ViewResult;
            Assert.AreEqual(null, r.ViewName);
        }

        [TestMethod]
        public void TestCarHire()
        {
            Image image = new Image()
            {
                Title = "testTitle",
                Data = Encoding.ASCII.GetBytes(new string('a', 100))
            };
            _dbContext.Images.Add(image);
            _dbContext.SaveChanges();

            ParkingLot pl = new ParkingLot()
            {
                Longitude = 0,
                Latitude = 0,
                Address = "testAddress",
                Description = "testDescription"
            };

            _dbContext.ParkingLots.Add(pl);
            _dbContext.SaveChanges();

            Car car = new Car()
            {
                Registration = "testRegistration",
                Description = "testDescription",
                Status = CarStatus.Available,
                NumSeats = 2,
                ImageId = image.Id,
                Image = image,
                ParkingLot = pl,
                ParkingLotId = pl.Id
            };
            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();

            string pickUpTime = "01/01/2020 1pm";
            string dropOffTime = "02/02/2020 2pm";

            var r = _uc.CarHire(car.Id, pickUpTime, dropOffTime) as ViewResult;

            Assert.AreEqual(image.Title, r.ViewData["ImageTitle"]);

            Assert.AreEqual(pl.Latitude, r.ViewData["plLatitude"]);
            Assert.AreEqual(pl.Longitude, r.ViewData["plLongitude"]);
            Assert.AreEqual(pl.Address, r.ViewData["plAddress"]);

            Assert.AreEqual(pickUpTime, r.ViewData["PickUpTime"]);
            Assert.AreEqual(dropOffTime, r.ViewData["dropOffTime"]);
        }

        [Ignore]
        [TestMethod]
        public void HireCarSubmit()
        {
            Image image = new Image()
            {
                Title = "testTitle",
                Data = Encoding.ASCII.GetBytes(new string('a', 100))
            };
            _dbContext.Images.Add(image);
            _dbContext.SaveChanges();

            ParkingLot pl = new ParkingLot()
            {
                Longitude = 0,
                Latitude = 0,
                Address = "testAddress",
                Description = "testDescription"
            };

            _dbContext.ParkingLots.Add(pl);
            _dbContext.SaveChanges();

            Car car = new Car()
            {
                Registration = "testRegistration",
                Description = "testDescription",
                Status = CarStatus.Available,
                NumSeats = 2,
                ImageId = image.Id,
                Image = image,
                ParkingLot = pl,
                ParkingLotId = pl.Id
            };
            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();
            string pickUpTime = "01/01/2020 1pm";
            string dropOffTime = "02/02/2020 2pm";

            var r = _uc.HireCarSubmit(car.Id, pickUpTime, dropOffTime) as ViewResult;

            CarHistory ch = _dbContext.CarHistory.Where(c => c.CarId == car.Id).FirstOrDefault();

            Assert.AreEqual(car.Id, ch.CarId);
        }
    }
}
