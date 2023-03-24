using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class AccountControllertest {
        private Mock<IDataRepository<Account>> _mockRepository;
        private AccountsController _controller;
        private MilibooDBContext context;
        private IDataRepository<Account> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<Account>>();
            _controller = new AccountsController(_mockRepository.Object);
        }

        public AccountControllertest() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new AccountManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<Account> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetAccounts_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetAccounts();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<Account>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetAccountById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetAccountById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task GetAccountByEmail_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetAccountByEmail("pierre.papier@ciseaux.com").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostAccount_ModelValidated_CreationOK_WithMoq() {
            Account acc = new Account {
                FirstName = "Shellie",
                LastName = "Cobb",
                Password = "FMU91OOW2JP",
                Mail = "cobb-shellie@yahoo.net",
                PhoneNumber = "0176636567",
                Oath = false
            };
            // Act
            var actionResult = _controller.PostAccount(acc).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Account>), "Not an ActionResult<Account>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Account), "Not an Account");
            acc.AccountID = ((Account)result.Value).AccountID;
            Assert.AreEqual(acc, (Account)result.Value, "Accounts not equals");
        }

        [TestMethod]
        public async Task DeleteAccountTest_ReturnsNoContent_WithMoq() {
            // Arrange
            Account acc = new Account {
                AccountID = 1,
                FirstName = "Pierre",
                LastName = "Papier",
                PhoneNumber = "0607080910",
                Mail = "pierre.papier@ciseaux.com",
                Password = "FKFJGFNBVSDF",
                Oath = false
            };
            _mockRepository.Setup(x => x.GetByIdAsync(acc.AccountID).Result).Returns(acc);
            // Act
            var actionResult = _controller.DeleteAccount(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteAccountTest_ReturnsNotFound_WithMoq() {
            // Arrange
            Account acc = new Account {
                AccountID = 5000,
                FirstName = "Pierre",
                LastName = "Papier",
                PhoneNumber = "0607080910",
                Mail = "pierre.papier@ciseaux.com",
                Password = "FKFJGFNBVSDF",
                Oath = false
            };
            _mockRepository.Setup(x => x.GetByIdAsync(acc.AccountID).Result).Returns(acc);
            // Act
            var actionResult = _controller.DeleteAccount(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutAccount_ReturnsNotFound_WithMoq() {
            // Arrange
            Account newAccount = new Account {
                AccountID = 1,
                FirstName = "Pierre",
                LastName = "Papier",
                PhoneNumber = "0607080910",
                Mail = "pierre.papier@ciseaux.com",
                Password = "FKFJGFNBVSDF",
                Oath = false
            };
            Account oldAccount = new Account {
                AccountID = 5000,
                FirstName = "Shi",
                LastName = "Fu",
                PhoneNumber = "0708091011",
                Mail = "shi.fu@mi.com",
                Password = "DPMDLSGJFFGINB",
                Oath = false
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newAccount.AccountID).Result).Returns(newAccount);
            // Act
            var actionResult = _controller.PutAccount(oldAccount.AccountID, oldAccount).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutAccount_ReturnsOk_WithMoq() {
            // Arrange
            Account newAccount = new Account {
                AccountID = 1,
                FirstName = "Pierre",
                LastName = "Papier",
                PhoneNumber = "0607080910",
                Mail = "pierre.papier@ciseaux.com",
                Password = "FKFJGFNBVSDF",
                Oath = false
            };

            Account oldAccount = new Account {
                AccountID = 1,
                FirstName = "Shi",
                LastName = "Fu",
                PhoneNumber = "0708091011",
                Mail = "shi.fu@mi.com",
                Password = "DPMDLSGJFFGINB",
                Oath = false
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newAccount.AccountID).Result).Returns(newAccount);
            // Act
            var actionResult = _controller.PutAccount(oldAccount.AccountID, oldAccount).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutAccount_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            Account newAccount = new Account {
                AccountID = 1,
                FirstName = "Pierre",
                LastName = "Papier",
                PhoneNumber = "0607080910",
                Mail = "pierre.papier@ciseaux.com",
                Password = "FKFJGFNBVSDF",
                Oath = false
            };

            Account oldAccount = new Account {
                AccountID = 1,
                FirstName = "Shi",
                LastName = "Fu",
                PhoneNumber = "0708091011",
                Mail = "shi.fu@mi.com",
                Password = "DPMDLSGJFFGINB",
                Oath = false
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newAccount.AccountID).Result).Returns(newAccount);
            // Act
            var actionResult = _controller.PutAccount(id, oldAccount).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }
    }
}