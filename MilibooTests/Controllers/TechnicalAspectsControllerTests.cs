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
    public class TechnicalAspectsControllerTests {
        private Mock<IDataRepository<TechnicalAspect>> _mockRepository;
        private TechnicalAspectsController _controller;
        private MilibooDBContext context;
        private IDataRepository<TechnicalAspect> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<TechnicalAspect>>();
            _controller = new TechnicalAspectsController(_mockRepository.Object);
        }

        public TechnicalAspectsControllerTests() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new TechnicalAspectManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<TechnicalAspect> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetTechnicalAspects_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetTechnicalAspects();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<TechnicalAspect>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetTechnicalAspectById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetTechnicalAspectById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostTechnicalAspect_ModelValidated_CreationOK_WithMoq() {
            TechnicalAspect rgp = new TechnicalAspect {
                TechnicalAspectId = 1,
                TechnicalAspectName = "Dimensions totales"
            };
            // Act
            var actionResult = _controller.PostTechnicalAspect(rgp).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<TechnicalAspect>), "Not an ActionResult<TechnicalAspect>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(TechnicalAspect), "Not an TechnicalAspect");
            rgp.TechnicalAspectId = ((TechnicalAspect)result.Value).TechnicalAspectId;
            rgp.TechnicalAspectId = ((TechnicalAspect)result.Value).TechnicalAspectId;
            Assert.AreEqual(rgp, (TechnicalAspect)result.Value, "TechnicalAspects not equals");
        }


        [TestMethod]
        public async Task DeleteTechnicalAspectTest_ReturnsOk_WithMoq() {
            // Arrange
            TechnicalAspect rgp = new TechnicalAspect {
                TechnicalAspectId = 1,
                TechnicalAspectName = "Dimensions totales"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(rgp.TechnicalAspectId).Result).Returns(rgp);
            // Act
            var actionResult = _controller.DeleteTechnicalAspect(rgp.TechnicalAspectId).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteTechnicalAspectTest_ReturnsNotFound_WithMoq() {
            // Arrange
            TechnicalAspect rgp = new TechnicalAspect {
                TechnicalAspectId = 5000,
                TechnicalAspectName = "Dimensions totales"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(rgp.TechnicalAspectId).Result).Returns(rgp);
            // Act
            var actionResult = _controller.DeleteTechnicalAspect(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutTechnicalAspect_ReturnsNotFound_WithMoq() {
            // Arrange
            TechnicalAspect newTechnicalAspect = new TechnicalAspect {
                TechnicalAspectId = 1,
                TechnicalAspectName = "Dimensions totales"
            };
            TechnicalAspect oldTechnicalAspect = new TechnicalAspect {
                TechnicalAspectId = 5000,
                TechnicalAspectName = "Dimensions totales"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newTechnicalAspect.TechnicalAspectId).Result).Returns(newTechnicalAspect);
            // Act
            var actionResult = _controller.PutTechnicalAspect(oldTechnicalAspect.TechnicalAspectId, oldTechnicalAspect).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutTechnicalAspect_ReturnsOk_WithMoq() {
            // Arrange
            TechnicalAspect newTechnicalAspect = new TechnicalAspect {
                TechnicalAspectId = 1,
                TechnicalAspectName = "Dimensions totales"
            };
            TechnicalAspect oldTechnicalAspect = new TechnicalAspect {
                TechnicalAspectId = 1,
                TechnicalAspectName = "Dimensions totales"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newTechnicalAspect.TechnicalAspectId).Result).Returns(newTechnicalAspect);
            // Act
            var actionResult = _controller.PutTechnicalAspect(oldTechnicalAspect.TechnicalAspectId, oldTechnicalAspect).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutTechnicalAspect_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            TechnicalAspect newTechnicalAspect = new TechnicalAspect {
                TechnicalAspectId = 1,
                TechnicalAspectName = "Dimensions totales"
            };
            TechnicalAspect oldTechnicalAspect = new TechnicalAspect {
                TechnicalAspectId = 5000,
                TechnicalAspectName = "Dimensions totales"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newTechnicalAspect.TechnicalAspectId).Result).Returns(newTechnicalAspect);
            // Act
            var actionResult = _controller.PutTechnicalAspect(id, oldTechnicalAspect).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }
    }
}