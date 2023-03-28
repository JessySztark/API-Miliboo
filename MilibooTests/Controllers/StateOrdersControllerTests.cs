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
    public class StateOrdersControllerTests {
        private Mock<IDataRepository<StateOrder>> _mockRepository;
        private StateOrdersController _controller;
        private MilibooDBContext context;
        private IDataRepository<StateOrder> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<StateOrder>>();
            _controller = new StateOrdersController(_mockRepository.Object);
        }

        public StateOrdersControllerTests() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new StateOrderManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<StateOrder> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetStateOrders_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetStateOrders();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<StateOrder>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetStateOrderById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetStateOrderById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostStateOrder_ModelValidated_CreationOK_WithMoq() {
            StateOrder rgp = new StateOrder {
                StateOrderID = 1,
                StateOrderName= "Payer"
            };
            // Act
            var actionResult = _controller.PostStateOrder(rgp).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<StateOrder>), "Not an ActionResult<StateOrder>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(StateOrder), "Not an StateOrder");
            rgp.StateOrderID = ((StateOrder)result.Value).StateOrderID;
            rgp.StateOrderID = ((StateOrder)result.Value).StateOrderID;
            Assert.AreEqual(rgp, (StateOrder)result.Value, "StateOrders not equals");
        }


        [TestMethod]
        public async Task DeleteStateOrderTest_ReturnsOk_WithMoq() {
            // Arrange
            StateOrder rgp = new StateOrder {
                StateOrderID = 1,
                StateOrderName = "Payer"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(rgp.StateOrderID).Result).Returns(rgp);
            // Act
            var actionResult = _controller.DeleteStateOrder(rgp.StateOrderID).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteStateOrderTest_ReturnsNotFound_WithMoq() {
            // Arrange
            StateOrder rgp = new StateOrder {
                StateOrderID = 5000,
                StateOrderName = "Payer"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(rgp.StateOrderID).Result).Returns(rgp);
            // Act
            var actionResult = _controller.DeleteStateOrder(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutStateOrder_ReturnsNotFound_WithMoq() {
            // Arrange
            StateOrder newStateOrder = new StateOrder {
                StateOrderID = 1,
                StateOrderName = "Payer"
            };
            StateOrder oldStateOrder = new StateOrder {
                StateOrderID = 5000,
                StateOrderName = "Payer"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newStateOrder.StateOrderID).Result).Returns(newStateOrder);
            // Act
            var actionResult = _controller.PutStateOrder(oldStateOrder.StateOrderID, oldStateOrder).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutStateOrder_ReturnsOk_WithMoq() {
            // Arrange
            StateOrder newStateOrder = new StateOrder {
                StateOrderID = 1,
                StateOrderName = "Payer"
            };
            StateOrder oldStateOrder = new StateOrder {
                StateOrderID = 1,
                StateOrderName = "Payer"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newStateOrder.StateOrderID).Result).Returns(newStateOrder);
            // Act
            var actionResult = _controller.PutStateOrder(oldStateOrder.StateOrderID, oldStateOrder).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutStateOrder_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            StateOrder newStateOrder = new StateOrder {
                StateOrderID = 1,
                StateOrderName = "Payer"
            };
            StateOrder oldStateOrder = new StateOrder {
                StateOrderID = 5000,
                StateOrderName = "Payer"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newStateOrder.StateOrderID).Result).Returns(newStateOrder);
            // Act
            var actionResult = _controller.PutStateOrder(id, oldStateOrder).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }
    }
}