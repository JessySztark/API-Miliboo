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
    public class RegroupsControllerTests {
        private Mock<IDataRepository<Regroup>> _mockRepository;
        private RegroupsController _controller;
        private MilibooDBContext context;
        private IDataRepository<Regroup> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<Regroup>>();
            _controller = new RegroupsController(_mockRepository.Object);
        }

        public RegroupsControllerTests() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new RegroupManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<Regroup> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetRegroups_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetRegroups();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<Regroup>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetRegroupById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetRegroupById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostRegroup_ModelValidated_CreationOK_WithMoq() {
            Regroup rgp = new Regroup {
                RegroupId = 1,
                ProductsNavigation = new Product { ProductId = 8 },
                GroupingsNavigation = new Grouping { GroupingId = 2 }
            };
            // Act
            var actionResult = _controller.PostRegroup(rgp).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Regroup>), "Not an ActionResult<Regroup>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Regroup), "Not an Regroup");
            rgp.RegroupId = ((Regroup)result.Value).RegroupId;
            rgp.RegroupId = ((Regroup)result.Value).RegroupId;
            Assert.AreEqual(rgp, (Regroup)result.Value, "Regroups not equals");
        }


        [TestMethod]
        public async Task DeleteRegroupTest_ReturnsOk_WithMoq() {
            // Arrange
            Regroup rgp = new Regroup {
                RegroupId = 1,
                ProductsNavigation = new Product { ProductId = 8 },
                GroupingsNavigation = new Grouping { GroupingId = 2 }
            };
            _mockRepository.Setup(x => x.GetByIdAsync(rgp.RegroupId).Result).Returns(rgp);
            // Act
            var actionResult = _controller.DeleteRegroup(rgp.RegroupId).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteRegroupTest_ReturnsNotFound_WithMoq() {
            // Arrange
            Regroup rgp = new Regroup {
                RegroupId = 5000,
                ProductsNavigation = new Product { ProductId = 8 },
                GroupingsNavigation = new Grouping { GroupingId = 2 }
            };
            _mockRepository.Setup(x => x.GetByIdAsync(rgp.RegroupId).Result).Returns(rgp);
            // Act
            var actionResult = _controller.DeleteRegroup(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutRegroup_ReturnsNotFound_WithMoq() {
            // Arrange
            Regroup newRegroup = new Regroup {
                RegroupId = 1,
                ProductsNavigation = new Product { ProductId = 8 },
                GroupingsNavigation = new Grouping { GroupingId = 2 }
            };
            Regroup oldRegroup = new Regroup {
                RegroupId = 5000,
                ProductsNavigation = new Product { ProductId = 8 },
                GroupingsNavigation = new Grouping { GroupingId = 2 }
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newRegroup.RegroupId).Result).Returns(newRegroup);
            // Act
            var actionResult = _controller.PutRegroup(oldRegroup.RegroupId, oldRegroup).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutRegroup_ReturnsOk_WithMoq() {
            // Arrange
            Regroup newRegroup = new Regroup {
                RegroupId = 1,
                ProductsNavigation = new Product { ProductId = 8 },
                GroupingsNavigation = new Grouping { GroupingId = 2 }
            };
            Regroup oldRegroup = new Regroup {
                RegroupId = 1,
                ProductsNavigation = new Product { ProductId = 8 },
                GroupingsNavigation = new Grouping { GroupingId = 2 }
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newRegroup.RegroupId).Result).Returns(newRegroup);
            // Act
            var actionResult = _controller.PutRegroup(oldRegroup.RegroupId, oldRegroup).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutRegroup_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            Regroup newRegroup = new Regroup {
                RegroupId = 1,
                ProductsNavigation = new Product { ProductId = 8 },
                GroupingsNavigation = new Grouping { GroupingId = 2 }
            };
            Regroup oldRegroup = new Regroup {
                RegroupId = 5000,
                ProductsNavigation = new Product { ProductId = 8 },
                GroupingsNavigation = new Grouping { GroupingId = 2 }
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newRegroup.RegroupId).Result).Returns(newRegroup);
            // Act
            var actionResult = _controller.PutRegroup(id, oldRegroup).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }
    }
}