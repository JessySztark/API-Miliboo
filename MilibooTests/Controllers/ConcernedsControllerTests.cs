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
    public class ConcernedControllertest {
        private Mock<IDataRepository<Concerned>> _mockRepository;
        private ConcernedsController _controller;
        private MilibooDBContext context;
        private IDataRepository<Concerned> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<Concerned>>();
            _controller = new ConcernedsController(_mockRepository.Object);
        }

        public ConcernedControllertest() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new ConcernedManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<Concerned> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetConcerneds_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetConcerned();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<Concerned>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetConcernedById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetConcernedByID(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostConcerned_ModelValidated_CreationOK_WithMoq() {
            Concerned con = new Concerned {
                ConcernedId = 1,
                Quantity = 6,
                ProductsNavigation = new Product { ProductId= 1 },
                OrdersNavigation = new Order { OrderID = 1 }
            };
            // Act
            var actionResult = _controller.PostConcerned(con).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Concerned>), "Not an ActionResult<Concerned>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Concerned), "Not a Concerned");
            con.ConcernedId = ((Concerned)result.Value).ConcernedId;
            Assert.AreEqual(con, (Concerned)result.Value, "Concerneds not equals");
        }

        [TestMethod]
        public async Task PutConcerned_ReturnsNotFound_WithMoq() {
            // Arrange
            Concerned newConcerned = new Concerned {
                ConcernedId = 1,
                Quantity = 6,
                ProductsNavigation = new Product { ProductId = 1 },
                OrdersNavigation = new Order { OrderID = 1 }
            };
            Concerned oldConcerned = new Concerned {
                ConcernedId = 5000,
                Quantity = 6,
                ProductsNavigation = new Product { ProductId = 1 },
                OrdersNavigation = new Order { OrderID = 1 }
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newConcerned.ConcernedId).Result).Returns(newConcerned);
            // Act
            var actionResult = _controller.PutConcerned(oldConcerned.ConcernedId, oldConcerned).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutConcerned_ReturnsOk_WithMoq() {
            // Arrange
            Concerned newConcerned = new Concerned {
                ConcernedId = 1,
                Quantity = 6,
                ProductsNavigation = new Product { ProductId = 1 },
                OrdersNavigation = new Order { OrderID = 1 }
            };
            Concerned oldConcerned = new Concerned {
                ConcernedId = 1,
                Quantity = 6,
                ProductsNavigation = new Product { ProductId = 1 },
                OrdersNavigation = new Order { OrderID = 1 }
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newConcerned.ConcernedId).Result).Returns(newConcerned);
            // Act
            var actionResult = _controller.PutConcerned(oldConcerned.ConcernedId, oldConcerned).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutConcerned_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            Concerned newConcerned = new Concerned {
                ConcernedId = 1,
                Quantity = 6,
                ProductsNavigation = new Product { ProductId = 1 },
                OrdersNavigation = new Order { OrderID = 1 }
            };
            Concerned oldConcerned = new Concerned {
                ConcernedId = 1,
                Quantity = 6,
                ProductsNavigation = new Product { ProductId = 1 },
                OrdersNavigation = new Order { OrderID = 1 }
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newConcerned.ConcernedId).Result).Returns(newConcerned);
            // Act
            var actionResult = _controller.PutConcerned(id, oldConcerned).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }

        [TestMethod]
        public async Task DeleteConcernedTest_ReturnsOk_WithMoq() {
            // Arrange
            Concerned con = new Concerned {
                ConcernedId = 1,
                Quantity = 6,
                ProductsNavigation = new Product { ProductId = 1 },
                OrdersNavigation = new Order { OrderID = 1 }
            };
            _mockRepository.Setup(x => x.GetByIdAsync(con.ConcernedId).Result).Returns(con);
            // Act
            var actionResult = _controller.DeleteConcerned(con.ConcernedId).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteConcernedTest_ReturnsNotFound_WithMoq() {
            // Arrange
            Concerned con = new Concerned {
                ConcernedId = 5000,
                Quantity = 6,
                ProductsNavigation = new Product { ProductId = 1 },
                OrdersNavigation = new Order { OrderID = 1 }
            };
            _mockRepository.Setup(x => x.GetByIdAsync(con.ConcernedId).Result).Returns(con);
            // Act
            var actionResult = _controller.DeleteConcerned(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }
    }
}