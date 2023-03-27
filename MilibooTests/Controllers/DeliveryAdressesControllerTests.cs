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
    public class DeliveryAdressesControllertest {
        private Mock<IDataRepository<DeliveryAdress>> _mockRepository;
        private DeliveryAdressesController _controller;
        private MilibooDBContext context;
        private IDataRepository<DeliveryAdress> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<DeliveryAdress>>();
            _controller = new DeliveryAdressesController(_mockRepository.Object);
        }

        public DeliveryAdressesControllertest() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new DeliveryAdressManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<DeliveryAdress> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetDeliveryAdresses_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetDeliveryAdresses();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<DeliveryAdress>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetDeliveryAdressById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetDeliveryAdressById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostDeliveryAdress_ModelValidated_CreationOK_WithMoq() {
            DeliveryAdress dla = new DeliveryAdress {
                IdDeliveryAdress = 1,
                AccountID = 2,
                FavAdressName = "Maison"
            };
            // Act
            var actionResult = _controller.PostDeliveryAdress(dla).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<DeliveryAdress>), "Not an ActionResult<DeliveryAdress>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(DeliveryAdress), "Not a DeliveryAdress");
            dla.IdDeliveryAdress = ((DeliveryAdress)result.Value).IdDeliveryAdress;
            Assert.AreEqual(dla, (DeliveryAdress)result.Value, "DeliveryAdresses not equals");
        }

        [TestMethod]
        public async Task PutDeliveryAdress_ReturnsNotFound_WithMoq() {
            // Arrange
            DeliveryAdress newDeliveryAdress = new DeliveryAdress {
                IdDeliveryAdress = 1,
                AccountID = 2,
                FavAdressName = "Maison"
            };
            DeliveryAdress oldDeliveryAdress = new DeliveryAdress {
                IdDeliveryAdress = 5000,
                AccountID = 2,
                FavAdressName = "Pas Maison"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newDeliveryAdress.IdDeliveryAdress).Result).Returns(newDeliveryAdress);
            // Act
            var actionResult = _controller.PutDeliveryAdress(oldDeliveryAdress.IdDeliveryAdress, oldDeliveryAdress).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutDeliveryAdress_ReturnsOk_WithMoq() {
            // Arrange
            DeliveryAdress newDeliveryAdress = new DeliveryAdress {
                IdDeliveryAdress = 1,
                AccountID = 2,
                FavAdressName = "Maison"
            };
            DeliveryAdress oldDeliveryAdress = new DeliveryAdress {
                IdDeliveryAdress = 1,
                AccountID = 2,
                FavAdressName = "Pas Maison"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newDeliveryAdress.IdDeliveryAdress).Result).Returns(newDeliveryAdress);
            // Act
            var actionResult = _controller.PutDeliveryAdress(oldDeliveryAdress.IdDeliveryAdress, oldDeliveryAdress).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutDeliveryAdress_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            DeliveryAdress newDeliveryAdress = new DeliveryAdress {
                IdDeliveryAdress = 1,
                AccountID = 2,
                FavAdressName = "Maison"
            };
            DeliveryAdress oldDeliveryAdress = new DeliveryAdress {
                IdDeliveryAdress = 1,
                AccountID = 2,
                FavAdressName = "Maison"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newDeliveryAdress.IdDeliveryAdress).Result).Returns(newDeliveryAdress);
            // Act
            var actionResult = _controller.PutDeliveryAdress(id, oldDeliveryAdress).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }

        [TestMethod]
        public async Task DeleteDeliveryAdressTest_ReturnsOk_WithMoq() {
            // Arrange
            DeliveryAdress dla = new DeliveryAdress {
                IdDeliveryAdress = 1,
                AccountID = 2,
                FavAdressName = "Maison"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(dla.IdDeliveryAdress).Result).Returns(dla);
            // Act
            var actionResult = _controller.DeleteDeliveryAdress(dla.IdDeliveryAdress).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteDeliveryAdressTest_ReturnsNotFound_WithMoq() {
            // Arrange
            DeliveryAdress dla = new DeliveryAdress {
                IdDeliveryAdress = 5000,
                AccountID = 2,
                FavAdressName = "Maison"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(dla.IdDeliveryAdress).Result).Returns(dla);
            // Act
            var actionResult = _controller.DeleteDeliveryAdress(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }
    }
}