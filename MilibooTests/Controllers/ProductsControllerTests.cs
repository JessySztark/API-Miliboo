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
    public class ProductsControllerTests {
        private Mock<IDataRepository<Product>> _mockRepository;
        private ProductsController _controller;
        private MilibooDBContext context;
        private IDataRepository<Product> dataRepository;
        private readonly MilibooDBContext _context;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<Product>>();
            _controller = new ProductsController(_mockRepository.Object, _context);
        }

        public ProductsControllerTests() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new ProductManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<Product> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetProducts_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetProducts();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<Product>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetProductById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetProductById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostProduct_ModelValidated_CreationOK_WithMoq() {
            Product prt = new Product {
                ProductId = 28,
                ColorsNavigation = new Color { ColorId = 1 },
                ProductTypesNavigation = new ProductType { ProductTypeId = 7 },
                ProductCategoriesNavigation = new ProductCategory { ProductCategoryId = 1 },
                ProductName = "Chaises design empilables grises (lot de 2) ANNA",
                ProductDescription = "Et si vous laissiez entrer la couleur dans votre ...",
                ProductPrice = 199.99,
                ProductDiscount = 0,
                NbStockProduct = 5,
                NbReservedProduct = 2
            };
            // Act
            var actionResult = _controller.PostProduct(prt).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Product>), "Not an ActionResult<Product>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Product), "Not an Product");
            prt.ProductId = ((Product)result.Value).ProductId;
            prt.ProductId = ((Product)result.Value).ProductId;
            Assert.AreEqual(prt, (Product)result.Value, "Products not equals");
        }


        [TestMethod]
        public async Task DeleteProductTest_ReturnsOk_WithMoq() {
            // Arrange
            Product prt = new Product {
                ProductId = 28,
                ColorsNavigation = new Color { ColorId = 1 },
                ProductTypesNavigation = new ProductType { ProductTypeId = 7 },
                ProductCategoriesNavigation = new ProductCategory { ProductCategoryId = 1 },
                ProductName = "Chaises design empilables grises (lot de 2) ANNA",
                ProductDescription = "Et si vous laissiez entrer la couleur dans votre ...",
                ProductPrice = 199.99,
                ProductDiscount = 0,
                NbStockProduct = 5,
                NbReservedProduct = 2
            };
            _mockRepository.Setup(x => x.GetByIdAsync((int)prt.ProductId).Result).Returns(prt);
            // Act
            var actionResult = _controller.DeleteProduct((int)prt.ProductId).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteProductTest_ReturnsNotFound_WithMoq() {
            // Arrange
            Product prt = new Product {
                ProductId = 5000,
                ColorsNavigation = new Color { ColorId = 1 },
                ProductTypesNavigation = new ProductType { ProductTypeId = 7 },
                ProductCategoriesNavigation = new ProductCategory { ProductCategoryId = 1 },
                ProductName = "Chaises design empilables grises (lot de 2) ANNA",
                ProductDescription = "Et si vous laissiez entrer la couleur dans votre ...",
                ProductPrice = 199.99,
                ProductDiscount = 0,
                NbStockProduct = 5,
                NbReservedProduct = 2
            };
            _mockRepository.Setup(x => x.GetByIdAsync((int)prt.ProductId).Result).Returns(prt);
            // Act
            var actionResult = _controller.DeleteProduct(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutProduct_ReturnsNotFound_WithMoq() {
            // Arrange
            Product newProduct = new Product {
                ProductId = 28,
                ColorsNavigation = new Color { ColorId = 1 },
                ProductTypesNavigation = new ProductType { ProductTypeId = 7 },
                ProductCategoriesNavigation = new ProductCategory { ProductCategoryId = 1 },
                ProductName = "Chaises design empilables grises (lot de 2) ANNA",
                ProductDescription = "Et si vous laissiez entrer la couleur dans votre ...",
                ProductPrice = 199.99,
                ProductDiscount = 0,
                NbStockProduct = 5,
                NbReservedProduct = 2
            };
            Product oldProduct = new Product {
                ProductId = 5000,
                ColorsNavigation = new Color { ColorId = 1 },
                ProductTypesNavigation = new ProductType { ProductTypeId = 7 },
                ProductCategoriesNavigation = new ProductCategory { ProductCategoryId = 1 },
                ProductName = "Chaises design empilables grises (lot de 2) ANNA",
                ProductDescription = "Et si vous laissiez entrer la couleur dans votre ...",
                ProductPrice = 199.99,
                ProductDiscount = 0,
                NbStockProduct = 5,
                NbReservedProduct = 2
            };

            _mockRepository.Setup(x => x.GetByIdAsync((int)newProduct.ProductId).Result).Returns(newProduct);
            // Act
            var actionResult = _controller.PutProduct((int)oldProduct.ProductId, oldProduct).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutProduct_ReturnsOk_WithMoq() {
            // Arrange
            Product newProduct = new Product {
                ProductId = 28,
                ColorsNavigation = new Color { ColorId = 1 },
                ProductTypesNavigation = new ProductType { ProductTypeId = 7 },
                ProductCategoriesNavigation = new ProductCategory { ProductCategoryId = 1 },
                ProductName = "Chaises design empilables grises (lot de 2) ANNA",
                ProductDescription = "Et si vous laissiez entrer la couleur dans votre ...",
                ProductPrice = 199.99,
                ProductDiscount = 0,
                NbStockProduct = 5,
                NbReservedProduct = 2
            };
            Product oldProduct = new Product {
                ProductId = 28,
                ColorsNavigation = new Color { ColorId = 1 },
                ProductTypesNavigation = new ProductType { ProductTypeId = 7 },
                ProductCategoriesNavigation = new ProductCategory { ProductCategoryId = 1 },
                ProductName = "Chaises design empilables grises (lot de 2) ANNA",
                ProductDescription = "Et si vous laissiez entrer la couleur dans votre ...",
                ProductPrice = 199.99,
                ProductDiscount = 0,
                NbStockProduct = 5,
                NbReservedProduct = 2
            };


            _mockRepository.Setup(x => x.GetByIdAsync((int)newProduct.ProductId).Result).Returns(newProduct);
            // Act
            var actionResult = _controller.PutProduct((int)oldProduct.ProductId, oldProduct).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutProduct_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            Product newProduct = new Product {
                ProductId = 28,
                ColorsNavigation = new Color { ColorId = 1 },
                ProductTypesNavigation = new ProductType { ProductTypeId = 7 },
                ProductCategoriesNavigation = new ProductCategory { ProductCategoryId = 1 },
                ProductName = "Chaises design empilables grises (lot de 2) ANNA",
                ProductDescription = "Et si vous laissiez entrer la couleur dans votre ...",
                ProductPrice = 199.99,
                ProductDiscount = 0,
                NbStockProduct = 5,
                NbReservedProduct = 2
            };
            Product oldProduct = new Product {
                ProductId = 5000,
                ColorsNavigation = new Color { ColorId = 1 },
                ProductTypesNavigation = new ProductType { ProductTypeId = 7 },
                ProductCategoriesNavigation = new ProductCategory { ProductCategoryId = 1 },
                ProductName = "Chaises design empilables grises (lot de 2) ANNA",
                ProductDescription = "Et si vous laissiez entrer la couleur dans votre ...",
                ProductPrice = 199.99,
                ProductDiscount = 0,
                NbStockProduct = 5,
                NbReservedProduct = 2
            };
            _mockRepository.Setup(x => x.GetByIdAsync((int)newProduct.ProductId).Result).Returns(newProduct);
            // Act
            var actionResult = _controller.PutProduct(id, oldProduct).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }
    }
}