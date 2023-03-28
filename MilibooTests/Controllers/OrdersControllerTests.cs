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
    public class OrderControllertest {
        private Mock<IDataRepository<Order>> _mockRepository;
        private OrdersController _controller;
        private MilibooDBContext context;
        private IDataRepository<Order> dataRepository;
        private readonly MilibooDBContext _context;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<Order>>();
            _controller = new OrdersController(_mockRepository.Object);
        }

        public OrderControllertest() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new OrderManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<Order> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetOrders_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetOrders();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<Order>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetOrderById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetOrderById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostOrder_ModelValidated_CreationOK_WithMoq() {
            Order ord = new Order {
                OrderID = 31,
                StateOrderID = 3,
                IdDeliveryMethod = 2,
                CardID = 31,
                AccountID = 31,
                Paymentmethodid = 2,
                IdDeliveryAdress = 31,
                DiscountID = null,
                OrderName = "Garrett",
                OrderFirstName = "Head",
                PhoneOrder = "04 43 16 34 71",
                CellPhone = "06 82 28 89 08",
                Company = null,
                AdressAdditional = null,
                OrderInstructions = null,
                OrderDate = new DateTime(2023, 03, 14),
                DeliveryPrice = 0,
                Sms = false
            };
            // Act
            var actionResult = _controller.PostOrder(ord).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Order>), "Not an ActionResult<Order>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Order), "Not an Order");
            ord.OrderID = ((Order)result.Value).OrderID;
            Assert.AreEqual(ord, (Order)result.Value, "Orders not equals");
        }

        [TestMethod]
        public async Task PutOrder_ReturnsNotFound_WithMoq() {
            // Arrange
            Order newOrder = new Order {
                OrderID = 31,
                StateOrderID = 3,
                IdDeliveryMethod = 2,
                CardID = 31,
                AccountID = 31,
                Paymentmethodid = 2,
                IdDeliveryAdress = 31,
                DiscountID = null,
                OrderName = "Garrett",
                OrderFirstName = "Head",
                PhoneOrder = "04 43 16 34 71",
                CellPhone = "06 82 28 89 08",
                Company = null,
                AdressAdditional = null,
                OrderInstructions = null,
                OrderDate = new DateTime(2023, 03, 14),
                DeliveryPrice = 0,
                Sms = false
            };
            Order oldOrder = new Order {
                OrderID = 5000,
                StateOrderID = 3,
                IdDeliveryMethod = 2,
                CardID = 31,
                AccountID = 31,
                Paymentmethodid = 2,
                IdDeliveryAdress = 31,
                DiscountID = null,
                OrderName = "Garrett",
                OrderFirstName = "Head",
                PhoneOrder = "04 43 16 34 71",
                CellPhone = "06 82 28 89 08",
                Company = null,
                AdressAdditional = null,
                OrderInstructions = null,
                OrderDate = new DateTime(2023, 03, 14),
                DeliveryPrice = 10,
                Sms = false
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newOrder.OrderID).Result).Returns(newOrder);
            // Act
            var actionResult = _controller.PutOrder(oldOrder.OrderID, oldOrder).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutOrder_ReturnsOk_WithMoq() {
            // Arrange
            Order newOrder = new Order {
                OrderID = 31,
                StateOrderID = 3,
                IdDeliveryMethod = 2,
                CardID = 31,
                AccountID = 31,
                Paymentmethodid = 2,
                IdDeliveryAdress = 31,
                DiscountID = null,
                OrderName = "Garrett",
                OrderFirstName = "Head",
                PhoneOrder = "04 43 16 34 71",
                CellPhone = "06 82 28 89 08",
                Company = null,
                AdressAdditional = null,
                OrderInstructions = null,
                OrderDate = new DateTime(2023, 03, 14),
                DeliveryPrice = 0,
                Sms = false
            };
            Order oldOrder = new Order {
                OrderID = 31,
                StateOrderID = 3,
                IdDeliveryMethod = 2,
                CardID = 31,
                AccountID = 31,
                Paymentmethodid = 2,
                IdDeliveryAdress = 31,
                DiscountID = null,
                OrderName = "Garrett",
                OrderFirstName = "Head",
                PhoneOrder = "04 43 16 34 71",
                CellPhone = "06 82 28 89 08",
                Company = null,
                AdressAdditional = null,
                OrderInstructions = null,
                OrderDate = new DateTime(2023, 03, 14),
                DeliveryPrice = 10,
                Sms = false
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newOrder.OrderID).Result).Returns(newOrder);
            // Act
            var actionResult = _controller.PutOrder(oldOrder.OrderID, oldOrder).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutOrder_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            Order newOrder = new Order {
                OrderID = 31,
                StateOrderID = 3,
                IdDeliveryMethod = 2,
                CardID = 31,
                AccountID = 31,
                Paymentmethodid = 2,
                IdDeliveryAdress = 31,
                DiscountID = null,
                OrderName = "Garrett",
                OrderFirstName = "Head",
                PhoneOrder = "04 43 16 34 71",
                CellPhone = "06 82 28 89 08",
                Company = null,
                AdressAdditional = null,
                OrderInstructions = null,
                OrderDate = new DateTime(2023, 03, 14),
                DeliveryPrice = 0,
                Sms = false
            };
            Order oldOrder = new Order {
                OrderID = 31,
                StateOrderID = 3,
                IdDeliveryMethod = 2,
                CardID = 31,
                AccountID = 31,
                Paymentmethodid = 2,
                IdDeliveryAdress = 31,
                DiscountID = null,
                OrderName = "Garrett",
                OrderFirstName = "Head",
                PhoneOrder = "04 43 16 34 71",
                CellPhone = "06 82 28 89 08",
                Company = null,
                AdressAdditional = null,
                OrderInstructions = null,
                OrderDate = new DateTime(2023, 03, 14),
                DeliveryPrice = 10,
                Sms = false
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newOrder.OrderID).Result).Returns(newOrder);
            // Act
            var actionResult = _controller.PutOrder(id, oldOrder).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }

        [TestMethod]
        public async Task DeleteOrderTest_ReturnsOk_WithMoq() {
            // Arrange
            Order ord = new Order {
                OrderID = 31,
                StateOrderID = 3,
                IdDeliveryMethod = 2,
                CardID = 31,
                AccountID = 31,
                Paymentmethodid = 2,
                IdDeliveryAdress = 31,
                DiscountID = null,
                OrderName = "Garrett",
                OrderFirstName = "Head",
                PhoneOrder = "04 43 16 34 71",
                CellPhone = "06 82 28 89 08",
                Company = null,
                AdressAdditional = null,
                OrderInstructions = null,
                OrderDate = new DateTime(2023, 03, 14),
                DeliveryPrice = 0,
                Sms = false
            };
            _mockRepository.Setup(x => x.GetByIdAsync(ord.OrderID).Result).Returns(ord);
            // Act
            var actionResult = _controller.DeleteOrder(ord.OrderID).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteOrderTest_ReturnsNotFound_WithMoq() {
            // Arrange
            Order ord = new Order {
                OrderID = 5000,
                StateOrderID = 3,
                IdDeliveryMethod = 2,
                CardID = 31,
                AccountID = 31,
                Paymentmethodid = 2,
                IdDeliveryAdress = 31,
                DiscountID = null,
                OrderName = "Garrett",
                OrderFirstName = "Head",
                PhoneOrder = "04 43 16 34 71",
                CellPhone = "06 82 28 89 08",
                Company = null,
                AdressAdditional = null,
                OrderInstructions = null,
                OrderDate = new DateTime(2023, 03, 14),
                DeliveryPrice = 0,
                Sms = false
            };
            _mockRepository.Setup(x => x.GetByIdAsync(ord.OrderID).Result).Returns(ord);
            // Act
            var actionResult = _controller.DeleteOrder(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }
    }
}