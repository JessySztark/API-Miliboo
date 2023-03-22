using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Miliboo.Controllers;
using Miliboo.Models.EntityFramework;
using Miliboo.Models.Repository;
using MilibooAPI.Controllers;
using MilibooAPI.Models.DataManager;
using Moq;

namespace MilibooTests.Controller {
    [TestClass()]
    public class AccountControllertest{
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
        public void GetAccounts_ReturnsNotFoundResult_AvecMoq() {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(() => null);

            // Act
            var actionResult = _controller.GetAccounts().Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetAccountById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq() {
            // Act
            var actionResult = _controller.GetAccountById(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostAccount_ModelValidated_CreationOK_AvecMoq() {
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
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Account>), "Pas un ActionResult<Account>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Account), "Pas un Account");
            acc.AccountID = ((Account)result.Value).AccountID;
            Assert.AreEqual(acc, (Account)result.Value, "Accounts pas identiques");
        }

        [TestMethod]
        public async Task DeleteAccountTest_WithMoq() {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE du WS ou remove du DbSet.
            Account accAtester = new Account() {
                FirstName = "MACHIN",
                LastName = "Luc",
                PhoneNumber = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Password = "Toto1234!",
                Oath = false
            };

            this.Context.Account.Add(accAtester);

            var result = _controller.GetAccountByEmail(accAtester.Mail).Result;

            Assert.IsInstanceOfType(result.Value, typeof(Account));

            Account resultUser = result.Value;

            var result2 = _controller.DeleteAccount(resultUser.AccountID).Result;

            Account? utiliseur = this.Context.Account.Find(resultUser.AccountID);

            Assert.IsNull(utiliseur, "l'utilisateur n'a pas été supprimé");


        }
    }
}