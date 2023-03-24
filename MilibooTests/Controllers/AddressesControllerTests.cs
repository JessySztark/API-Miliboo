using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Miliboo.Controllers;
using Miliboo.Models.DataManager;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;
using MilibooAPI.Controllers;
using MilibooAPI.Models.DataManager;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miliboo.Controllers.Tests {
    [TestClass()]
    public class AddressesControllerTests {
        private Mock<IDataRepository<Address>> _mockRepository;
        private AddressesController _controller;
        private MilibooDBContext context;
        private IDataRepository<Address> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<Address>>();
            _controller = new AddressesController(_mockRepository.Object);
        }

        public AddressesControllerTests() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new AddressesManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<Address> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetAddresses_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetAddress();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<Address>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetAddressById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetAddress(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task GetAddressbyPostalCode_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetAddressFromPostalCode("74000").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostAddress_ModelValidated_CreationOK_WithMoq() {
            Address add = new Address {
                AddressID = 5000,
                CountryID = 5,
                Wording = "14 Rue Bastille",
                PostalCode = "74000",
                City = "Annecy",
                Longitude = (float)8.01,
                Latitude = (float)44.45
            };
            // Act
            var actionResult = _controller.PostAddress(add).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Address>), "Not an ActionResult<Address>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Address), "Not an Address");
            add.AddressID = ((Address)result.Value).AddressID;
            Assert.AreEqual(add, (Address)result.Value, "Addresses not equals");
        }


        [TestMethod]
        public async Task DeleteAddressTest_ReturnsNoContent_WithMoq() {
            // Arrange
            Address add = new Address {
                AddressID = 1,
                CountryID = 5,
                Wording = "14 Rue Bastille",
                PostalCode = "74000",
                City = "Annecy",
                Longitude = (float)8.01,
                Latitude = (float)44.45
            };
            _mockRepository.Setup(x => x.GetByIdAsync(add.AddressID).Result).Returns(add);
            // Act
            var actionResult = _controller.DeleteAddress(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteAddressTest_ReturnsNotFound_WithMoq() {
            // Arrange
            Address add = new Address {
                AddressID = 5000,
                CountryID = 5,
                Wording = "14 Rue Bastille",
                PostalCode = "74000",
                City = "Annecy",
                Longitude = (float)8.01,
                Latitude = (float)44.45
            };
            _mockRepository.Setup(x => x.GetByIdAsync(add.AddressID).Result).Returns(add);
            // Act
            var actionResult = _controller.DeleteAddress(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutAddress_ReturnsNotFound_WithMoq() {
            // Arrange
            Address newAddress = new Address {
                AddressID = 1,
                CountryID = 5,
                Wording = "14 Rue Bastille",
                PostalCode = "74000",
                City = "Annecy",
                Longitude = (float)8.01,
                Latitude = (float)44.45
            };
            Address oldAddress = new Address {
                AddressID = 5001,
                CountryID = 5,
                Wording = "17 Rue Louis de Broglie",
                PostalCode = "74000",
                City = "Annecy-le-Vieux",
                Longitude = (float)8.23,
                Latitude = (float)45.07
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newAddress.AddressID).Result).Returns(newAddress);
            // Act
            var actionResult = _controller.PutAddress(oldAddress.AddressID, oldAddress).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutAddress_ReturnsOk_WithMoq() {
            // Arrange
            Address newAddress = new Address {
                AddressID = 1,
                CountryID = 5,
                Wording = "14 Rue Bastille",
                PostalCode = "74000",
                City = "Annecy",
                Longitude = (float)8.01,
                Latitude = (float)44.45
            };
            Address oldAddress = new Address {
                AddressID = 1,
                CountryID = 5,
                Wording = "17 Rue Louis de Broglie",
                PostalCode = "74000",
                City = "Annecy-le-Vieux",
                Longitude = (float)8.23,
                Latitude = (float)45.07
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newAddress.AddressID).Result).Returns(newAddress);
            // Act
            var actionResult = _controller.PutAddress(oldAddress.AddressID, oldAddress).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutAddress_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            Address newAddress = new Address {
                AddressID = 5000,
                CountryID = 5,
                Wording = "14 Rue Bastille",
                PostalCode = "74000",
                City = "Annecy",
                Longitude = (float)8.01,
                Latitude = (float)44.45
            };
            Address oldAddress = new Address {
                AddressID = 5000,
                CountryID = 5,
                Wording = "17 Rue Louis de Broglie",
                PostalCode = "74000",
                City = "Annecy-le-Vieux",
                Longitude = (float)8.23,
                Latitude = (float)45.07
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newAddress.AddressID).Result).Returns(newAddress);
            // Act
            var actionResult = _controller.PutAddress(id, oldAddress).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }
    }
}