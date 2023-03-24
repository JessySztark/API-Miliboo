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
    public class AsFiltersControllerTests {
        private Mock<IDataRepository<AsFilter>> _mockRepository;
        private AsFiltersController _controller;
        private MilibooDBContext context;
        private IDataRepository<AsFilter> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<AsFilter>>();
            _controller = new AsFiltersController(_mockRepository.Object);
        }

        public AsFiltersControllerTests() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new AsFilterManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<AsFilter> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetAsFilters_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetAsFilter();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<AsFilter>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetAsFilterById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetAsFilter(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostAsFilter_ModelValidated_CreationOK_WithMoq() {
            AsFilter asf = new AsFilter {
                FilterCategoryId = 1,
                ProductCategoryId = 1,
            };
            // Act
            var actionResult = _controller.PostAsFilter(asf).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<AsFilter>), "Not an ActionResult<AsFilter>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(AsFilter), "Not an AsFilter");
            asf.FilterCategoryId = ((AsFilter)result.Value).FilterCategoryId;
            asf.ProductCategoryId = ((AsFilter)result.Value).ProductCategoryId;
            Assert.AreEqual(asf, (AsFilter)result.Value, "AsFilters not equals");
        }


        [TestMethod]
        public async Task DeleteAsFilterTest_ReturnsNoContent_WithMoq() {
            // Arrange
            AsFilter asf = new AsFilter {
                FilterCategoryId = 1,
                ProductCategoryId = 1,
            };
            _mockRepository.Setup(x => x.GetByIdAsync(asf.FilterCategoryId).Result).Returns(asf);
            // Act
            var actionResult = _controller.DeleteAsFilter(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteAsFilterTest_ReturnsNotFound_WithMoq() {
            // Arrange
            AsFilter asf = new AsFilter {
                FilterCategoryId = 5000,
                ProductCategoryId = 1,
            };
            _mockRepository.Setup(x => x.GetByIdAsync(asf.FilterCategoryId).Result).Returns(asf);
            // Act
            var actionResult = _controller.DeleteAsFilter(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutAsFilter_ReturnsNotFound_WithMoq() {
            // Arrange
            AsFilter newAsFilter = new AsFilter {
                FilterCategoryId = 1,
                ProductCategoryId = 1,
            };
            AsFilter oldAsFilter = new AsFilter {
                FilterCategoryId = 5000,
                ProductCategoryId = 1,
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newAsFilter.FilterCategoryId).Result).Returns(newAsFilter);
            // Act
            var actionResult = _controller.PutAsFilter(oldAsFilter.FilterCategoryId, oldAsFilter).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutAsFilter_ReturnsOk_WithMoq() {
            // Arrange
            AsFilter newAsFilter = new AsFilter {
                FilterCategoryId = 1,
                ProductCategoryId = 1,
            };
            AsFilter oldAsFilter = new AsFilter {
                FilterCategoryId = 1,
                ProductCategoryId = 1,
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newAsFilter.FilterCategoryId).Result).Returns(newAsFilter);
            // Act
            var actionResult = _controller.PutAsFilter(oldAsFilter.FilterCategoryId, oldAsFilter).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutAsFilter_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            AsFilter newAsFilter = new AsFilter {
                FilterCategoryId = 1,
                ProductCategoryId = 1,
            };
            AsFilter oldAsFilter = new AsFilter {
                FilterCategoryId = 5000,
                ProductCategoryId = 1,
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newAsFilter.FilterCategoryId).Result).Returns(newAsFilter);
            // Act
            var actionResult = _controller.PutAsFilter(id, oldAsFilter).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }
    }
}