using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Logger.Abstract;
using bizapps_test.BLL.Services.Implementation;
using bizapps_test.DAL.Repositories;
using bizapps_test.Domain.Models;
using Moq;

namespace bizapps_test.BLL.Tests
{

    [TestClass]
    public class CommentServiceTests
    {
        private static Mock<ICustomLogger> _logger;
        private static Mock<IUnitOfWorkFactory> _unitOfWorkFactory;
        private static Mock<IRepositoryFactory> _repositoryFactory;
        private static Mock<IPostRepository> _postRepository;
        private static Mock<IBlogUserRepository> _userRepository;
        private static Mock<ICommentRepository> _commentRepository;
        private static Mock<IUnitOfWork> _unitOfWork;
        private static Mock<ILogFactory> _logFactory;
        private static CommentService _commentService;


        [ClassInitialize]
        public static void InitializeImmutableDependencies(TestContext context)
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
            _unitOfWorkFactory.Setup(f => f.Create()).Returns(_unitOfWork.Object);
            _logger = new Mock<ICustomLogger>();
            _logFactory = new Mock<ILogFactory>();
            _logFactory.Setup(f => f.CreateLogger(It.IsAny<Type>())).Returns(_logger.Object);
        }


        [TestInitialize]
        public void InitializeDependencies()
        {
            _commentRepository = new Mock<ICommentRepository>();
            _repositoryFactory = new Mock<IRepositoryFactory>();
            _postRepository = new Mock<IPostRepository>();           
            _userRepository = new Mock<IBlogUserRepository>();          
            _commentService = new CommentService(_unitOfWorkFactory.Object, _repositoryFactory.Object, _logFactory.Object);
        }

        #region CreatingTests
        private static void ArrangeForCreating()
        {
            _commentRepository
                .Setup(r => r.CreateEntity(It.IsAny<Comment>()))
                .Returns(It.IsAny<int>());

            _repositoryFactory
                .Setup(f => f.CreateCommentRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_commentRepository.Object);

            _postRepository
                .Setup(r => r.GetEntityById(It.IsAny<int>()));

            _repositoryFactory
                .Setup(f => f.CreatePostRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_postRepository.Object);

            _userRepository
                .Setup(r => r.GetEntityById(It.IsAny<int>()));

            _repositoryFactory
                .Setup(f => f.CreateUserRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_userRepository.Object);
        }

        private static AnswerStatus CreateValidComment()
        {
            var dto = new CommentDto
            {
                CommentText = "test",
                CreationDate = DateTime.Now,
                CreatedBy = new BlogUserDto { Id = 1 },
                RelatedTo = new PostDto { Id = 1 }
            };

            return _commentService.CreateComment(dto);
        }

        private static AnswerStatus CreateInvalidComment()
        {
            return _commentService.CreateComment(new CommentDto());
        }


        [TestMethod]
        public void CreateComment_WhenPassingValidCommentDTO_ServiceReturnSuccessful()
        {
            ArrangeForCreating();

            var receivedResult = CreateValidComment();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void CreateComment_WhenPassingValidCommentDTO_CreatingOccured()
        {
            ArrangeForCreating();

            CreateValidComment();

            _commentRepository.Verify(r => r.CreateEntity(It.IsAny<Comment>()), Times.Once());
        }

        [TestMethod]
        public void CreateComment_WhenPassingInvalidCommentDTO_ServiceReturnFailed()
        {
            ArrangeForCreating();

            var receivedResult = CreateInvalidComment();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }


        [TestMethod]
        public void CreateComment_WhenAnyException_ServiceReturnFailed()
        {
            _repositoryFactory
                .Setup(f => f.CreatePostRepository(It.IsAny<IUnitOfWork>()))
                .Throws(new Exception());

            var receivedResult = CreateValidComment();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }
        #endregion

        #region UpdatingTests
        private static void ArrangeForUpdating()
        {
            _commentRepository
                .Setup(r => r.UpdateEntity(It.IsAny<Comment>()))
                .Returns(It.IsAny<int>());

            _repositoryFactory
                .Setup(f => f.CreateCommentRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_commentRepository.Object);
        }

        private static AnswerStatus UpdateValidComment()
        {
            var dto = new CommentDto
            {
                Id = 1,
                CommentText = "test",
                CreationDate = DateTime.Now
            };

            return _commentService.UpdateComment(dto);
        }

        private static AnswerStatus UpdateInvalidComment()
        {
            return _commentService.UpdateComment(new CommentDto());
        }



        [TestMethod]
        public void UpdateComment_WhenPassingValidCommentDTO_ServiceReturnSuccessful()
        {
            ArrangeForUpdating();

            var receivedResult = UpdateValidComment();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void UpdateComment_WhenPassingValidCommentDTO_UpdatingOccured()
        {
            ArrangeForUpdating();

            UpdateValidComment();

            _commentRepository.Verify(r => r.UpdateEntity(It.IsAny<Comment>()), Times.Once());
        }

        [TestMethod]
        public void UpdateComment_WhenPassingInvalidCommentDTO_ServiceReturnFailed()
        {
            ArrangeForUpdating();

            var receivedResult = UpdateInvalidComment();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }

        [TestMethod]
        public void UpdateComment_WhenAnyException_ServiceReturnFailed()
        {
            _repositoryFactory
                .Setup(f => f.CreateCommentRepository(It.IsAny<IUnitOfWork>()))
                .Throws(new Exception());

            var receivedResult = UpdateValidComment();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }

        #endregion

        #region DeletingTests
        private static void ArrangeForDeleting()
        {
            _commentRepository
                .Setup(r => r.DeleteEntity(It.IsAny<Comment>()))
                .Returns(It.IsAny<int>());

            _commentRepository
                .Setup(r => r.GetEntityById(It.IsAny<int>()))
                .Returns(It.IsAny<Comment>());

            _repositoryFactory
                .Setup(f => f.CreateCommentRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_commentRepository.Object);
        }

        private static AnswerStatus DeleteValidComment()
        {
            var dto = new CommentDto
            {
                Id = 1
            };

            return _commentService.DeleteComment(dto);
        }

        private static AnswerStatus DeleteInvalidComment()
        {
            return _commentService.DeleteComment(new CommentDto());
        }


        [TestMethod]
        public void DeleteComment_WhenPassingValidCommentDTO_ServiceReturnSuccessful()
        {
            ArrangeForDeleting();

            var receivedResult = DeleteValidComment();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void DeleteComment_WhenPassingValidCommentDTO_DeletingOccured()
        {
            ArrangeForDeleting();

            DeleteValidComment();

            _commentRepository.Verify(r => r.DeleteEntity(It.IsAny<Comment>()), Times.Once());
        }

        [TestMethod]
        public void DeleteComment_WhenPassingInvalidCommentDTO_ServiceReturnFailed()
        {
            ArrangeForDeleting();

            var receivedResult = DeleteInvalidComment();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }


        [TestMethod]
        public void DeleteComment_WhenAnyException_ServiceReturnFailed()
        {
            _repositoryFactory
                .Setup(f => f.CreateCommentRepository(It.IsAny<IUnitOfWork>()))
                .Throws(new Exception());

            var receivedResult = DeleteValidComment();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }
        #endregion

        #region DataSelectionTests
        [TestMethod]
        public void GetCommentById_ReturningCommentDto_ReturnServiceAnswerWithSuccessfulStatus()
        {
            _commentRepository
                .Setup(r => r.GetEntityById(It.IsAny<int>()))
                .Returns(It.IsAny<Comment>());

            _repositoryFactory
                .Setup(f => f.CreateCommentRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_commentRepository.Object);

            var receivedResult = _commentService.GetCommentById(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult.Status);
        }

        [TestMethod]
        public void GetCommentById_CommentRepositoryThrowsException_ReturnServiceAnswerWithFailedStatus()
        {
            _commentRepository
                .Setup(r => r.GetEntityById(It.IsAny<int>()))
                .Throws(new Exception());

            _repositoryFactory
                .Setup(f => f.CreateCommentRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_commentRepository.Object);

            var receivedResult = _commentService.GetCommentById(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Failed, receivedResult.Status);
        }

        [TestMethod]
        public void GetPostComments_ReturningCommentDtoList_ReturnServiceAnswerWithSuccessfulStatus()
        {
            _commentRepository
                .Setup(r => r.GetPostComments(It.IsAny<int>()))
                .Returns(It.IsAny<List<Comment>>());

            _repositoryFactory
                .Setup(f => f.CreateCommentRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_commentRepository.Object);

            var receivedResult = _commentService.GetPostComments(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult.Status);
        }

        [TestMethod]
        public void GetPostComments_CommentRepositoryThrowsException_ReturnServiceAnswerWithFailedStatus()
        {
            _commentRepository
                .Setup(r => r.GetPostComments(It.IsAny<int>()))
                .Throws(new Exception());

            _repositoryFactory
                .Setup(f => f.CreateCommentRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_commentRepository.Object);

            var receivedResult = _commentService.GetPostComments(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Failed, receivedResult.Status);
        }
        #endregion

    }
}
