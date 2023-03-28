using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miliboo.Controllers;
using Miliboo.Models;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miliboo.Controllers.Tests {
    [TestClass()]
    public class UserControllerTests {
        private Mock<IDataRepository<User>> _mockRepository;
        private UserController _controller;
        private MilibooDBContext context;
        private IDataRepository<User> dataRepository;
        /*
        [TestMethod()]
        public void GetUserDataTest_ReturnsOk_WithMoq() {
            // Act
            var actionResult = _controller.GetUserData().Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult), "Ok result");
        }

        [TestMethod()]
        public void GetAdminDataTest() {
            // Act
            var actionResult = _controller.GetAdminData().Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult), "Ok result");
        }*/
    }
}