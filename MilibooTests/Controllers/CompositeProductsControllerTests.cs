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
    public class CompositeProductControllertest {
        private Mock<IDataRepository<CompositeProduct>> _mockRepository;
        private CompositeProductsController _controller;
        private MilibooDBContext context;
        private IDataRepository<CompositeProduct> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<CompositeProduct>>();
            _controller = new CompositeProductsController(_mockRepository.Object);
        }

        public CompositeProductControllertest() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new CompositeProductManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<CompositeProduct> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetCompositeProducts_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetCompositeProduct();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<CompositeProduct>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetCompositeProductById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetCompositeProductByID(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostCompositeProduct_ModelValidated_CreationOK_WithMoq() {
            CompositeProduct cmp = new CompositeProduct {
                CompositeID = 16,
                ProductId = 28,
                CompositeproductID = 80,
                CompositeDescription = "chaise"
            };
            // Act
            var actionResult = _controller.PostCompositeProduct(cmp).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<CompositeProduct>), "Not an ActionResult<CompositeProduct>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(CompositeProduct), "Not a CompositeProduct");
            cmp.CompositeID = ((CompositeProduct)result.Value).CompositeID;
            Assert.AreEqual(cmp, (CompositeProduct)result.Value, "CompositeProducts not equals");
        }

        [TestMethod]
        public async Task PutCompositeProduct_ReturnsNotFound_WithMoq() {
            // Arrange
            CompositeProduct newCompositeProduct = new CompositeProduct {
                CompositeID = 16,
                ProductId = 28,
                CompositeproductID = 80,
                CompositeDescription = "chaise"
            };
            CompositeProduct oldCompositeProduct = new CompositeProduct {
                CompositeID = 5000,
                ProductId = 28,
                CompositeproductID = 80,
                CompositeDescription = "chaise"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newCompositeProduct.CompositeID).Result).Returns(newCompositeProduct);
            // Act
            var actionResult = _controller.PutCompositeProduct(oldCompositeProduct.CompositeID, oldCompositeProduct).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutCompositeProduct_ReturnsOk_WithMoq() {
            // Arrange
            CompositeProduct newCompositeProduct = new CompositeProduct {
                CompositeID = 16,
                ProductId = 28,
                CompositeproductID = 80,
                CompositeDescription = "chaise"
            };
            CompositeProduct oldCompositeProduct = new CompositeProduct {
                CompositeID = 16,
                ProductId = 28,
                CompositeproductID = 80,
                CompositeDescription = "chaise longue"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newCompositeProduct.CompositeID).Result).Returns(newCompositeProduct);
            // Act
            var actionResult = _controller.PutCompositeProduct(oldCompositeProduct.CompositeID, oldCompositeProduct).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutCompositeProduct_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            CompositeProduct newCompositeProduct = new CompositeProduct {
                CompositeID = 16,
                ProductId = 28,
                CompositeproductID = 80,
                CompositeDescription = "chaise"
            };
            CompositeProduct oldCompositeProduct = new CompositeProduct {
                CompositeID = 16,
                ProductId = 28,
                CompositeproductID = 80,
                CompositeDescription = "chaise"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newCompositeProduct.CompositeID).Result).Returns(newCompositeProduct);
            // Act
            var actionResult = _controller.PutCompositeProduct(id, oldCompositeProduct).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }

        [TestMethod]
        public async Task DeleteCompositeProductTest_ReturnsOk_WithMoq() {
            // Arrange
            CompositeProduct com = new CompositeProduct {
                CompositeID = 16,
                ProductId = 28,
                CompositeproductID = 80,
                CompositeDescription = "chaise"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(com.CompositeID).Result).Returns(com);
            // Act
            var actionResult = _controller.DeleteCompositeProduct(com.CompositeID).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteCompositeProductTest_ReturnsNotFound_WithMoq() {
            // Arrange
            CompositeProduct cmp = new CompositeProduct {
                CompositeID = 5000,
                ProductId = 28,
                CompositeproductID = 80,
                CompositeDescription = "chaise"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(cmp.CompositeID).Result).Returns(cmp);
            // Act
            var actionResult = _controller.DeleteCompositeProduct(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }
    }
}