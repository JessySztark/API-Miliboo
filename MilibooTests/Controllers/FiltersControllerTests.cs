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
    public class FiltersControllertest {
        private Mock<IDataRepository<Filter>> _mockRepository;
        private FiltersController _controller;
        private MilibooDBContext context;
        private IDataRepository<Filter> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<Filter>>();
            _controller = new FiltersController(_mockRepository.Object);
        }

        public FiltersControllertest() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new FilterManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<Filter> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetFilters_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetFilters();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<Filter>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetFilterById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetFilterById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostFilter_ModelValidated_CreationOK_WithMoq() {
            Filter fca = new Filter {
                FilterId = 14,
                FilterName = "Angle droit"
            };
            // Act
            var actionResult = _controller.PostFilter(fca).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Filter>), "Not an ActionResult<Filter>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Filter), "Not a Filter");
            fca.FilterId = ((Filter)result.Value).FilterId;
            Assert.AreEqual(fca, (Filter)result.Value, "Filters not equals");
        }

        [TestMethod]
        public async Task PutFilter_ReturnsNotFound_WithMoq() {
            // Arrange
            Filter newFilter = new Filter {
                FilterId = 14,
                FilterName = "Angle droit"
            };
            Filter oldFilter = new Filter {
                FilterId = 5000,
                FilterName = "Angle droit"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newFilter.FilterId).Result).Returns(newFilter);
            // Act
            var actionResult = _controller.PutFilter(oldFilter.FilterId, oldFilter).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutFilter_ReturnsOk_WithMoq() {
            // Arrange
            Filter newFilter = new Filter {
                FilterId = 14,
                FilterName = "Angle droit"
            };
            Filter oldFilter = new Filter {
                FilterId = 14,
                FilterName = "Angle droit"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newFilter.FilterId).Result).Returns(newFilter);
            // Act
            var actionResult = _controller.PutFilter(oldFilter.FilterId, oldFilter).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutFilter_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            Filter newFilter = new Filter {
                FilterId = 14,
                FilterName = "Angle droit"
            };
            Filter oldFilter = new Filter {
                FilterId = 14,
                FilterName = "Angle droit"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newFilter.FilterId).Result).Returns(newFilter);
            // Act
            var actionResult = _controller.PutFilter(id, oldFilter).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }

        [TestMethod]
        public async Task DeleteFilterTest_ReturnsOk_WithMoq() {
            // Arrange
            Filter fca = new Filter {
                FilterId = 14,
                FilterName = "Angle droit"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(fca.FilterId).Result).Returns(fca);
            // Act
            var actionResult = _controller.DeleteFilter(fca.FilterId).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteFilterTest_ReturnsNotFound_WithMoq() {
            // Arrange
            Filter fca = new Filter {
                FilterId = 5000,
                FilterName = "Angle droit"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(fca.FilterId).Result).Returns(fca);
            // Act
            var actionResult = _controller.DeleteFilter(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }
    }
}