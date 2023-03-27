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
    public class DiscountsControllertest {
        private Mock<IDataRepository<Discount>> _mockRepository;
        private DiscountsController _controller;
        private MilibooDBContext context;
        private IDataRepository<Discount> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<Discount>>();
            _controller = new DiscountsController(_mockRepository.Object);
        }

        public DiscountsControllertest() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new DiscountManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<Discount> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetDiscounts_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetDiscounts();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<Discount>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetDiscountById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetDiscountById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostDiscount_ModelValidated_CreationOK_WithMoq() {
            Discount dsc = new Discount {
                DiscountID = 1,
                DiscountName = "ARTHUR33",
                IsActive = false,
                DiscountValue = 33
            };
            // Act
            var actionResult = _controller.PostDiscount(dsc).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Discount>), "Not an ActionResult<Discount>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Discount), "Not a Discount");
            dsc.DiscountID = ((Discount)result.Value).DiscountID;
            Assert.AreEqual(dsc, (Discount)result.Value, "Discounts not equals");
        }

        [TestMethod]
        public async Task PutDiscount_ReturnsNotFound_WithMoq() {
            // Arrange
            Discount newDiscount = new Discount {
                DiscountID = 1,
                DiscountName = "ARTHUR33",
                IsActive = false,
                DiscountValue = 33
            };
            Discount oldDiscount = new Discount {
                DiscountID = 5000,
                DiscountName = "ARTHUR99",
                IsActive = false,
                DiscountValue = 99
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newDiscount.DiscountID).Result).Returns(newDiscount);
            // Act
            var actionResult = _controller.PutDiscount(oldDiscount.DiscountID, oldDiscount).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutDiscount_ReturnsOk_WithMoq() {
            // Arrange
            Discount newDiscount = new Discount {
                DiscountID = 1,
                DiscountName = "ARTHUR33",
                IsActive = false,
                DiscountValue = 33
            };
            Discount oldDiscount = new Discount {
                DiscountID = 1,
                DiscountName = "ARTHUR99",
                IsActive = false,
                DiscountValue = 99
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newDiscount.DiscountID).Result).Returns(newDiscount);
            // Act
            var actionResult = _controller.PutDiscount(oldDiscount.DiscountID, oldDiscount).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutDiscount_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            Discount newDiscount = new Discount {
                DiscountID = 1,
                DiscountName = "ARTHUR33",
                IsActive = false,
                DiscountValue = 33
            };
            Discount oldDiscount = new Discount {
                DiscountID = 1,
                DiscountName = "ARTHUR99",
                IsActive = false,
                DiscountValue = 99
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newDiscount.DiscountID).Result).Returns(newDiscount);
            // Act
            var actionResult = _controller.PutDiscount(id, oldDiscount).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }

        [TestMethod]
        public async Task DeleteDiscountTest_ReturnsOk_WithMoq() {
            // Arrange
            Discount dsc = new Discount {
                DiscountID = 1,
                DiscountName = "ARTHUR33",
                IsActive = false,
                DiscountValue = 33
            };
            _mockRepository.Setup(x => x.GetByIdAsync(dsc.DiscountID).Result).Returns(dsc);
            // Act
            var actionResult = _controller.DeleteDiscount(dsc.DiscountID).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteDiscountTest_ReturnsNotFound_WithMoq() {
            // Arrange
            Discount dsc = new Discount {
                DiscountID = 5000,
                DiscountName = "ARTHUR33",
                IsActive = false,
                DiscountValue = 33
            };
            _mockRepository.Setup(x => x.GetByIdAsync(dsc.DiscountID).Result).Returns(dsc);
            // Act
            var actionResult = _controller.DeleteDiscount(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }
    }
}