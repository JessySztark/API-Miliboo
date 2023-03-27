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
    public class CreditCardsControllertest {
        private Mock<IDataRepository<CreditCard>> _mockRepository;
        private CreditCardsController _controller;
        private MilibooDBContext context;
        private IDataRepository<CreditCard> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<CreditCard>>();
            _controller = new CreditCardsController(_mockRepository.Object);
        }

        public CreditCardsControllertest() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new CreditCardManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<CreditCard> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetCreditCards_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetCreditCards();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<CreditCard>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetCreditCardById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetCreditCardById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostCreditCard_ModelValidated_CreationOK_WithMoq() {
            CreditCard ccd = new CreditCard {
                CardID = 1,
                AccountID = 2,
                Name = "Dumont",
                FirstName = "Janna",
                ExpirationDate = new DateTime(2023,11,16),
                CardNumber = "4 532 831 659 287 560",
                Cryptogram = "416"
            };
            // Act
            var actionResult = _controller.PostCreditCard(ccd).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<CreditCard>), "Not an ActionResult<CreditCard>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(CreditCard), "Not a CreditCard");
            ccd.CardID = ((CreditCard)result.Value).CardID;
            Assert.AreEqual(ccd, (CreditCard)result.Value, "CreditCards not equals");
        }

        [TestMethod]
        public async Task PutCreditCard_ReturnsNotFound_WithMoq() {
            // Arrange
            CreditCard newCreditCard = new CreditCard {
                CardID = 1,
                AccountID = 2,
                Name = "Dumont",
                FirstName = "Janna",
                ExpirationDate = new DateTime(2023, 11, 16),
                CardNumber = "4 532 831 659 287 560",
                Cryptogram = "416"
            };
            CreditCard oldCreditCard = new CreditCard {
                CardID = 5000,
                AccountID = 2,
                Name = "Dumont",
                FirstName = "Jean",
                ExpirationDate = new DateTime(2023, 11, 16),
                CardNumber = "4 532 831 659 287 560",
                Cryptogram = "416"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newCreditCard.CardID).Result).Returns(newCreditCard);
            // Act
            var actionResult = _controller.PutCreditCard(oldCreditCard.CardID, oldCreditCard).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutCreditCard_ReturnsOk_WithMoq() {
            // Arrange
            CreditCard newCreditCard = new CreditCard {
                CardID = 1,
                AccountID = 2,
                Name = "Dumont",
                FirstName = "Janna",
                ExpirationDate = new DateTime(2023, 11, 16),
                CardNumber = "4 532 831 659 287 560",
                Cryptogram = "416"
            };
            CreditCard oldCreditCard = new CreditCard {
                CardID = 1,
                AccountID = 2,
                Name = "Dumont",
                FirstName = "Janna",
                ExpirationDate = new DateTime(2023, 11, 16),
                CardNumber = "4 532 831 659 287 560",
                Cryptogram = "416"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newCreditCard.CardID).Result).Returns(newCreditCard);
            // Act
            var actionResult = _controller.PutCreditCard(oldCreditCard.CardID, oldCreditCard).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutCreditCard_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            CreditCard newCreditCard = new CreditCard {
                CardID = 1,
                AccountID = 2,
                Name = "Dumont",
                FirstName = "Janna",
                ExpirationDate = new DateTime(2023, 11, 16),
                CardNumber = "4 532 831 659 287 560",
                Cryptogram = "416"
            };
            CreditCard oldCreditCard = new CreditCard {
                CardID = 1,
                AccountID = 2,
                Name = "Dumont",
                FirstName = "Janna",
                ExpirationDate = new DateTime(2023, 11, 16),
                CardNumber = "4 532 831 659 287 560",
                Cryptogram = "416"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newCreditCard.CardID).Result).Returns(newCreditCard);
            // Act
            var actionResult = _controller.PutCreditCard(id, oldCreditCard).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }

        [TestMethod]
        public async Task DeleteCreditCardTest_ReturnsOk_WithMoq() {
            // Arrange
            CreditCard ccd = new CreditCard {
                CardID = 1,
                AccountID = 2,
                Name = "Dumont",
                FirstName = "Janna",
                ExpirationDate = new DateTime(2023, 11, 16),
                CardNumber = "4 532 831 659 287 560",
                Cryptogram = "416"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(ccd.CardID).Result).Returns(ccd);
            // Act
            var actionResult = _controller.DeleteCreditCard(ccd.CardID).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteCreditCardTest_ReturnsNotFound_WithMoq() {
            // Arrange
            CreditCard ccd = new CreditCard {
                CardID = 5000,
                AccountID = 2,
                Name = "Dumont",
                FirstName = "Janna",
                ExpirationDate = new DateTime(2023, 11, 16),
                CardNumber = "4 532 831 659 287 560",
                Cryptogram = "416"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(ccd.CardID).Result).Returns(ccd);
            // Act
            var actionResult = _controller.DeleteCreditCard(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }
    }
}