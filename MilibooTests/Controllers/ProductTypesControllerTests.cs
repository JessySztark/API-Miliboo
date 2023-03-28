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
    public class ProductTypesControllerTests {
        private Mock<IDataRepository<ProductType>> _mockRepository;
        private ProductTypesController _controller;
        private MilibooDBContext context;
        private IDataRepository<ProductType> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<ProductType>>();
            _controller = new ProductTypesController(_mockRepository.Object);
        }

        public ProductTypesControllerTests() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new ProductTypeManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<ProductType> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetProductTypes_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetProductTypes();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<ProductType>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetProductTypeById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetProductTypeById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task GetProductTypeByName_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetProductTypeByName("Siège en titane").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostProductType_ModelValidated_CreationOK_WithMoq() {
            ProductType ptt = new ProductType {
                ProductTypeId = 1,
                ProductTypeName= "Canapé scandinave 3 places PAPEL",
                PTMaintenanceComment = "Pour un nettoyage efficace et optimal, il vous su..."
            };
            // Act
            var actionResult = _controller.PostProductType(ptt).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<ProductType>), "Not an ActionResult<ProductType>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(ProductType), "Not an ProductType");
            ptt.ProductTypeId = ((ProductType)result.Value).ProductTypeId;
            ptt.ProductTypeId = ((ProductType)result.Value).ProductTypeId;
            Assert.AreEqual(ptt, (ProductType)result.Value, "ProductTypes not equals");
        }


        [TestMethod]
        public async Task DeleteProductTypeTest_ReturnsOk_WithMoq() {
            // Arrange
            ProductType ptt = new ProductType {
                ProductTypeId = 1,
                ProductTypeName = "Canapé scandinave 3 places PAPEL",
                PTMaintenanceComment = "Pour un nettoyage efficace et optimal, il vous su..."
            };
            _mockRepository.Setup(x => x.GetByIdAsync(ptt.ProductTypeId).Result).Returns(ptt);
            // Act
            var actionResult = _controller.DeleteProductType(ptt.ProductTypeId).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteProductTypeTest_ReturnsNotFound_WithMoq() {
            // Arrange
            ProductType ptt = new ProductType {
                ProductTypeId = 5000,
                ProductTypeName = "Canapé scandinave 3 places PAPEL",
                PTMaintenanceComment = "Pour un nettoyage efficace et optimal, il vous su..."
            };
            _mockRepository.Setup(x => x.GetByIdAsync(ptt.ProductTypeId).Result).Returns(ptt);
            // Act
            var actionResult = _controller.DeleteProductType(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutProductType_ReturnsNotFound_WithMoq() {
            // Arrange
            ProductType newProductType = new ProductType {
                ProductTypeId = 1,
                ProductTypeName = "Canapé scandinave 3 places PAPEL",
                PTMaintenanceComment = "Pour un nettoyage efficace et optimal, il vous su..."
            };
            ProductType oldProductType = new ProductType {
                ProductTypeId = 5000,
                ProductTypeName = "Canapé scandinave 3 places PAPEL",
                PTMaintenanceComment = "Pour un nettoyage efficace et optimal, il vous suffit de nettoyer plus fort"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newProductType.ProductTypeId).Result).Returns(newProductType);
            // Act
            var actionResult = _controller.PutProductType(oldProductType.ProductTypeId, oldProductType).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutProductType_ReturnsOk_WithMoq() {
            // Arrange
            ProductType newProductType = new ProductType {
                ProductTypeId = 1,
                ProductTypeName = "Canapé scandinave 3 places PAPEL",
                PTMaintenanceComment = "Pour un nettoyage efficace et optimal, il vous su..."
            };
            ProductType oldProductType = new ProductType {
                ProductTypeId = 1,
                ProductTypeName = "Canapé scandinave 3 places PAPEL",
                PTMaintenanceComment = "Pour un nettoyage efficace et optimal, il vous suffit de nettoyer plus fort"
            };


            _mockRepository.Setup(x => x.GetByIdAsync(newProductType.ProductTypeId).Result).Returns(newProductType);
            // Act
            var actionResult = _controller.PutProductType(oldProductType.ProductTypeId, oldProductType).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutProductType_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            ProductType newProductType = new ProductType {
                ProductTypeId = 1,
                ProductTypeName = "Canapé scandinave 3 places PAPEL",
                PTMaintenanceComment = "Pour un nettoyage efficace et optimal, il vous su..."
            };
            ProductType oldProductType = new ProductType {
                ProductTypeId = 5000,
                ProductTypeName = "Canapé scandinave 3 places PAPEL",
                PTMaintenanceComment = "Pour un nettoyage efficace et optimal, il vous suffit de nettoyer plus fort"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newProductType.ProductTypeId).Result).Returns(newProductType);
            // Act
            var actionResult = _controller.PutProductType(id, oldProductType).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }
    }
}