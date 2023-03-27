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

namespace MilibooTests.Controller {
    [TestClass()]
    public class CountriesControllertest {
        private Mock<IDataRepository<Country>> _mockRepository;
        private CountriesController _controller;
        private MilibooDBContext context;
        private IDataRepository<Country> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<Country>>();
            _controller = new CountriesController(_mockRepository.Object);
        }

        public CountriesControllertest() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new CountryManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<Country> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetCountrys_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetCountries();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<Country>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetCountryById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetCountryById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task GetCountryByWording_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetCountryByWording("LISTENBOURG").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostCountry_ModelValidated_CreationOK_WithMoq() {
            Country cnt = new Country {
                CountryID = 1,
                Wording = "AFRIQUE DU SUD",
                PhoneCode = "27"
            };
            // Act
            var actionResult = _controller.PostCountry(cnt).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Country>), "Not an ActionResult<Country>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Country), "Not a Country");
            cnt.CountryID = ((Country)result.Value).CountryID;
            Assert.AreEqual(cnt, (Country)result.Value, "Countries not equals");
        }

        [TestMethod]
        public async Task PutCountry_ReturnsNotFound_WithMoq() {
            // Arrange
            Country newCountry = new Country {
                CountryID = 1,
                Wording = "AFRIQUE DU SUD",
                PhoneCode = "27"
            };
            Country oldCountry = new Country {
                CountryID = 5000,
                Wording = "AFRIQUE DU SUD",
                PhoneCode = "27"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newCountry.CountryID).Result).Returns(newCountry);
            // Act
            var actionResult = _controller.PutCountry(oldCountry.CountryID, oldCountry).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutCountry_ReturnsOk_WithMoq() {
            // Arrange
            Country newCountry = new Country {
                CountryID = 1,
                Wording = "AFRIQUE DU SUD",
                PhoneCode = "27"
            };
            Country oldCountry = new Country {
                CountryID = 1,
                Wording = "AFRIQUE DU SUD",
                PhoneCode = "271"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newCountry.CountryID).Result).Returns(newCountry);
            // Act
            var actionResult = _controller.PutCountry(oldCountry.CountryID, oldCountry).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutCountry_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            Country newCountry = new Country {
                CountryID = 1,
                Wording = "AFRIQUE DU SUD",
                PhoneCode = "27"
            };
            Country oldCountry = new Country {
                CountryID = 1,
                Wording = "AFRIQUE DU SUD",
                PhoneCode = "27"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newCountry.CountryID).Result).Returns(newCountry);
            // Act
            var actionResult = _controller.PutCountry(id, oldCountry).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }

        [TestMethod]
        public async Task DeleteCountryTest_ReturnsOk_WithMoq() {
            // Arrange
            Country cnt = new Country {
                CountryID = 1,
                Wording = "AFRIQUE DU SUD",
                PhoneCode = "27"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(cnt.CountryID).Result).Returns(cnt);
            // Act
            var actionResult = _controller.DeleteCountry(cnt.CountryID).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteCountryTest_ReturnsNotFound_WithMoq() {
            // Arrange
            Country cnt = new Country {
                CountryID = 5000,
                Wording = "AFRIQUE DU SUD",
                PhoneCode = "27"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(cnt.CountryID).Result).Returns(cnt);
            // Act
            var actionResult = _controller.DeleteCountry(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }
    }
}