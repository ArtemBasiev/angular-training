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
    public class CategoryServiceTests
    {
        private static Mock<ICustomLogger> _logger;
        private static Mock<IUnitOfWorkFactory> _unitOfWorkFactory;
        private static Mock<IRepositoryFactory> _repositoryFactory;
        private static Mock<IPostRepository> _postRepository;
        private static Mock<ICategoryRepository> _categoryRepository;
        private static Mock<IUnitOfWork> _unitOfWork;
        private static Mock<ILogFactory> _logFactory;
        private static CategoryService _categoryService;


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
            _repositoryFactory = new Mock<IRepositoryFactory>();
            _categoryRepository = new Mock<ICategoryRepository>();
            _postRepository = new Mock<IPostRepository>();
            _categoryService = new CategoryService(_unitOfWorkFactory.Object, _repositoryFactory.Object, _logFactory.Object);
        }

        #region CreatingTests
        private static void ArrangeForCreating()
        {
            _categoryRepository
                .Setup(r => r.CreateEntity(It.IsAny<Category>()))
                .Returns(It.IsAny<int>());

            _repositoryFactory
                .Setup(f => f.CreateCategoryRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_categoryRepository.Object);
        }

        private static AnswerStatus CreateValidCategory()
        {
            var dto = new CategoryDto
            {
                CategoryName = "testCategory",
                RelatedTo = new BlogDto
                {
                    BlogTitle = "testblog",
                    CreationDate = DateTime.Now
                }
            };

            return _categoryService.CreateCategory(dto);
        }

        private static AnswerStatus CreateInvalidCategory()
        {
            return _categoryService.CreateCategory(new CategoryDto());
        }


        [TestMethod]
        public void CreateCategory_WhenPassingValidCategoryDTO_ServiceReturnSuccessful()
        {
            ArrangeForCreating();

            var receivedResult = CreateValidCategory();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void CreateCategory_WhenPassingValidCategoryDTO_CreationOccured()
        {
            ArrangeForCreating();

            CreateValidCategory();

            _categoryRepository.Verify(r => r.CreateEntity(It.IsAny<Category>()), Times.Once());
        }

        [TestMethod]
        public void CreateCategory_WhenPassingInvalidCategoryDTO_ServiceReturnFailed()
        {
            ArrangeForCreating();

            var receivedResult = CreateInvalidCategory();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }

        [TestMethod]
        public void CreateCategory_WhenAnyException_ServiceReturnFailed()
        {
            _repositoryFactory
                .Setup(f => f.CreateCategoryRepository(It.IsAny<IUnitOfWork>()))
                .Throws(new Exception());

            var receivedResult = CreateValidCategory();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }
        #endregion

        #region UpdatingTests
        private static void ArrangeForUpdating()
        {
            _categoryRepository
                .Setup(r => r.UpdateEntity(It.IsAny<Category>()))
                .Returns(It.IsAny<int>());

            _repositoryFactory
                .Setup(f => f.CreateCategoryRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_categoryRepository.Object);
        }

        private static AnswerStatus UpdateValidCategory()
        {
            var dto = new CategoryDto
            {
                Id = 1,
                CategoryName = "testCategory"
            };

            return _categoryService.UpdateCategory(dto);
        }

        private static AnswerStatus UpdateInvalidCategory()
        {
            return _categoryService.UpdateCategory(new CategoryDto());
        }

        [TestMethod]
        public void UpdateCategory_WhenPassingValidCategoryDTO_ServiceReturnSuccessful()
        {
            ArrangeForUpdating();

            var receivedResult = UpdateValidCategory();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void UpdateCategory_WhenPassingValidCategoryDTO_UpdatingOccured()
        {
            ArrangeForUpdating();

            UpdateValidCategory();

            _categoryRepository.Verify(r => r.UpdateEntity(It.IsAny<Category>()), Times.Once());
        }

        [TestMethod]
        public void UpdateCategory_WhenPassingInvalidCategoryDTO_ServiceReturnFailed()
        {
            ArrangeForUpdating();

            var receivedResult = UpdateInvalidCategory();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }

        [TestMethod]
        public void UpdatingCategory_WhenAnyException_ServiceReturnFailed()
        {
            _repositoryFactory
                .Setup(f => f.CreateCategoryRepository(It.IsAny<IUnitOfWork>()))
                .Throws(new Exception());

            var receivedResult = UpdateValidCategory();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }

        #endregion

        #region DeletingTests
        private static void ArrangeForDeleting(bool withPosts = false)
        {
            _categoryRepository
                .Setup(r => r.DeleteEntity(It.IsAny<Category>()))
                .Returns(It.IsAny<int>());

            _categoryRepository
                .Setup(r => r.GetEntityById(It.IsAny<int>()))
                .Returns(new Category("test"));

            _repositoryFactory
                .Setup(f => f.CreateCategoryRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_categoryRepository.Object);

            if (withPosts)
            {
                _postRepository
                    .Setup(r => r.GetPostsByCategoryId(It.IsAny<int>()))
                    .Returns(new List<Post> { new Post("testpost", "test", DateTime.Now) });

                _postRepository
                    .Setup(r => r.RemoveCategoryFromPost(It.IsAny<Post>(), It.IsAny<Category>()));
            }
            else
            {
                _postRepository
                    .Setup(r => r.GetPostsByCategoryId(It.IsAny<int>()))
                    .Returns(new List<Post>());
            }

            _repositoryFactory
                .Setup(f => f.CreatePostRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_postRepository.Object);
        }

        private static AnswerStatus DeleteValidCategory()
        {
            var dto = new CategoryDto
            {
                Id = 1
            };

            return _categoryService.DeleteCategory(dto);
        }

        private static AnswerStatus DeleteInvalidCategory()
        {
            return _categoryService.DeleteCategory(new CategoryDto());
        }

        [TestMethod]
        public void DeleteCategory_WhenPassingValidCategoryDTO_ServiceReturnSuccessful()
        {
            ArrangeForDeleting();

            var receivedResult = DeleteValidCategory();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void DeleteCategory_WhenPassingValidCategoryDTO_DeletingOccured()
        {
            ArrangeForDeleting();

            DeleteValidCategory();

            _categoryRepository.Verify(r => r.DeleteEntity(It.IsAny<Category>()), Times.Once());
        }

        [TestMethod]
        public void DeleteCategory_WhenPassingInvalidCategoryDTO_ServiceReturnFailed()
        {
            ArrangeForDeleting();

            var receivedResult = DeleteInvalidCategory();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }

        [TestMethod]
        public void DeleteCategory_WhenPostRelationsExist_ServiceReturnSuccessful()
        {
            ArrangeForDeleting(withPosts: true);

            var receivedResult = DeleteValidCategory();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void DeleteCategory_WhenPostRelationsExist_PostRelationDeletingCalled()
        {
            ArrangeForDeleting(withPosts: true);

            DeleteValidCategory();

            _postRepository.Verify(r => r.RemoveCategoryFromPost(It.IsAny<Post>(), It.IsAny<Category>()), Times.Once());
        }

        [TestMethod]
        public void DeleteCategory_WhenPostRelationsNOTExist_PostRelationDeletingNOTCalled()
        {
            ArrangeForDeleting();

            DeleteValidCategory();

            _postRepository.Verify(r => r.RemoveCategoryFromPost(It.IsAny<Post>(), It.IsAny<Category>()), Times.Never());
        }


        [TestMethod]
        public void DeleteCategory_WhenAnyException_ServiceReturnFailed()
        {
            _repositoryFactory
                .Setup(f => f.CreateCategoryRepository(It.IsAny<IUnitOfWork>()))
                .Throws(new Exception());

            var receivedResult = DeleteValidCategory();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }
        #endregion

        #region DataSelectionTests

        [TestMethod]
        public void GetCategoryById_ReturningCategoryDto_ReturnServiceAnswerWithSuccessfulStatus()
        {
            _categoryRepository
                .Setup(r => r.GetEntityById(It.IsAny<int>()))
                .Returns(It.IsAny<Category>());

            _repositoryFactory
                .Setup(f => f.CreateCategoryRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_categoryRepository.Object);

            var receivedResult = _categoryService.GetCategoryById(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult.Status);
        }

        [TestMethod]
        public void GetCategoryById_CategoryRepositoryThrowsException_ReturnServiceAnswerWithFailedStatus()
        {
            _categoryRepository
                .Setup(r => r.GetEntityById(It.IsAny<int>())).Throws(new Exception());
            _repositoryFactory
                .Setup(f => f.CreateCategoryRepository(It.IsAny<IUnitOfWork>())).Returns(_categoryRepository.Object);

            var receivedResult = _categoryService.GetCategoryById(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Failed, receivedResult.Status);
        }

        [TestMethod]
        public void GetBlogCategories_ReturningCategoryDtoList_ReturnServiceAnswerWithSuccessfulStatus()
        {
            _categoryRepository
                .Setup(r => r.GetBlogCategories(It.IsAny<int>()))
                .Returns(It.IsAny<List<Category>>());

            _repositoryFactory
                .Setup(f => f.CreateCategoryRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_categoryRepository.Object);

            var receivedResult = _categoryService.GetBlogCategories(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult.Status);
        }

        [TestMethod]
        public void GetBlogCategories_CategoryRepositoryThrowsException_ReturnServiceAnswerWithFailedStatus()
        {
            _categoryRepository
                .Setup(r => r.GetBlogCategories(It.IsAny<int>()))
                .Throws(new Exception());

            _repositoryFactory
                .Setup(f => f.CreateCategoryRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_categoryRepository.Object);

            var receivedResult = _categoryService.GetBlogCategories(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Failed, receivedResult.Status);
        }

        [TestMethod]
        public void GetPostCategories_ReturningCategoryDtoList_ReturnServiceAnswerWithSuccessfulStatus()
        {
            _categoryRepository
                .Setup(r => r.GetPostCategories(It.IsAny<int>()))
                .Returns(It.IsAny<List<Category>>());

            _repositoryFactory
                .Setup(f => f.CreateCategoryRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_categoryRepository.Object);

            var receivedResult = _categoryService.GetPostCategories(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult.Status);
        }

        [TestMethod]
        public void GetPostCategories_CategoryRepositoryThrowsException_ReturnServiceAnswerWithFailedStatus()
        {
            _categoryRepository
                .Setup(r => r.GetPostCategories(It.IsAny<int>()))
                .Throws(new Exception());

            _repositoryFactory
                .Setup(f => f.CreateCategoryRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_categoryRepository.Object);

            var receivedResult = _categoryService.GetPostCategories(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Failed, receivedResult.Status);
        }
        #endregion

    }
}
