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
    public class IsFilteredsControllertest {
        private Mock<IDataRepository<IsFiltered>> _mockRepository;
        private IsFilteredsController _controller;
        private MilibooDBContext context;
        private IDataRepository<IsFiltered> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<IsFiltered>>();
            _controller = new IsFilteredsController(_mockRepository.Object);
        }

        public IsFilteredsControllertest() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new IsFilteredManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<IsFiltered> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetIsFiltereds_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetIsFiltereds();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<IsFiltered>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetIsFilteredById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetIsFilteredById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostIsFiltered_ModelValidated_CreationOK_WithMoq() {
            IsFiltered isf = new IsFiltered {
                IsFilteredId = 63,
                FiltersNavigation = new Filter { FilterId= 34 },
                ProductsNavigation = new Product { ProductId= 18 }
            };
            // Act
            var actionResult = _controller.PostIsFiltered(isf).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<IsFiltered>), "Not an ActionResult<IsFiltered>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(IsFiltered), "Not a IsFiltered");
            isf.IsFilteredId = ((IsFiltered)result.Value).IsFilteredId;
            Assert.AreEqual(isf, (IsFiltered)result.Value, "IsFiltereds not equals");
        }

        [TestMethod]
        public async Task PutIsFiltered_ReturnsNotFound_WithMoq() {
            // Arrange
            IsFiltered newIsFiltered = new IsFiltered {
                IsFilteredId = 63,
                FiltersNavigation = new Filter { FilterId = 34 },
                ProductsNavigation = new Product { ProductId = 18 }
            };
            IsFiltered oldIsFiltered = new IsFiltered {
                IsFilteredId = 5000,
                FiltersNavigation = new Filter { FilterId = 34 },
                ProductsNavigation = new Product { ProductId = 18 }
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newIsFiltered.IsFilteredId).Result).Returns(newIsFiltered);
            // Act
            var actionResult = _controller.PutIsFiltered(oldIsFiltered.IsFilteredId, oldIsFiltered).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutIsFiltered_ReturnsOk_WithMoq() {
            // Arrange
            IsFiltered newIsFiltered = new IsFiltered {
                IsFilteredId = 63,
                FiltersNavigation = new Filter { FilterId = 34 },
                ProductsNavigation = new Product { ProductId = 18 }
            };
            IsFiltered oldIsFiltered = new IsFiltered {
                IsFilteredId = 63,
                FiltersNavigation = new Filter { FilterId = 34 },
                ProductsNavigation = new Product { ProductId = 18 }
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newIsFiltered.IsFilteredId).Result).Returns(newIsFiltered);
            // Act
            var actionResult = _controller.PutIsFiltered(oldIsFiltered.IsFilteredId, oldIsFiltered).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutIsFiltered_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            IsFiltered newIsFiltered = new IsFiltered {
                IsFilteredId = 63,
                FiltersNavigation = new Filter { FilterId = 34 },
                ProductsNavigation = new Product { ProductId = 18 }
            };
            IsFiltered oldIsFiltered = new IsFiltered {
                IsFilteredId = 63,
                FiltersNavigation = new Filter { FilterId = 34 },
                ProductsNavigation = new Product { ProductId = 18 }
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newIsFiltered.IsFilteredId).Result).Returns(newIsFiltered);
            // Act
            var actionResult = _controller.PutIsFiltered(id, oldIsFiltered).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }

        [TestMethod]
        public async Task DeleteIsFilteredTest_ReturnsOk_WithMoq() {
            // Arrange
            IsFiltered isf = new IsFiltered {
                IsFilteredId = 63,
                FiltersNavigation = new Filter { FilterId = 34 },
                ProductsNavigation = new Product { ProductId = 18 }
            };
            _mockRepository.Setup(x => x.GetByIdAsync(isf.IsFilteredId).Result).Returns(isf);
            // Act
            var actionResult = _controller.DeleteIsFiltered(isf.IsFilteredId).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteIsFilteredTest_ReturnsNotFound_WithMoq() {
            // Arrange
            IsFiltered isf = new IsFiltered {
                IsFilteredId = 5000,
                FiltersNavigation = new Filter { FilterId = 34 },
                ProductsNavigation = new Product { ProductId = 18 }
            };
            _mockRepository.Setup(x => x.GetByIdAsync(isf.IsFilteredId).Result).Returns(isf);
            // Act
            var actionResult = _controller.DeleteIsFiltered(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }
    }
}