using System;
using System.Collections.Generic;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Logger.Abstract;
using bizapps_test.BLL.Services;
using bizapps_test.BLL.Services.Implementation;
using bizapps_test.DAL.Repositories;
using bizapps_test.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace bizapps_test.BLL.Tests
{
    [TestClass]
    public class BlogServiceTests
    {
        private static Mock<ICustomLogger> _logger;
        private static Mock<ICategoryService> _categoryService;
        private static Mock<IPostService> _postService;
        private static Mock<IUnitOfWorkFactory> _unitOfWorkFactory;
        private static Mock<IRepositoryFactory> _repositoryFactory;
        private static Mock<IBlogRepository> _blogRepository;
        private static Mock<IUnitOfWork> _unitOfWork;
        private static Mock<ILogFactory> _logFactory;
        private static BlogService _blogService;


        [ClassInitialize]
        public static void InitializeImmutableDependencies(TestContext context)
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
            _unitOfWorkFactory.Setup(f => f.Create()).Returns(_unitOfWork.Object);
            _logger = new Mock<ICustomLogger>();
            _logFactory = new Mock<ILogFactory>();
            _logFactory.Setup(f => f.CreateLogger(It.IsAny<Type>())).Returns(_logger.Object);
            _postService = new Mock<IPostService>();
        }


        [TestInitialize]
        public void InitializeDependencies()
        {
            _categoryService = new Mock<ICategoryService>();
            _postService = new Mock<IPostService>();
            _repositoryFactory = new Mock<IRepositoryFactory>();
            _blogRepository = new Mock<IBlogRepository>();
            _blogService = new BlogService(_categoryService.Object, _postService.Object,
            _unitOfWorkFactory.Object, _repositoryFactory.Object, _logFactory.Object);
        }


        #region CreatingTests

               
        private static void ArrangeForCreating()
        {
            _repositoryFactory
                .Setup(f => f.CreateBlogRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_blogRepository.Object);

            _blogRepository
                .Setup(r => r.CreateEntity(It.IsAny<Blog>()))
                .Returns(It.IsAny<int>());
        }

        private static AnswerStatus CreateValidBlog(bool includeCategories = false)
        {
            var dto = new BlogDto
            {
                BlogTitle = "test",
                CreationDate = DateTime.Now,
                CreatedBy = new BlogUserDto
                {
                    UserName = "test",
                    UserPassword = "1111"
                }
            };
            
            if (includeCategories)
            {
                dto.BlogCategories = new List<CategoryDto> {new CategoryDto()};
            }

            return _blogService.CreateBlog(dto);
        }

        private static AnswerStatus CreateInvalidBlog()
        {
            var createResult = _blogService.CreateBlog(new BlogDto());
            return createResult;
        }


        [TestMethod]
        public void CreateBlog_WhenPassingValidBlogDTO_ServiceReturnOK()
        {
            ArrangeForCreating();
            var createResult = CreateValidBlog();
            Assert.AreEqual(AnswerStatus.Successfull, createResult);
        }

        [TestMethod]
        public void CreateBlog_WhenPassingValidBlogDTO_BlogIsPersisted()
        {
            ArrangeForCreating();
            CreateValidBlog();
            _blogRepository.Verify(r => r.CreateEntity(It.IsAny<Blog>()), Times.Once());
        }

        [TestMethod]
        public void CreateBlog_WhenPassingInvalidBlogDTO_ServiceFailed()
        {
            ArrangeForCreating();
            var createResult = CreateInvalidBlog();
            Assert.AreEqual(AnswerStatus.Failed, createResult);
        }

        [TestMethod]
        public void CreateBlog_WhenNOTPassingBlogCategories__CategoryServiceNOTCalled()
        {
            ArrangeForCreating();
            CreateValidBlog();

            _categoryService
                .Verify(s => s.CreateCategory(It.IsAny<CategoryDto>(), It.IsAny<ICategoryRepository>()), Times.Never());
        }

        [TestMethod]
        public void CreateBlog_WhenPassingBlogCategories_ServiceReturnOK()
        {
           ArrangeForCreating();
            _categoryService
                .Setup(s => s.CreateCategory(It.IsAny<CategoryDto>(), It.IsAny<ICategoryRepository>()))
                .Returns(AnswerStatus.Successfull);

            var receivedResult = CreateValidBlog(includeCategories: true);

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }


        [TestMethod]
        public void CreateBlog_WhenPassingBlogCategories__CategoryServiceCalled()
        {
           ArrangeForCreating();
            _categoryService
                .Setup(s => s.CreateCategory(It.IsAny<CategoryDto>(), It.IsAny<ICategoryRepository>()))
                .Returns(AnswerStatus.Successfull);

            CreateValidBlog(includeCategories: true);

            _categoryService
                .Verify(s => s.CreateCategory(It.IsAny<CategoryDto>(), It.IsAny<ICategoryRepository>()), Times.Once());
        }

        [TestMethod]
        public void CreateBlog_WhenAnyException_ServiceFailed()
        {
            _repositoryFactory
                .Setup(f => f.CreateBlogRepository(It.IsAny<IUnitOfWork>()))
                .Throws(new Exception());

            var receivedResult = CreateValidBlog();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }
        #endregion

        #region UpdatingTests
        
        private static void ArrangeForUpdating()
        {
            _blogRepository
                .Setup(r => r.UpdateEntity(It.IsAny<Blog>())).Returns(It.IsAny<int>());

            _repositoryFactory
                .Setup(f => f.CreateBlogRepository(It.IsAny<IUnitOfWork>())).Returns(_blogRepository.Object);
        }

        private static AnswerStatus UpdateValidBlog()
        {
            var dto = new BlogDto
            {
                BlogTitle = "test",
                CreationDate = DateTime.Now,
            };

            return _blogService.UpdateBlog(dto);
        }

        private static AnswerStatus UpdateInvalidBlog()
        {
            var updateResult = _blogService.UpdateBlog(new BlogDto());
            return updateResult;
        }


        [TestMethod]
        public void UpdateBlog_WhenPassingValidBlogDTO_ServiceReturnSuccessful()
        {
            ArrangeForUpdating();

            var receivedResult = UpdateValidBlog();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void UpdateBlog_WhenPassingValidBlogDTO_UpdatingOccured()
        {
            ArrangeForUpdating();

            UpdateValidBlog();

            _blogRepository
                .Verify(r => r.UpdateEntity(It.IsAny<Blog>()), Times.Once());
        }

        [TestMethod]
        public void UpdateBlog_WhenPassingInvalidBlogDTO_ServiceFailed()
        {
            ArrangeForUpdating();

            var receivedResult = UpdateInvalidBlog();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }


        [TestMethod]
        public void UpdateBlog_WhenAnyException_ServiceFailed()
        {
            _repositoryFactory
                .Setup(f => f.CreateBlogRepository(It.IsAny<IUnitOfWork>()))
                .Throws(new Exception());

            var receivedResult = UpdateValidBlog();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }
        #endregion

        #region DeletingTests


        private static void ArrangeForDeleting(bool withCategories = false, bool withPosts = false)
        {
            _blogRepository
                .Setup(r => r.DeleteEntity(It.IsAny<Blog>()))
                .Returns(It.IsAny<int>());

            _blogRepository
                .Setup(r => r.GetEntityById(It.IsAny<int>()))
                .Returns(It.IsAny<Blog>());

            _repositoryFactory
                .Setup(f => f.CreateBlogRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_blogRepository.Object);

            if (withCategories)
            {
                _categoryService
                    .Setup(s => s.GetBlogCategories(It.IsAny<int>()))
                    .Returns(new ServiceAnswer<IEnumerable<CategoryDto>>(new List<CategoryDto> { new CategoryDto() }, AnswerStatus.Successfull));

                _categoryService
                    .Setup(s => s.DeleteCategory(It.IsAny<CategoryDto>(), It.IsAny<ICategoryRepository>()))
                    .Returns(AnswerStatus.Successfull);
            }
            else
            {
                _categoryService
                    .Setup(s => s.GetBlogCategories(It.IsAny<int>()))
                    .Returns(new ServiceAnswer<IEnumerable<CategoryDto>>(new List<CategoryDto>(), AnswerStatus.Successfull));
            }

            if (withPosts)
            {
                _postService
                    .Setup(s => s.GetBlogPosts(It.IsAny<int>()))
                    .Returns(new ServiceAnswer<IEnumerable<PostDto>>(new List<PostDto> { new PostDto() }, AnswerStatus.Successfull));

                _postService
                    .Setup(s => s.DeletePost(It.IsAny<PostDto>(), It.IsAny<IPostRepository>()))
                    .Returns(AnswerStatus.Successfull);
            }
            else
            {
                _postService
                    .Setup(s => s.GetBlogPosts(It.IsAny<int>()))
                    .Returns(new ServiceAnswer<IEnumerable<PostDto>>(new List<PostDto>(), AnswerStatus.Successfull));
            }
        }

        private static AnswerStatus DeleteValidBlog()
        {
            var dto = new BlogDto
            {
                Id = 1
            };

            return _blogService.DeleteBlog(dto);
        }

        private static AnswerStatus DeleteInvalidBlog()
        {
            return _blogService.DeleteBlog(new BlogDto());
        }

        [TestMethod]
        public void DeleteBlog_WhenPassingValidBlogDTO_ServiceReturnSuccessful()
        {
            ArrangeForDeleting();

            var receivedResult = DeleteValidBlog();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void DeleteBlog_WhenPassingInvalidBlogDTO_ServiceReturnFailed()
        {
            ArrangeForDeleting();

            var receivedResult = DeleteInvalidBlog();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }

        [TestMethod]
        public void DeleteBlog_WhenPassingValidBlogDTO_DeletingOccured()
        {
            ArrangeForDeleting();

            DeleteValidBlog();

            _blogRepository.Verify(r => r.DeleteEntity(It.IsAny<Blog>()), Times.Once());
        }

        [TestMethod]
        public void DeleteBlog_WhenPassingCategories_CategoryDeletingOccured()
        {
            ArrangeForDeleting(withCategories:true);           

            DeleteValidBlog();

            _categoryService.Verify(s => s.DeleteCategory(It.IsAny<CategoryDto>(), It.IsAny<ICategoryRepository>()), Times.Once());
        }

        [TestMethod]
        public void DeleteBlog_WhenPassingCategories_ServiceReturnSuccessful()
        {
            ArrangeForDeleting(withCategories: true);

            var receivedResult = DeleteValidBlog();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void DeleteBlog_WhenNOTPassingCategories_CategoryDeletingNOTCalled()
        {
            ArrangeForDeleting();

            DeleteValidBlog();

            _categoryService.Verify(s => s.DeleteCategory(It.IsAny<CategoryDto>(), It.IsAny<ICategoryRepository>()), Times.Never());
        }

        [TestMethod]
        public void DeleteBlog_WhenPassingPosts_PostsDeletingOccured()
        {
            ArrangeForDeleting(withPosts:true);

            DeleteValidBlog();

            _postService.Verify(s => s.DeletePost(It.IsAny<PostDto>(), It.IsAny<IPostRepository>()), Times.Once());
        }

        [TestMethod]
        public void DeleteBlog_WhenPassingPosts_ServiceReturnSuccessful()
        {
            ArrangeForDeleting(withPosts: true);

            var receivedResult = DeleteValidBlog();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void DeleteBlog_WhenNOTPassingPosts_PostsDeletingNOTCalled()
        {
            ArrangeForDeleting();

            DeleteValidBlog();

            _postService.Verify(s => s.DeletePost(It.IsAny<PostDto>(), It.IsAny<IPostRepository>()), Times.Never());
        }

        [TestMethod]
        public void DeleteBlog_WhenAnyException_ServiceReturnFailed()
        {
            _blogRepository
                .Setup(r => r.GetEntityById(It.IsAny<int>()))
                .Throws(new Exception());

            var receivedResult = DeleteValidBlog();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }
        #endregion

        #region DataSelectionTests


        [TestMethod]
        public void GetBlogById_ReturningBlogDto_ReturnServiceAnswerWithSuccessfulStatus()
        {
            _blogRepository
                .Setup(r => r.GetEntityById(It.IsAny<int>()))
                .Returns(It.IsAny<Blog>());

            _repositoryFactory
                .Setup(f => f.CreateBlogRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_blogRepository.Object);

            var receivedResult = _blogService.GetBlogById(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult.Status);
        }


        [TestMethod]
        public void GetBlogById_BlogRepositoryThrowsException_ReturnServiceAnswerWithFailedStatus()
        {
            _blogRepository
                .Setup(r => r.GetEntityById(It.IsAny<int>()))
                .Throws(new Exception());

            _repositoryFactory
                .Setup(f => f.CreateBlogRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_blogRepository.Object);

            var receivedResult = _blogService.GetBlogById(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Failed, receivedResult.Status);
        }

        [TestMethod]
        public void GetBlogByUserId_ReturningBlogDto_ReturnServiceAnswerWithSuccessfulStatus()
        {
            _blogRepository.Setup(r => r.GetBlogByUserId(It.IsAny<int>())).Returns(It.IsAny<Blog>());
            _repositoryFactory.Setup(f => f.CreateBlogRepository(It.IsAny<IUnitOfWork>())).Returns(_blogRepository.Object);

            var receivedResult = _blogService.GetBlogByUserId(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult.Status);
        }

        [TestMethod]
        public void GetBlogByUserId_BlogRepositoryThrowsException_ReturnServiceAnswerWithFailedStatus()
        {
            _blogRepository
                .Setup(r => r.GetBlogByUserId(It.IsAny<int>()))
                .Throws(new Exception());

            _repositoryFactory
                .Setup(f => f.CreateBlogRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_blogRepository.Object);

            var receivedResult = _blogService.GetBlogByUserId(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Failed, receivedResult.Status);
        }
        #endregion
    }
}
