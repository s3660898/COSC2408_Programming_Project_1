using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarShare.Controllers;
using CarShare.Data;
using CarShare.Identity.Data;
using CarShare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nest;
using IgnoreAttribute = Microsoft.VisualStudio.TestTools.UnitTesting.IgnoreAttribute;

namespace CarShare.UnitTests
{
    [TestClass]
    public class AdminControllerTests
    {

        private ApplicationDbContext _dbContext;
        private AdminController _ac;
        private CarShareUser testUser;
        private Car testCar;
        private Image testImage;

        [TestInitialize]
        public void Initialize()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase<ApplicationDbContext>("AdminControllerTestDb");
            _dbContext = new ApplicationDbContext(optionsBuilder.Options);
            _ac = new AdminController(_dbContext);

            // adding test user
            testUser = new CarShareUser()
            {
                UserName = "testUserName",
                Email = "testUserEmail",
                EmailConfirmed = true
            };
            _dbContext.Users.Add(testUser);

            // adding test image
            testImage = new Image()
            {
                Title = "testTitle",
                Data = Encoding.ASCII.GetBytes(new string('a', 100))
            };
            _dbContext.Images.Add(testImage);
            _dbContext.SaveChanges();

            // adding test car
            Image dbImage = _dbContext.Images.FirstOrDefault();
            testCar = new Car()
            {
                Registration = "testRegistration",
                Description = "testDescription",
                Status = CarStatus.Available,
                NumSeats = 2,
                ImageId = dbImage.Id,
                Image = dbImage
            };
            _dbContext.Cars.Add(testCar);

            _dbContext.SaveChanges();
        }

        [TestMethod]
        public void TestIndexView()
        {
            var r = _ac.Index() as ViewResult;
            Assert.AreEqual(null, r.ViewName);
        }

        [TestMethod]
        public void TestUserManagementView()
        {
            var r = _ac.UserManagement() as ViewResult;
            Assert.AreEqual(null, r.ViewName);
        }

        [TestMethod]
        public void TestViewUser()
        {
            var r = _ac.ViewUser(testUser.Id) as ViewResult;
            CarShareUser csu = (CarShareUser)r.Model;
            Assert.AreEqual("testUserName", csu.UserName);
        }

        [TestMethod]
        public void TestEditUserDetails()
        {
            var r = _ac.EditUserDetails(testUser.Id) as ViewResult;
            CarShareUser csu = (CarShareUser)r.Model;
            Assert.AreEqual("testUserName", csu.UserName);
        }

        [TestMethod]
        public void TestEditUserDetailsPost()
        {
            // mocking model
            CarShareUser modelUser = new CarShareUser()
            {
                Id = testUser.Id,
                Email = "newTestEmail",
                Address = "newTestAddress",
                UserStatus = UserStatus.AwaitingHire
            };

            var r = _ac.EditUserDetails(modelUser) as ViewResult;

            // pulling edited user from db
            CarShareUser editedUser = _dbContext.Users.Where(u => u.Id == testUser.Id).FirstOrDefault();

            // asserting all required changes have been made correctly
            Assert.AreEqual(modelUser.Id, editedUser.Id);
            Assert.AreEqual(modelUser.Email, editedUser.Email);
            Assert.AreEqual(modelUser.Address, editedUser.Address);
            Assert.AreEqual(modelUser.UserStatus, editedUser.UserStatus);
        }

        [TestMethod]
        public void TestFleetManagement()
        {
            // adding nescessary parking lot
            ParkingLot pl = new ParkingLot()
            {
                Longitude = 0,
                Latitude = 0,
                Address = "123 test Address",
                Description = "testDescription"
            };

            _dbContext.ParkingLots.Add(pl);
            _dbContext.SaveChanges();

            var r = _ac.FleetManagement() as ViewResult;
            List<CarFleetViewModel> resultCarList = (List<CarFleetViewModel>)r.ViewData["CarList"];
            Assert.AreEqual("testRegistration", resultCarList.FirstOrDefault().Registration);
        }

        [TestMethod]
        public void TestViewCar()
        {
            var task = _ac.ViewCar(testCar.Id, "", "");
            task.Wait();
            var r = task.Result as ViewResult;


            // testing the car is correct
            Car modelCar = (Car)r.Model;
            Assert.AreEqual("testRegistration", modelCar.Registration);

            // testing the parking lot is correct
            ParkingLot modelParkingLot = (ParkingLot)r.ViewData["ParkingLot"];
            Assert.AreEqual("Error parking lot :^)", modelParkingLot.Description);
        }

        [TestMethod]
        public void TestAddCar()
        {
            var r = _ac.AddCar() as ViewResult;
            Assert.AreEqual(null, r.ViewName);
        }

        [Ignore]
        [TestMethod]
        public void TestAddCarPost()
        {
            Car modelCar = new Car()
            {
                Registration = "tmRegistration",
                Description = "tmDescription",
                ImageId = testImage.Id,
                Image = testImage,
                NumSeats = 2,
                Latitude = 0,
                Longitude = 0,
                Category = CarCategory.Sport,
                ParkingLotId = 0,
                Status = CarStatus.Available

            };

            var r = _ac.AddCar(modelCar) as ViewResult;

            // testing added car
            Car pulledCar = _dbContext.Cars.Where(c => c.Registration == modelCar.Registration).FirstOrDefault();
            Assert.AreEqual(modelCar.Registration, pulledCar.Registration);
            Assert.AreEqual(modelCar.Description, pulledCar.Description);
            Assert.AreEqual(modelCar.ImageId, pulledCar.ImageId);
            Assert.AreEqual(modelCar.NumSeats, pulledCar.NumSeats);
            Assert.AreEqual(modelCar.Latitude, pulledCar.Latitude);
            Assert.AreEqual(modelCar.Longitude, pulledCar.Longitude);
            Assert.AreEqual(modelCar.Category, pulledCar.Category);
            Assert.AreEqual(modelCar.ParkingLotId, pulledCar.ParkingLotId);
            Assert.AreEqual(modelCar.Status, pulledCar.Status);
        }

        [TestMethod]
        public void TestEditCar()
        {
            var r = _ac.EditCar(testCar.Id) as ViewResult;
            Car modelCar = (Car)r.Model;
            Assert.AreEqual(testCar.Id, modelCar.Id);
        }

        [Ignore]
        [TestMethod]
        public void TestEditCarPost()
        {
            Car initialCar = new Car()
            {
                Registration = "initialRegistration",
                Description = "initialDescription",
                ImageId = testImage.Id,
                Image = testImage,
                NumSeats = 2,
                Latitude = 0,
                Longitude = 0,
                Category = CarCategory.Sport,
                ParkingLotId = 0,
                Status = CarStatus.Available
            };
            _dbContext.Cars.Add(initialCar);
            _dbContext.SaveChanges();


            var r = _ac.EditCar(initialCar) as ViewResult;

            // testing added car
            Car pulledCar = _dbContext.Cars.Where(c => c.Registration == initialCar.Registration).FirstOrDefault();
            Assert.AreEqual(initialCar.Registration, pulledCar.Registration);
            Assert.AreEqual(initialCar.Description, pulledCar.Description);
            Assert.AreEqual(initialCar.ImageId, pulledCar.ImageId);
            Assert.AreEqual(initialCar.NumSeats, pulledCar.NumSeats);
            Assert.AreEqual(initialCar.Latitude, pulledCar.Latitude);
            Assert.AreEqual(initialCar.Longitude, pulledCar.Longitude);
            Assert.AreEqual(initialCar.Category, pulledCar.Category);
            Assert.AreEqual(initialCar.ParkingLotId, pulledCar.ParkingLotId);
            Assert.AreEqual(initialCar.Status, pulledCar.Status);
        }

        [TestMethod]
        public void TestParkingLotManagement()
        {
            ParkingLot testParkingLot = new ParkingLot()
            {
                Longitude = 0,
                Latitude = 0,
                Description = "testDescription",
                Address = "testAddress"
            };
            _dbContext.ParkingLots.Add(testParkingLot);
            _dbContext.SaveChanges();

            var r = _ac.ParkingLotManagement() as ViewResult;
            List<ParkingLot> plList = (List<ParkingLot>)r.ViewData["ParkingLotList"];

            // our inserted parking lot
            ParkingLot pl = plList.Where(p => p.Id == testParkingLot.Id).FirstOrDefault();

            // asserting that the relevant data is correct
            Assert.AreEqual(pl.Description, testParkingLot.Description);
            Assert.AreEqual(pl.Address, testParkingLot.Address);
            Assert.AreEqual(pl.Longitude, testParkingLot.Longitude);
            Assert.AreEqual(pl.Latitude, testParkingLot.Latitude);
        }

        [TestMethod]
        public void TestViewParkingLot()
        {
            ParkingLot testParkingLot = new ParkingLot()
            {
                Longitude = 0,
                Latitude = 0,
                Description = "testDescription",
                Address = "testAddress"
            };

            _dbContext.ParkingLots.Add(testParkingLot);
            _dbContext.SaveChanges();

            var r = _ac.ViewParkingLot(testParkingLot.Id) as ViewResult;

            // our parking lot
            ParkingLot pl = (ParkingLot)r.Model;

            // asserting that the relevant data is correct
            Assert.AreEqual(pl.Description, testParkingLot.Description);
            Assert.AreEqual(pl.Address, testParkingLot.Address);
            Assert.AreEqual(pl.Longitude, testParkingLot.Longitude);
            Assert.AreEqual(pl.Latitude, testParkingLot.Latitude);
        }

        [TestMethod]
        public void TestAddParkingLot()
        {
            var r = _ac.AddParkingLot() as ViewResult;
            Assert.AreEqual(null, r.ViewName);
        }

        [TestMethod]
        public void TestAddParkingLotPost()
        {
            ParkingLot modelParkingLot = new ParkingLot()
            {
                Longitude = 0,
                Latitude = 0,
                Description = "modelDescription",
                Address = "modelAddress"
            };
            _dbContext.ParkingLots.Add(modelParkingLot);
            _dbContext.SaveChanges();

            var r = _ac.AddParkingLot(modelParkingLot) as ViewResult;

            ParkingLot postedPl = _dbContext.ParkingLots.Where(p => p.Id == modelParkingLot.Id).FirstOrDefault();

            Assert.AreEqual(postedPl.Longitude, modelParkingLot.Longitude);
            Assert.AreEqual(postedPl.Latitude, modelParkingLot.Latitude);
            Assert.AreEqual(postedPl.Description, modelParkingLot.Description);
            Assert.AreEqual(postedPl.Address, modelParkingLot.Address);
        }

        [TestMethod]
        public void TestEditParkingLot()
        {
            ParkingLot initialPl = new ParkingLot()
            {
                Longitude = 0,
                Latitude = 0,
                Description = "modelDescription",
                Address = "modelAddress"
            };
            _dbContext.ParkingLots.Add(initialPl);
            _dbContext.SaveChanges();

            var r = _ac.EditParkingLot(initialPl.Id) as ViewResult;

            ParkingLot modelPl = (ParkingLot)r.Model;

            Assert.AreEqual(modelPl.Longitude, initialPl.Longitude);
            Assert.AreEqual(modelPl.Latitude, initialPl.Latitude);
            Assert.AreEqual(modelPl.Description, initialPl.Description);
            Assert.AreEqual(modelPl.Address, initialPl.Address);
        }

        [TestMethod]
        public void TestEditParkingLotPost()
        {
            ParkingLot initialPl = new ParkingLot()
            {
                Longitude = 0,
                Latitude = 0,
                Description = "modelDescription",
                Address = "modelAddress"
            };
            _dbContext.ParkingLots.Add(initialPl);
            _dbContext.SaveChanges();

            // all new data bar Id
            ParkingLot editPl = new ParkingLot()
            {
                Id = initialPl.Id,
                Longitude = 1,
                Latitude = 1,
                Description = "newDesc",
                Address = "newAddress"
            };

            var r = _ac.EditParkingLot(editPl) as ViewResult;

            ParkingLot editedPl = _dbContext.ParkingLots.Where(p => p.Id == editPl.Id).FirstOrDefault();

            Assert.AreEqual(editedPl.Longitude, editPl.Longitude);
            Assert.AreEqual(editedPl.Latitude, editPl.Latitude);
            Assert.AreEqual(editedPl.Description, editPl.Description);
            Assert.AreEqual(editedPl.Address, editPl.Address);
        }
    }
}
