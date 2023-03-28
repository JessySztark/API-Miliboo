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
    public class PhotosControllerTests {
        private Mock<IDataRepository<Photo>> _mockRepository;
        private PhotosController _controller;
        private MilibooDBContext context;
        private IDataRepository<Photo> dataRepository;

        [TestInitialize]
        public void Initialize() {
            _mockRepository = new Mock<IDataRepository<Photo>>();
            _controller = new PhotosController(_mockRepository.Object);
        }

        public PhotosControllerTests() {
            var builder = new DbContextOptionsBuilder<MilibooDBContext>()
                  .UseNpgsql("Server = 51.83.36.122; port = 5432; Database = s234_miliboo; uid = s234; password = ejx2RG;");
            this.Context = new MilibooDBContext(builder.Options);
            this.dataRepository = new PhotoManager(context);
        }

        public MilibooDBContext Context {
            get {
                return context;
            }

            set {
                context = value;
            }
        }

        public IDataRepository<Photo> DataRepository {
            get {
                return dataRepository;
            }

            set {
                dataRepository = value;
            }
        }

        [TestMethod]
        public async Task GetPhotos_ReturnsNotNull_WithMoq() {
            var actionResult = _controller.GetPhotos();
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(Task<ActionResult<IEnumerable<Photo>>>), "Not a Task ActionResult IEnumerable");
        }

        [TestMethod]
        public async Task GetPhotoById_ReturnsNotFoundResult_WithMoq() {
            // Act
            var actionResult = _controller.GetPhotoById(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PostPhoto_ModelValidated_CreationOK_WithMoq() {
            Photo pht = new Photo {
                PhotoID = 1,
                ProductPhoto = new Product { ProductId = 1 },
                CommentPhoto= new Comment { },
                Link = "/picture/1.jpg"
            };
            // Act
            var actionResult = _controller.PostPhoto(pht).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Photo>), "Not an ActionResult<Photo>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Not a CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Photo), "Not an Photo");
            pht.PhotoID = ((Photo)result.Value).PhotoID;
            Assert.AreEqual(pht, (Photo)result.Value, "Photos not equals");
        }


        [TestMethod]
        public async Task DeletePhotoTest_ReturnsOk_WithMoq() {
            // Arrange
            Photo pht = new Photo {
                PhotoID = 1,
                ProductPhoto = new Product { ProductId = 1 },
                CommentPhoto = new Comment { },
                Link = "/picture/1.jpg"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(pht.PhotoID).Result).Returns(pht);
            // Act
            var actionResult = _controller.DeletePhoto(pht.PhotoID).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task DeletePhotoTest_ReturnsNotFound_WithMoq() {
            // Arrange
            Photo pht = new Photo {
                PhotoID = 5000,
                ProductPhoto = new Product { ProductId = 1 },
                CommentPhoto = new Comment { },
                Link = "/picture/1.jpg"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(pht.PhotoID).Result).Returns(pht);
            // Act
            var actionResult = _controller.DeletePhoto(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutPhoto_ReturnsNotFound_WithMoq() {
            // Arrange
            Photo newPhoto = new Photo {
                PhotoID = 1,
                ProductPhoto = new Product { ProductId = 1 },
                CommentPhoto = new Comment { },
                Link = "/picture/1.jpg"
            };
            Photo oldPhoto = new Photo {
                PhotoID = 5000,
                ProductPhoto = new Product { ProductId = 1 },
                CommentPhoto = new Comment { },
                Link = "/picture/5000.jpg"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newPhoto.PhotoID).Result).Returns(newPhoto);
            // Act
            var actionResult = _controller.PutPhoto(oldPhoto.PhotoID, oldPhoto).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult), "Not Found");
        }

        [TestMethod]
        public async Task PutPhoto_ReturnsOk_WithMoq() {
            // Arrange
            Photo newPhoto = new Photo {
                PhotoID = 1,
                ProductPhoto = new Product { ProductId = 1 },
                CommentPhoto = new Comment { },
                Link = "/picture/1.jpg"
            };
            Photo oldPhoto = new Photo {
                PhotoID = 1,
                ProductPhoto = new Product { ProductId = 1 },
                CommentPhoto = new Comment { },
                Link = "/picture/1_1.jpg"
            };

            _mockRepository.Setup(x => x.GetByIdAsync(newPhoto.PhotoID).Result).Returns(newPhoto);
            // Act
            var actionResult = _controller.PutPhoto(oldPhoto.PhotoID, oldPhoto).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Ok Result");
        }

        [TestMethod]
        public async Task PutPhoto_ReturnsBadRequest_WithMoq() {
            // Arrange
            int id = 5001;
            Photo newPhoto = new Photo {
                PhotoID = 1,
                ProductPhoto = new Product { ProductId = 1 },
                CommentPhoto = new Comment { },
                Link = "/picture/1.jpg"
            };
            Photo oldPhoto = new Photo {
                PhotoID = 5000,
                ProductPhoto = new Product { ProductId = 1 },
                CommentPhoto = new Comment { },
                Link = "/picture/5001.jpg"
            };
            _mockRepository.Setup(x => x.GetByIdAsync(newPhoto.PhotoID).Result).Returns(newPhoto);
            // Act
            var actionResult = _controller.PutPhoto(id, oldPhoto).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult), "Bad Request");
        }
    }
}