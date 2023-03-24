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
    public class AsAspectsControllerTests {
        private Mock<IDataRepository<AsAspect>> _mockRepository;
        private AsAspectsController _controller;
        private MilibooDBContext context;
        private IDataRepository<AsAspect> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<AsAspect>>();
            _controller = new AsAspectsController(_mockRepository.Object);
        }

        public AsAspectsControllerTests() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new AsAspectManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<AsAspect> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetAsAspects_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetAsAspect();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<AsAspect>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetAsAspectById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetAsAspect(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostAsAspect_ModelValidated_CreationOK_WithMoq() {
            AsAspect asa = new AsAspect {
                ProductTypeId= 1,
                TechnicalAspectId= 1,
            };
            // Act
            var actionResult = _controller.PostAsAspect(asa).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<AsAspect>), "Not an ActionResult<AsAspect>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(AsAspect), "Not an AsAspect");
            asa.ProductTypeId = ((AsAspect)result.Value).ProductTypeId;
            asa.TechnicalAspectId = ((AsAspect)result.Value).TechnicalAspectId;
            Assert.AreEqual(asa, (AsAspect)result.Value, "AsAspects not equals");
        }


        [TestMethod]
        public async Task DeleteAsAspectTest_ReturnsNoContent_WithMoq() {
            // Arrange
            AsAspect asa = new AsAspect {
                ProductTypeId = 1,
                TechnicalAspectId = 1,
            };
            _mockRepository.Setup(x => x.GetByIdAsync(asa.ProductTypeId).Result).Returns(asa);
            // Act
            var actionResult = _controller.DeleteAsAspect(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteAsAspectTest_ReturnsNotFound_WithMoq() {
            // Arrange
            AsAspect asa = new AsAspect {
                ProductTypeId = 5000,
                TechnicalAspectId = 1,
            };
            _mockRepository.Setup(x => x.GetByIdAsync(asa.ProductTypeId).Result).Returns(asa);
            // Act
            var actionResult = _controller.DeleteAsAspect(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutAsAspect_ReturnsNotFound_WithMoq() {
            // Arrange
            AsAspect newAsAspect = new AsAspect {
                ProductTypeId = 1,
                TechnicalAspectId = 1,
            };
            AsAspect oldAsAspect = new AsAspect {
                ProductTypeId = 5000,
                TechnicalAspectId = 1,
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newAsAspect.ProductTypeId).Result).Returns(newAsAspect);
            // Act
            var actionResult = _controller.PutAsAspect(oldAsAspect.ProductTypeId, oldAsAspect).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutAsAspect_ReturnsOk_WithMoq() {
            // Arrange
            AsAspect newAsAspect = new AsAspect {
                ProductTypeId = 1,
                TechnicalAspectId = 1,
            };
            AsAspect oldAsAspect = new AsAspect {
                ProductTypeId = 1,
                TechnicalAspectId = 1,
            };


            _mockRepository.Setup(x => x.GetByIdAsync(newAsAspect.ProductTypeId).Result).Returns(newAsAspect);
            // Act
            var actionResult = _controller.PutAsAspect(oldAsAspect.ProductTypeId, oldAsAspect).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutAsAspect_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            AsAspect newAsAspect = new AsAspect {
                ProductTypeId = 1,
                TechnicalAspectId = 1,
            };
            AsAspect oldAsAspect = new AsAspect {
                ProductTypeId = 5000,
                TechnicalAspectId = 1,
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newAsAspect.ProductTypeId).Result).Returns(newAsAspect);
            // Act
            var actionResult = _controller.PutAsAspect(id, oldAsAspect).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }
    }
}