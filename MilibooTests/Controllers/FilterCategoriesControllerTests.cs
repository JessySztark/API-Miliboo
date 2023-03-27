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
    public class FilterCategoriesControllertest {
        private Mock<IDataRepository<FilterCategory>> _mockRepository;
        private FilterCategoriesController _controller;
        private MilibooDBContext context;
        private IDataRepository<FilterCategory> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<FilterCategory>>();
            _controller = new FilterCategoriesController(_mockRepository.Object);
        }

        public FilterCategoriesControllertest() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new FilterCategoryManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<FilterCategory> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetFilterCategories_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetFilterCategories();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<FilterCategory>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetFilterCategoryById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetFilterCategoryById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostFilterCategory_ModelValidated_CreationOK_WithMoq() {
            FilterCategory fca = new FilterCategory {
                FilterCategoryId = 1,
                FilterCategoryName = "Type de canapé"
            };
            // Act
            var actionResult = _controller.PostFilterCategory(fca).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<FilterCategory>), "Not an ActionResult<FilterCategory>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(FilterCategory), "Not a FilterCategory");
            fca.FilterCategoryId = ((FilterCategory)result.Value).FilterCategoryId;
            Assert.AreEqual(fca, (FilterCategory)result.Value, "FilterCategories not equals");
        }

        [TestMethod]
        public async Task PutFilterCategory_ReturnsNotFound_WithMoq() {
            // Arrange
            FilterCategory newFilterCategory = new FilterCategory {
                FilterCategoryId = 1,
                FilterCategoryName = "Type de canapé"
            };
            FilterCategory oldFilterCategory = new FilterCategory {
                FilterCategoryId = 5000,
                FilterCategoryName = "Type de transat"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newFilterCategory.FilterCategoryId).Result).Returns(newFilterCategory);
            // Act
            var actionResult = _controller.PutFilterCategory(oldFilterCategory.FilterCategoryId, oldFilterCategory).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutFilterCategory_ReturnsOk_WithMoq() {
            // Arrange
            FilterCategory newFilterCategory = new FilterCategory {
                FilterCategoryId = 1,
                FilterCategoryName = "Type de canapé"
            };
            FilterCategory oldFilterCategory = new FilterCategory {
                FilterCategoryId = 1,
                FilterCategoryName = "Type de transat"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newFilterCategory.FilterCategoryId).Result).Returns(newFilterCategory);
            // Act
            var actionResult = _controller.PutFilterCategory(oldFilterCategory.FilterCategoryId, oldFilterCategory).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutFilterCategory_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            FilterCategory newFilterCategory = new FilterCategory {
                FilterCategoryId = 1,
                FilterCategoryName = "Type de canapé"
            };
            FilterCategory oldFilterCategory = new FilterCategory {
                FilterCategoryId = 1,
                FilterCategoryName = "Type de transat"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newFilterCategory.FilterCategoryId).Result).Returns(newFilterCategory);
            // Act
            var actionResult = _controller.PutFilterCategory(id, oldFilterCategory).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }

        [TestMethod]
        public async Task DeleteFilterCategoryTest_ReturnsOk_WithMoq() {
            // Arrange
            FilterCategory fca = new FilterCategory {
                FilterCategoryId = 1,
                FilterCategoryName = "Type de canapé"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(fca.FilterCategoryId).Result).Returns(fca);
            // Act
            var actionResult = _controller.DeleteFilterCategory(fca.FilterCategoryId).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteFilterCategoryTest_ReturnsNotFound_WithMoq() {
            // Arrange
            FilterCategory fca = new FilterCategory {
                FilterCategoryId = 5000,
                FilterCategoryName = "Type de canapé"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(fca.FilterCategoryId).Result).Returns(fca);
            // Act
            var actionResult = _controller.DeleteFilterCategory(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }
    }
}