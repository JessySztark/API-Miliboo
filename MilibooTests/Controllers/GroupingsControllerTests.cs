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
    public class GroupingsControllertest {
        private Mock<IDataRepository<Grouping>> _mockRepository;
        private GroupingsController _controller;
        private MilibooDBContext context;
        private IDataRepository<Grouping> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<Grouping>>();
            _controller = new GroupingsController(_mockRepository.Object);
        }

        public GroupingsControllertest() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new GroupingManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<Grouping> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetGroupings_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetGroupings();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<Grouping>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetGroupingById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetGroupingById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostGrouping_ModelValidated_CreationOK_WithMoq() {
            Grouping grp = new Grouping {
                GroupingId = 0,
                GroupingName = "Nos produits"
            };
            // Act
            var actionResult = _controller.PostGrouping(grp).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Grouping>), "Not an ActionResult<Grouping>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Grouping), "Not a Grouping");
            grp.GroupingId = ((Grouping)result.Value).GroupingId;
            Assert.AreEqual(grp, (Grouping)result.Value, "Groupings not equals");
        }

        [TestMethod]
        public async Task PutGrouping_ReturnsNotFound_WithMoq() {
            // Arrange
            Grouping newGrouping = new Grouping {
                GroupingId = 0,
                GroupingName = "Nos produits"
            };
            Grouping oldGrouping = new Grouping {
                GroupingId = 5000,
                GroupingName = "Nos produits phares"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newGrouping.GroupingId).Result).Returns(newGrouping);
            // Act
            var actionResult = _controller.PutGrouping(oldGrouping.GroupingId, oldGrouping).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutGrouping_ReturnsOk_WithMoq() {
            // Arrange
            Grouping newGrouping = new Grouping {
                GroupingId = 0,
                GroupingName = "Nos produits"
            };
            Grouping oldGrouping = new Grouping {
                GroupingId = 0,
                GroupingName = "Nos produits phares"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newGrouping.GroupingId).Result).Returns(newGrouping);
            // Act
            var actionResult = _controller.PutGrouping(oldGrouping.GroupingId, oldGrouping).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutGrouping_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            Grouping newGrouping = new Grouping {
                GroupingId = 0,
                GroupingName = "Nos produits"
            };
            Grouping oldGrouping = new Grouping {
                GroupingId = 0,
                GroupingName = "Nos produits phare"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newGrouping.GroupingId).Result).Returns(newGrouping);
            // Act
            var actionResult = _controller.PutGrouping(id, oldGrouping).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }

        [TestMethod]
        public async Task DeleteGroupingTest_ReturnsOk_WithMoq() {
            // Arrange
            Grouping grp = new Grouping {
                GroupingId = 0,
                GroupingName = "Nos produits"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(grp.GroupingId).Result).Returns(grp);
            // Act
            var actionResult = _controller.DeleteGrouping(grp.GroupingId).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteGroupingTest_ReturnsNotFound_WithMoq() {
            // Arrange
            Grouping grp = new Grouping {
                GroupingId = 5000,
                GroupingName = "Nos produits"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(grp.GroupingId).Result).Returns(grp);
            // Act
            var actionResult = _controller.DeleteGrouping(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }
    }
}