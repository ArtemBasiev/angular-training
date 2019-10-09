using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using bizapps_test.BLL.Services;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Logger.Abstract;
using bizapps_test.BLL.Services.Implementation;
using bizapps_test.DAL.Repositories;
using bizapps_test.Domain.Models;
using Moq;

namespace bizapps_test.BLL.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private static Mock<IBlogService> _blogService;
        private static Mock<ICustomLogger> _logger;
        private static Mock<IUnitOfWorkFactory> _unitOfWorkFactory;
        private static Mock<IRepositoryFactory> _repositoryFactory;
        private static Mock<IBlogUserRepository> _blogUserRepository;
        private static Mock<ICommentRepository> _commentRepository;
        private static Mock<IUnitOfWork> _unitOfWork;
        private static Mock<ILogFactory> _logFactory;
        private static UserService _userService;


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
            _blogUserRepository = new Mock<IBlogUserRepository>();
            _repositoryFactory = new Mock<IRepositoryFactory>();
            _commentRepository = new Mock<ICommentRepository>();
            _blogService = new Mock<IBlogService>();
            _userService = new UserService(_blogService.Object,  _unitOfWorkFactory.Object, _repositoryFactory.Object, _logFactory.Object);

        }

        #region CreatingTests
        private static void ArrangeForCreating(bool withBlog = false)
        {
            _blogUserRepository
                .Setup(r => r.CreateEntity(It.IsAny<BlogUser>()))
                .Returns(It.IsAny<int>());

            _repositoryFactory
                .Setup(f => f.CreateUserRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_blogUserRepository.Object);
            if (withBlog)
            {
                _blogService.Setup(s => s.CreateBlog(It.IsAny<BlogDto>(), It.IsAny<IBlogRepository>())).Returns(AnswerStatus.Successfull);
            }
        }

        private AnswerStatus CreateValidUser(bool withBlog = false)
        {
            var dto = new BlogUserDto
            {
                UserName = "test",
                UserPassword = "1111"
            };

            if (withBlog)
            {
                dto.UserBlog = new BlogDto();
            }

            return _userService.CreateUser(dto);
        }

        private AnswerStatus CreateInvalidUser()
        {
            return _userService.CreateUser(new BlogUserDto());
        }

        [TestMethod]
        public void CreateUser_WhenPassingValidUserDTO_ServiceReturnSuccessful()
        {
            ArrangeForCreating();

            var receivedResult = CreateValidUser();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void CreateUser_WhenPassingValidUserDTO_CreationOccured()
        {
            ArrangeForCreating();

            CreateValidUser();

            _blogUserRepository.Verify(r => r.CreateEntity(It.IsAny<BlogUser>()), Times.Once());
        }

        [TestMethod]
        public void CreateUser_WhenPassingInvalidUserDTO_ServiceReturnFailed()
        {
            ArrangeForCreating();

            var receivedResult = CreateInvalidUser();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }

        [TestMethod]
        public void CreateUser_WhenPassingBlogDTO_BlogCreatingOccured()
        {
            ArrangeForCreating(withBlog: true);

            CreateValidUser(withBlog: true);

            _blogService.Verify(s => s.CreateBlog(It.IsAny<BlogDto>(), It.IsAny<IBlogRepository>()), Times.Once());
        }

        [TestMethod]
        public void CreateUser_WhenPassingBlogDTO_ServiceReturnSuccessful()
        {
            ArrangeForCreating(withBlog: true);

            var receivedResult = CreateValidUser(withBlog: true);

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void CreateUser_WhenNOTPassingBlogDTO_BlogCreationNOTCalled()
        {
            ArrangeForCreating();

            CreateValidUser();

            _blogService.Verify(s => s.CreateBlog(It.IsAny<BlogDto>(), It.IsAny<IBlogRepository>()), Times.Never());
        }

        [TestMethod]
        public void CreateUser_WhenAnyException_ServiceReturnFailed()
        {
            _repositoryFactory
                .Setup(f => f.CreateUserRepository(It.IsAny<IUnitOfWork>()))
                .Throws(new Exception());

            var receivedResult = CreateValidUser();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }

        #endregion

        #region UpdatingTests
        private static void ArrangeForUpdating()
        {
            _blogUserRepository
                .Setup(r => r.UpdateEntity(It.IsAny<BlogUser>()))
                .Returns(It.IsAny<int>());

            _repositoryFactory
                .Setup(f => f.CreateUserRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_blogUserRepository.Object);
        }

        private AnswerStatus UpdateValidUser()
        {
            var dto = new BlogUserDto
            {
                Id = 1,
                UserName = "test",
                UserPassword = "1111"
            };

            return _userService.UpdateUser(dto);
        }

        private AnswerStatus UpdateInvalidUser()
        {
            return _userService.UpdateUser(new BlogUserDto());
        }

        [TestMethod]
        public void UpdateUser_WhenPassingValidUserDTO_ServiceReturnSuccessful()
        {
            ArrangeForUpdating();

            var receivedResult = UpdateValidUser();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void UpdateUser_WhenPassingValidUserDTO_UpdatingOccured()
        {
            ArrangeForUpdating();

            UpdateValidUser();

            _blogUserRepository.Verify(r => r.UpdateEntity(It.IsAny<BlogUser>()), Times.Once());
        }

        [TestMethod]
        public void UpdateUser_WhenPassingInvalidUserDTO_ServiceReturnFailed()
        {
            ArrangeForUpdating();

            var receivedResult = UpdateInvalidUser();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }

        [TestMethod]
        public void UpdateUser_WhenAnyException_ServiceReturnFailed()
        {
            _repositoryFactory
                .Setup(f => f.CreateUserRepository(It.IsAny<IUnitOfWork>()))
                .Throws(new Exception());

            var receivedResult = UpdateValidUser();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }

        #endregion

        #region DeletingTests
        private static void ArrangeForDeleting(bool withBlog = false, bool withComments = false)
        {
            _blogUserRepository
                .Setup(r => r.DeleteEntity(It.IsAny<BlogUser>()))
                .Returns(It.IsAny<int>());

            _repositoryFactory
                .Setup(f => f.CreateUserRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_blogUserRepository.Object);

            if (withBlog)
            {
                _blogService
                    .Setup(s => s.GetBlogByUserId(It.IsAny<int>()))
                    .Returns(new ServiceAnswer<BlogDto>(new BlogDto(), AnswerStatus.Successfull));

                _blogService
                    .Setup(s => s.DeleteBlog(It.IsAny<BlogDto>(), It.IsAny<IBlogRepository>()))
                    .Returns(AnswerStatus.Successfull);
            }
            else
            {
                _blogService
                    .Setup(s => s.GetBlogByUserId(It.IsAny<int>()))
                    .Returns(new ServiceAnswer<BlogDto>(null, AnswerStatus.Successfull));
            }

            if (withComments)
            {
                _commentRepository
                    .Setup(r => r.GetUserComments(It.IsAny<int>()))
                    .Returns(new List<Comment> { new Comment("test", DateTime.Now) });

                _commentRepository
                    .Setup(r => r.DeleteEntity(It.IsAny<Comment>()))
                    .Returns(It.IsAny<int>());
            }
            else
            {
                _commentRepository
                    .Setup(r => r.GetUserComments(It.IsAny<int>()))
                    .Returns(new List<Comment>());
            }
            _repositoryFactory
                .Setup(f => f.CreateCommentRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_commentRepository.Object);
        }

        private AnswerStatus DeleteValidUser()
        {
            var dto = new BlogUserDto
            {
                Id = 1
            };

            return _userService.DeleteUser(dto);
        }

        private AnswerStatus DeleteInvalidUser()
        {
            return _userService.DeleteUser(new BlogUserDto());
        }

        [TestMethod]
        public void DeleteUser_WhenPassingValidUserDTO_ServiceReturnSuccessful()
        {
            ArrangeForDeleting();

            var receivedResult = DeleteValidUser();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void DeleteUser_WhenPassingValidUserDTO_DeletingOccured()
        {
            ArrangeForDeleting();

            DeleteValidUser();

            _blogUserRepository.Verify(r => r.DeleteEntity(It.IsAny<BlogUser>()), Times.Once());
        }

        [TestMethod]
        public void DeleteUser_WhenPassingInvalidUserDTO_ServiceReturnFailed()
        {
            ArrangeForDeleting();

            var receivedResult = DeleteInvalidUser();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }

        [TestMethod]
        public void DeleteUser_WhenBlogExists_ServiceReturnSuccessful()
        {
            ArrangeForDeleting(withBlog: true);

            var receivedResult = DeleteValidUser();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void DeleteUser_WhenBlogExists_BlogDeletingCalled()
        {
            ArrangeForDeleting(withBlog: true);

            DeleteValidUser();

            _blogService.Verify(s => s.DeleteBlog(It.IsAny<BlogDto>(), It.IsAny<IBlogRepository>()), Times.Once());
        }

        [TestMethod]
        public void DeleteUser_WhenBlogNOTExists_BlogDeletingNOTCalled()
        {
            ArrangeForDeleting();

            DeleteValidUser();

            _blogService.Verify(s => s.DeleteBlog(It.IsAny<BlogDto>(), It.IsAny<IBlogRepository>()), Times.Never());
        }

        [TestMethod]
        public void DeleteUser_WhenCommentsExist_ServiceReturnSuccessful()
        {
            ArrangeForDeleting(withComments: true);

            var receivedResult = DeleteValidUser();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void DeleteUser_WhenCommentsExist_CommentsDeletingCalled()
        {
            ArrangeForDeleting(withComments: true);

            DeleteValidUser();

            _commentRepository.Verify(r => r.DeleteEntity(It.IsAny<Comment>()), Times.Once());
        }

        [TestMethod]
        public void DeleteUser_WhenCommentsNOTExist_CommentsDeletingNOTCalled()
        {
            ArrangeForDeleting();

            DeleteValidUser();

            _commentRepository.Verify(r => r.DeleteEntity(It.IsAny<Comment>()), Times.Never());
        }


        [TestMethod]
        public void DeleteUser_WhenAnyException_ServiceReturnFailed()
        {
            _repositoryFactory
                .Setup(f => f.CreateUserRepository(It.IsAny<IUnitOfWork>()))
                .Throws(new Exception());

            var receivedResult = DeleteValidUser();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }

        #endregion

        #region DataSelectionTests
        [TestMethod]
        public void GetUserById_ReturningUserDto_ReturnServiceAnswerWithSuccessfulStatus()
        {
            _blogUserRepository
                .Setup(r => r.GetEntityById(It.IsAny<int>()))
                .Returns(It.IsAny<BlogUser>());

            _repositoryFactory
                .Setup(f => f.CreateUserRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_blogUserRepository.Object);

            var receivedResult = _userService.GetUserById(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult.Status);
        }

        [TestMethod]
        public void GetUserById_UserRepositoryThowsException_ReturnServiceAnswerWithFailedStatus()
        {
            _blogUserRepository
                .Setup(r => r.GetEntityById(It.IsAny<int>()))
                .Throws(new Exception());

            _repositoryFactory
                .Setup(f => f.CreateUserRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_blogUserRepository.Object);

            var receivedResult = _userService.GetUserById(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Failed, receivedResult.Status);
        }

        [TestMethod]
        public void GetUserByName_ReturningUserDto_ReturnServiceAnswerWithSuccessfulStatus()
        {
            _blogUserRepository
                .Setup(r => r.GetBlogUserByName(It.IsAny<string>()))
                .Returns(It.IsAny<BlogUser>());

            _repositoryFactory
                .Setup(f => f.CreateUserRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_blogUserRepository.Object);

            var receivedResult = _userService.GetUserByName(It.IsAny<string>());

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult.Status);
        }

        [TestMethod]
        public void GetUserByName_UserRepositoryThowsException_ReturnServiceAnswerWithFailedStatus()
        {
            _blogUserRepository
                .Setup(r => r.GetBlogUserByName(It.IsAny<string>()))
                .Throws(new Exception());

            _repositoryFactory
                .Setup(f => f.CreateUserRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_blogUserRepository.Object);

            var receivedResult = _userService.GetUserByName(It.IsAny<string>());

            Assert.AreEqual(AnswerStatus.Failed, receivedResult.Status);
        }

        #endregion

    }
}
