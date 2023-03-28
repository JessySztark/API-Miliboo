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
    public class PaymentMethodsControllerTests {
        private Mock<IDataRepository<PaymentMethod>> _mockRepository;
        private PaymentMethodsController _controller;
        private MilibooDBContext context;
        private IDataRepository<PaymentMethod> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<PaymentMethod>>();
            _controller = new PaymentMethodsController(_mockRepository.Object);
        }

        public PaymentMethodsControllerTests() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new PaymentMethodManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<PaymentMethod> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetPaymentMethods_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetPaymentMethods();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<PaymentMethod>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetPaymentMethodById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetPaymentMethodById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostPaymentMethod_ModelValidated_CreationOK_WithMoq() {
            PaymentMethod pmd = new PaymentMethod {
                Paymentmethodid = 1,
                MethodName= "Carte Bancaire"
            };
            // Act
            var actionResult = _controller.PostPaymentMethod(pmd).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<PaymentMethod>), "Not an ActionResult<PaymentMethod>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(PaymentMethod), "Not an PaymentMethod");
            pmd.Paymentmethodid = ((PaymentMethod)result.Value).Paymentmethodid;
            Assert.AreEqual(pmd, (PaymentMethod)result.Value, "PaymentMethods not equals");
        }


        [TestMethod]
        public async Task DeletePaymentMethodTest_ReturnsOk_WithMoq() {
            // Arrange
            PaymentMethod pmd = new PaymentMethod {
                Paymentmethodid = 1,
                MethodName = "Carte Bancaire"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(pmd.Paymentmethodid).Result).Returns(pmd);
            // Act
            var actionResult = _controller.DeletePaymentMethod(pmd.Paymentmethodid).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeletePaymentMethodTest_ReturnsNotFound_WithMoq() {
            // Arrange
            PaymentMethod pmd = new PaymentMethod {
                Paymentmethodid = 5000,
                MethodName = "Carte Bancaire"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(pmd.Paymentmethodid).Result).Returns(pmd);
            // Act
            var actionResult = _controller.DeletePaymentMethod(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutPaymentMethod_ReturnsNotFound_WithMoq() {
            // Arrange
            PaymentMethod newPaymentMethod = new PaymentMethod {
                Paymentmethodid = 1,
                MethodName = "Carte Bancaire"
            };
            PaymentMethod oldPaymentMethod = new PaymentMethod {
                Paymentmethodid = 5000,
                MethodName = "Carte Bancaire"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newPaymentMethod.Paymentmethodid).Result).Returns(newPaymentMethod);
            // Act
            var actionResult = _controller.PutPaymentMethod(oldPaymentMethod.Paymentmethodid, oldPaymentMethod).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutPaymentMethod_ReturnsOk_WithMoq() {
            // Arrange
            PaymentMethod newPaymentMethod = new PaymentMethod {
                Paymentmethodid = 1,
                MethodName = "Carte Bancaire"
            };
            PaymentMethod oldPaymentMethod = new PaymentMethod {
                Paymentmethodid = 1,
                MethodName = "Carte Bancaire"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newPaymentMethod.Paymentmethodid).Result).Returns(newPaymentMethod);
            // Act
            var actionResult = _controller.PutPaymentMethod(oldPaymentMethod.Paymentmethodid, oldPaymentMethod).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutPaymentMethod_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            PaymentMethod newPaymentMethod = new PaymentMethod {
                Paymentmethodid = 1,
                MethodName = "Carte Bancaire"
            };
            PaymentMethod oldPaymentMethod = new PaymentMethod {
                Paymentmethodid = 5000,
                MethodName = "Carte Bancaire"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newPaymentMethod.Paymentmethodid).Result).Returns(newPaymentMethod);
            // Act
            var actionResult = _controller.PutPaymentMethod(id, oldPaymentMethod).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }
    }
}