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
    public class ProductCategoriesControllerTests {
        private Mock<IDataRepository<ProductCategory>> _mockRepository;
        private ProductCategoriesController _controller;
        private MilibooDBContext context;
        private IDataRepository<ProductCategory> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<ProductCategory>>();
            _controller = new ProductCategoriesController(_mockRepository.Object);
        }

        public ProductCategoriesControllerTests() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new ProductCategoryManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<ProductCategory> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetProductCategories_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetProductCategories();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<ProductCategory>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetProductCategoryById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetProductCategoryById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task GetProductTypeByName_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetOrderByCategoryName("Buro").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostProductCategory_ModelValidated_CreationOK_WithMoq() {
            ProductCategory pcy = new ProductCategory {
                ProductCategoryId = 1,
                ProductCategoriesProduct = new List<Product> { new Product {  } },
                ProductCategoryName = "Canapé & Fauteuil"
            };
            // Act
            var actionResult = _controller.PostProductCategory(pcy).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<ProductCategory>), "Not an ActionResult<ProductCategory>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(ProductCategory), "Not an ProductCategory");
            pcy.ProductCategoryId = ((ProductCategory)result.Value).ProductCategoryId;
            Assert.AreEqual(pcy, (ProductCategory)result.Value, "ProductCategories not equals");
        }


        [TestMethod]
        public async Task DeleteProductCategoryTest_ReturnsOk_WithMoq() {
            // Arrange
            ProductCategory pcy = new ProductCategory {
                ProductCategoryId = 1,
                ProductCategoriesProduct = new List<Product> { new Product { } },
                ProductCategoryName = "Canapé & Fauteuil"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(pcy.ProductCategoryId).Result).Returns(pcy);
            // Act
            var actionResult = _controller.DeleteProductCategory(pcy.ProductCategoryId).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteProductCategoryTest_ReturnsNotFound_WithMoq() {
            // Arrange
            ProductCategory pcy = new ProductCategory {
                ProductCategoryId = 5000,
                ProductCategoriesProduct = new List<Product> { new Product { } },
                ProductCategoryName = "Canapé & Fauteuil"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(pcy.ProductCategoryId).Result).Returns(pcy);
            // Act
            var actionResult = _controller.DeleteProductCategory(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutProductCategory_ReturnsNotFound_WithMoq() {
            // Arrange
            ProductCategory newProductCategory = new ProductCategory {
                ProductCategoryId = 1,
                ProductCategoriesProduct = new List<Product> { new Product { } },
                ProductCategoryName = "Canapé & Fauteuil"
            };
            ProductCategory oldProductCategory = new ProductCategory {
                ProductCategoryId = 5000,
                ProductCategoriesProduct = new List<Product> { new Product { } },
                ProductCategoryName = "Canapé & Fauteuil"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newProductCategory.ProductCategoryId).Result).Returns(newProductCategory);
            // Act
            var actionResult = _controller.PutProductCategory(oldProductCategory.ProductCategoryId, oldProductCategory).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutProductCategory_ReturnsOk_WithMoq() {
            // Arrange
            ProductCategory newProductCategory = new ProductCategory {
                ProductCategoryId = 1,
                ProductCategoriesProduct = new List<Product> { new Product { } },
                ProductCategoryName = "Canapé & Fauteuil"
            };
            ProductCategory oldProductCategory = new ProductCategory {
                ProductCategoryId = 1,
                ProductCategoriesProduct = new List<Product> { new Product { } },
                ProductCategoryName = "Canapé & Fauteuil"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newProductCategory.ProductCategoryId).Result).Returns(newProductCategory);
            // Act
            var actionResult = _controller.PutProductCategory(oldProductCategory.ProductCategoryId, oldProductCategory).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutProductCategory_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            ProductCategory newProductCategory = new ProductCategory {
                ProductCategoryId = 1,
                ProductCategoriesProduct = new List<Product> { new Product { } },
                ProductCategoryName = "Canapé & Fauteuil"
            };
            ProductCategory oldProductCategory = new ProductCategory {
                ProductCategoryId = 5000,
                ProductCategoriesProduct = new List<Product> { new Product { } },
                ProductCategoryName = "Canapé & Fauteuil"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newProductCategory.ProductCategoryId).Result).Returns(newProductCategory);
            // Act
            var actionResult = _controller.PutProductCategory(id, oldProductCategory).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }
    }
}