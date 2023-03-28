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
    public class OwningsControllerTests {
        private Mock<IDataRepository<Owning>> _mockRepository;
        private OwningsController _controller;
        private MilibooDBContext context;
        private IDataRepository<Owning> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<Owning>>();
            _controller = new OwningsController(_mockRepository.Object);
        }

        public OwningsControllerTests() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new OwningManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<Owning> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetOwnings_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetOwning();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<Owning>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetOwningById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetOwningById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostOwning_ModelValidated_CreationOK_WithMoq() {
            Owning own = new Owning {
                AccountID = 2,
                AddressID = 2
            };
            // Act
            var actionResult = _controller.PostOwning(own).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Owning>), "Not an ActionResult<Owning>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Owning), "Not an Owning");
            own.AccountID = ((Owning)result.Value).AccountID;
            own.AddressID = ((Owning)result.Value).AddressID;
            Assert.AreEqual(own, (Owning)result.Value, "Ownings not equals");
        }


        [TestMethod]
        public async Task DeleteOwningTest_ReturnsOk_WithMoq() {
            // Arrange
            Owning own = new Owning {
                AccountID = 2,
                AddressID = 2
            };
            _mockRepository.Setup(x => x.GetByIdAsync(own.AccountID).Result).Returns(own);
            // Act
            var actionResult = _controller.DeleteOwning(own.AccountID).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteOwningTest_ReturnsNotFound_WithMoq() {
            // Arrange
            Owning own = new Owning {
                AccountID = 5000,
                AddressID = 2
            };
            _mockRepository.Setup(x => x.GetByIdAsync(own.AccountID).Result).Returns(own);
            // Act
            var actionResult = _controller.DeleteOwning(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutOwning_ReturnsNotFound_WithMoq() {
            // Arrange
            Owning newOwning = new Owning {
                AccountID = 2,
                AddressID = 2
            };
            Owning oldOwning = new Owning {
                AccountID = 5000,
                AddressID = 5000
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newOwning.AccountID).Result).Returns(newOwning);
            // Act
            var actionResult = _controller.PutOwning(oldOwning.AccountID, oldOwning).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutOwning_ReturnsOk_WithMoq() {
            // Arrange
            Owning newOwning = new Owning {
                AccountID = 2,
                AddressID = 2
            };
            Owning oldOwning = new Owning {
                AccountID = 2,
                AddressID = 2
            };


            _mockRepository.Setup(x => x.GetByIdAsync(newOwning.AccountID).Result).Returns(newOwning);
            // Act
            var actionResult = _controller.PutOwning(oldOwning.AccountID, oldOwning).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutOwning_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            Owning newOwning = new Owning {
                AccountID = 2,
                AddressID = 2
            };
            Owning oldOwning = new Owning {
                AccountID = 5000,
                AddressID = 2
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newOwning.AccountID).Result).Returns(newOwning);
            // Act
            var actionResult = _controller.PutOwning(id, oldOwning).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }
    }
}