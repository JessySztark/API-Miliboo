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
    public class ColorsControllerTests {
        private Mock<IDataRepository<Color>> _mockRepository;
        private ColorsController _controller;
        private MilibooDBContext context;
        private IDataRepository<Color> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<Color>>();
            _controller = new ColorsController(_mockRepository.Object);
        }

        public ColorsControllerTests() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new ColorManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<Color> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetColors_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetColors();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<Color>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetColorById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetColorById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutColor_ReturnsNotFound_WithMoq() {
            // Arrange
            Color newColor = new Color {
                ColorId = 1,
                ColorHexaCode = "#EAD315",
                ColorName = "Jaune cumin"
            };
            Color oldColor = new Color {
                ColorId = 5000,
                ColorHexaCode = "#EAD315",
                ColorName = "Jaune cumin"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newColor.ColorId).Result).Returns(newColor);
            // Act
            var actionResult = _controller.PutColor(oldColor.ColorId, oldColor).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutColor_ReturnsOk_WithMoq() {
            // Arrange
            Color newColor = new Color {
                ColorId = 1,
                ColorHexaCode = "#EAD315",
                ColorName = "Jaune cumin"
            };
            Color oldColor = new Color {
                ColorId = 1,
                ColorHexaCode = "#EAD315",
                ColorName = "Jaune cumin"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newColor.ColorId).Result).Returns(newColor);
            // Act
            var actionResult = _controller.PutColor(oldColor.ColorId, oldColor).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutColor_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            Color newColor = new Color {
                ColorId = 1,
                ColorHexaCode = "#EAD315",
                ColorName = "Jaune cumin"
            };
            Color oldColor = new Color {
                ColorId = 5000,
                ColorHexaCode = "#EAD315",
                ColorName = "Jaune cumin"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newColor.ColorId).Result).Returns(newColor);
            // Act
            var actionResult = _controller.PutColor(id, oldColor).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }

        [TestMethod]
        public async Task DeleteColorTest_ReturnsOk_WithMoq() {
            // Arrange
            Color clr = new Color {
                ColorId = 1,
                ColorHexaCode = "#EAD315",
                ColorName = "Jaune cumin"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(clr.ColorId).Result).Returns(clr);
            // Act
            var actionResult = _controller.DeleteColor(clr.ColorId).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteColorTest_ReturnsNotFound_WithMoq() {
            // Arrange
            Color clr = new Color {
                ColorId = 5000,
                ColorHexaCode = "#EAD315",
                ColorName = "Jaune cumin"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(clr.ColorId).Result).Returns(clr);
            // Act
            var actionResult = _controller.DeleteColor(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }
    }
}