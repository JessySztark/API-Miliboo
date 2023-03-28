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
    public class CommentControllertest {
        private Mock<IDataRepository<Comment>> _mockRepository;
        private CommentsController _controller;
        private MilibooDBContext context;
        private IDataRepository<Comment> dataRepository;
        private readonly MilibooDBContext _context;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<Comment>>();
            _controller = new CommentsController(_mockRepository.Object, _context);
        }

        public CommentControllertest() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new CommentManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<Comment> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetComments_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetComments();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<Comment>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetCommentById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetCommentById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task GetCommentByFK_ReturnsTypeObject_WithMoq() {
            // Act
            var actionResult = _controller.GetCommentByForeignKey(1);
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(object), "Object ok");
        }

        [TestMethod]
        public async Task PostComment_ModelValidated_CreationOK_WithMoq() {
            Comment com = new Comment {
                CommentID = 1,
                AccountID = 2,
                ProductTypeId = 1,
                Title = "Satisfait",
                Mark = 4,
                Description = "J'adore ce produit",
                Date = new DateTime(2022,11,16),
                Answer = null
            };
            // Act
            var actionResult = _controller.PostComment(com).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Comment>), "Not an ActionResult<Comment>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Comment), "Not a Comment");
            com.CommentID = ((Comment)result.Value).CommentID;
            Assert.AreEqual(com, (Comment)result.Value, "Comments not equals");
        }

        [TestMethod]
        public async Task PutComment_ReturnsNotFound_WithMoq() {
            // Arrange
            Comment newComment = new Comment {
                CommentID = 1,
                AccountID = 2,
                ProductTypeId = 1,
                Title = "Satisfait",
                Mark = 4,
                Description = "J'adore ce produit",
                Date = new DateTime(2022, 11, 16),
                Answer = null
            };
            Comment oldComment = new Comment {
                CommentID = 5000,
                AccountID = 2,
                ProductTypeId = 1,
                Title = "Satisfait",
                Mark = 4,
                Description = "J'adore ce produit",
                Date = new DateTime(2022, 11, 16),
                Answer = null
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newComment.CommentID).Result).Returns(newComment);
            // Act
            var actionResult = _controller.PutComment(oldComment.CommentID, oldComment).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutComment_ReturnsOk_WithMoq() {
            // Arrange
            Comment newComment = new Comment {
                CommentID = 1,
                AccountID = 2,
                ProductTypeId = 1,
                Title = "Satisfait",
                Mark = 4,
                Description = "J'adore ce produit",
                Date = new DateTime(2022, 11, 16),
                Answer = null
            };
            Comment oldComment = new Comment {
                CommentID = 1,
                AccountID = 2,
                ProductTypeId = 1,
                Title = "Satisfaite",
                Mark = 4,
                Description = "J'adore ce produit",
                Date = new DateTime(2022, 11, 16),
                Answer = null
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newComment.CommentID).Result).Returns(newComment);
            // Act
            var actionResult = _controller.PutComment(oldComment.CommentID, oldComment).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutComment_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            Comment newComment = new Comment {
                CommentID = 1,
                AccountID = 2,
                ProductTypeId = 1,
                Title = "Satisfait",
                Mark = 4,
                Description = "J'adore ce produit",
                Date = new DateTime(2022, 11, 16),
                Answer = null
            };
            Comment oldComment = new Comment {
                CommentID = 1,
                AccountID = 2,
                ProductTypeId = 1,
                Title = "Satisfait",
                Mark = 4,
                Description = "J'adore ce produit",
                Date = new DateTime(2022, 11, 16),
                Answer = null
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newComment.CommentID).Result).Returns(newComment);
            // Act
            var actionResult = _controller.PutComment(id, oldComment).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }

        [TestMethod]
        public async Task DeleteCommentTest_ReturnsOk_WithMoq() {
            // Arrange
            Comment com = new Comment {
                CommentID = 1,
                AccountID = 2,
                ProductTypeId = 1,
                Title = "Satisfait",
                Mark = 4,
                Description = "J'adore ce produit",
                Date = new DateTime(2022, 11, 16),
                Answer = null
            };
            _mockRepository.Setup(x => x.GetByIdAsync(com.CommentID).Result).Returns(com);
            // Act
            var actionResult = _controller.DeleteComment(com.CommentID).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeleteCommentTest_ReturnsNotFound_WithMoq() {
            // Arrange
            Comment com = new Comment {
                CommentID = 5000,
                AccountID = 2,
                ProductTypeId = 1,
                Title = "Satisfait",
                Mark = 4,
                Description = "J'adore ce produit",
                Date = new DateTime(2022, 11, 16),
                Answer = null
            };
            _mockRepository.Setup(x => x.GetByIdAsync(com.CommentID).Result).Returns(com);
            // Act
            var actionResult = _controller.DeleteComment(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }
    }
}