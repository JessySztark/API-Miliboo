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
    public class DeliveryMethodsControllertest {
        private Mock<IDataRepository<DeliveryMethod>> _mockRepository;
        private DeliveryMethodsController _controller;
        private MilibooDBContext context;
        private IDataRepository<DeliveryMethod> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<DeliveryMethod>>();
            _controller = new DeliveryMethodsController(_mockRepository.Object);
        }

        public DeliveryMethodsControllertest() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new DeliveryMethodManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<DeliveryMethod> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetDeliveryMethods_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetDeliveryMethods();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<DeliveryMethod>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetDeliveryMethodById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetDeliveryMethodById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostDeliveryMethod_ModelValidated_CreationOK_WithMoq() {
            DeliveryMethod dlm = new DeliveryMethod {
                IdDeliveryMethod = 1,
                Description = "Transport à domicile"
            };
            // Act
            var actionResult = _controller.PostDeliveryMethod(dlm).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<DeliveryMethod>), "Not an ActionResult<DeliveryMethod>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(DeliveryMethod), "Not a DeliveryMethod");
            dlm.IdDeliveryMethod = ((DeliveryMethod)result.Value).IdDeliveryMethod;
            Assert.AreEqual(dlm, (DeliveryMethod)result.Value, "DeliveryMethods not equals");
        }

        [TestMethod]
        public async Task PutDeliveryMethod_ReturnsNotFound_WithMoq() {
            // Arrange
            DeliveryMethod newDeliveryMethod = new DeliveryMethod {
                IdDeliveryMethod = 1,
                Description = "Transport à domicile"
            };
            DeliveryMethod oldDeliveryMethod = new DeliveryMethod {
                IdDeliveryMethod = 5000,
                Description = "Transport chez l'habitant"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newDeliveryMethod.IdDeliveryMethod).Result).Returns(newDeliveryMethod);
            // Act
            var actionResult = _controller.PutDeliveryMethod(oldDeliveryMethod.IdDeliveryMethod, oldDeliveryMethod).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutDeliveryMethod_ReturnsOk_WithMoq() {
            // Arrange
            DeliveryMethod newDeliveryMethod = new DeliveryMethod {
                IdDeliveryMethod = 1,
                Description = "Transport à domicile"
            };
            DeliveryMethod oldDeliveryMethod = new DeliveryMethod {
                IdDeliveryMethod = 1,
                Description = "Transport chez l'habitant"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newDeliveryMethod.IdDeliveryMethod).Result).Returns(newDeliveryMethod);
            // Act
            var actionResult = _controller.PutDeliveryMethod(oldDeliveryMethod.IdDeliveryMethod, oldDeliveryMethod).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutDeliveryMethod_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            DeliveryMethod newDeliveryMethod = new DeliveryMethod {
                IdDeliveryMethod = 1,
                Description = "Transport à domicile"
            };
            DeliveryMethod oldDeliveryMethod = new DeliveryMethod {
                IdDeliveryMethod = 1,
                Description = "Transport à domicile"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newDeliveryMethod.IdDeliveryMethod).Result).Returns(newDeliveryMethod);
            // Act
            var actionResult = _controller.PutDeliveryMethod(id, oldDeliveryMethod).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }

        [TestMethod]
        public async Task DeleteDeliveryMethodTest_ReturnsOk_WithMoq() {
            // Arrange
            DeliveryMethod dlm = new DeliveryMethod {
                IdDeliveryMethod = 1,
                Description = "Transport à domicile"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(dlm.IdDeliveryMethod).Result).Returns(dlm);
            // Act
            var actionResult = _controller.DeleteDeliveryMethod(dlm.IdDeliveryMethod).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteDeliveryMethodTest_ReturnsNotFound_WithMoq() {
            // Arrange
            DeliveryMethod dlm = new DeliveryMethod {
                IdDeliveryMethod = 5000,
                Description = "Transport à domicile"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(dlm.IdDeliveryMethod).Result).Returns(dlm);
            // Act
            var actionResult = _controller.DeleteDeliveryMethod(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }
    }
}