using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Logger.Abstract;
using bizapps_test.BLL.Services.Implementation;
using bizapps_test.DAL.Repositories;
using bizapps_test.Domain.ModelBuilders.Concrete;
using bizapps_test.Domain.Models;
using Moq;

namespace bizapps_test.BLL.Tests
{
    [TestClass]
    public class PostServiceTests
    {
        private static Mock<ICustomLogger> _logger;
        private static Mock<IUnitOfWorkFactory> _unitOfWorkFactory;
        private static Mock<IRepositoryFactory> _repositoryFactory;
        private static Mock<IPostRepository> _postRepository;
        private static Mock<ICategoryRepository> _categoryRepository;
        private static Mock<ICommentRepository> _commentRepository;
        private static Mock<IUnitOfWork> _unitOfWork;
        private static Mock<ILogFactory> _logFactory;
        private static PostService _postService;


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
            _categoryRepository = new Mock<ICategoryRepository>();
            _commentRepository = new Mock<ICommentRepository>();
            _repositoryFactory = new Mock<IRepositoryFactory>();
            _postRepository = new Mock<IPostRepository>();
            _postService = new PostService(_unitOfWorkFactory.Object, _repositoryFactory.Object, _logFactory.Object);
        }

        #region CreatingTests
        private static void ArrangeForCreating(bool withCategories = false)
        {
            _postRepository
                .Setup(r => r.CreateEntity(It.IsAny<Post>()))
                .Returns(It.IsAny<int>());

            _repositoryFactory
                .Setup(f => f.CreatePostRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_postRepository.Object);

            if (withCategories)
            {
                _postRepository
                    .Setup(r => r.AddCategoryToPost(It.IsAny<Post>(), It.IsAny<Category>()));

                _categoryRepository
                    .Setup(r => r.GetEntityById(It.IsAny<int>()))
                    .Returns(new Category("test"));

                _repositoryFactory
                    .Setup(f => f.CreateCategoryRepository(It.IsAny<IUnitOfWork>()))
                    .Returns(_categoryRepository.Object);
            }
        }


        private static AnswerStatus CreateValidPost(bool withCategories = false)
        {
            var dto = new PostDto
            {
                PostTitle = "test",
                PostContent = "test",
                CreationDate = DateTime.Now,
                RelatedTo = new BlogDto
                {
                    BlogTitle = "testblog",
                    CreationDate = DateTime.Now
                }
            };

            if (withCategories)
            {
                dto.PostCategories = new List<CategoryDto> { new CategoryDto() };
            }

            return _postService.CreatePost(dto);
        }

        private static AnswerStatus CreateInvalidPost()
        {
            return _postService.CreatePost(new PostDto());
        }

        [TestMethod]
        public void CreatePost_WhenPassingValidPostDTO_ServiceReturnSuccessful()
        {
            ArrangeForCreating();

            var receivedResult = CreateValidPost();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void CreatePost_WhenPassingValidPostDTO_CreatingOccured()
        {
            ArrangeForCreating();

            CreateValidPost();

            _postRepository.Verify(r => r.CreateEntity(It.IsAny<Post>()), Times.Once());
        }

        [TestMethod]
        public void CreatePost_WhenPassinginvalidPostDTO_ServiceReturnFailed()
        {
            ArrangeForCreating();

            var receivedResult = CreateInvalidPost();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }

        [TestMethod]
        public void CreatePost_WhenPassingCategoryDTOs_CategoryAddingCalled()
        {
            ArrangeForCreating(withCategories: true);

            CreateValidPost(withCategories: true);

            _postRepository.Verify(r => r.AddCategoryToPost(It.IsAny<Post>(), It.IsAny<Category>()), Times.Once());
        }

        [TestMethod]
        public void CreatePost_WhenPassingCategoryDTOs_ServiceReturnSuccessful()
        {
            ArrangeForCreating(withCategories: true);

            var receivedResult = CreateValidPost(withCategories: true);

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }


        [TestMethod]
        public void CreatePost_WhenNOTPassingCategoryDTOs_CategoryAddingNOTCalled()
        {
            ArrangeForCreating();

            CreateValidPost();

            _postRepository.Verify(r => r.AddCategoryToPost(It.IsAny<Post>(), It.IsAny<Category>()), Times.Never());
        }

        [TestMethod]
        public void CreatePost_WhenAnyException_ReturnFailedStatus()
        {
            _repositoryFactory
                .Setup(f => f.CreatePostRepository(It.IsAny<IUnitOfWork>()))
                .Throws(new Exception());

            var receivedResult = CreateValidPost();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }
        #endregion

        #region UpdatingTests
        private static void ArrangeForUpdating(bool withExistentCategories = false)
        {
            _postRepository
                .Setup(r => r.UpdateEntity(It.IsAny<Post>())).Returns(It.IsAny<int>());

            _repositoryFactory
                .Setup(f => f.CreatePostRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_postRepository.Object);

            if (withExistentCategories)
            {
                var categoryBuilder = new CategoryBuilder();
                var receivedCategory = categoryBuilder.CreateCategory("postCategory");
                categoryBuilder.SetCategoryId(receivedCategory, 1);

                _categoryRepository
                    .Setup(r => r.GetPostCategories(It.IsAny<int>()))
                    .Returns(new List<Category> { receivedCategory });


                _postRepository
                    .Setup(r => r.RemoveCategoryFromPost(It.IsAny<Post>(), It.IsAny<Category>()));
            }
            else
            {
                _postRepository
                    .Setup(r => r.AddCategoryToPost(It.IsAny<Post>(), It.IsAny<Category>()));

                _categoryRepository
                    .Setup(r => r.GetPostCategories(It.IsAny<int>()))
                    .Returns(new List<Category>());
            }

            _repositoryFactory
                .Setup(f => f.CreateCategoryRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_categoryRepository.Object);
        }


        private static AnswerStatus UpdateValidPost(bool withNonexistentCategories = false)
        {
            var dto = new PostDto
            {
                Id = 1,
                PostTitle = "test",
                PostContent = "test",
                CreationDate = DateTime.Now,
                RelatedTo = new BlogDto
                {
                    BlogTitle = "testblog",
                    CreationDate = DateTime.Now
                }
            };

            if (withNonexistentCategories)
            {
                dto.PostCategories = new List<CategoryDto> { new CategoryDto { Id = 2 } };
            }

            return _postService.UpdatePost(dto);
        }

        private static AnswerStatus UpdateInvalidPost()
        {
            return _postService.UpdatePost(new PostDto());
        }

        [TestMethod]
        public void UpdatePost_WhenPassingValidPostDTO_ServiceReturnSuccessful()
        {
            ArrangeForUpdating();

            var receivedResult = UpdateValidPost();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void UpdatePost_WhenPassingValidPostDTO_PostUpdatingOccured()
        {
            ArrangeForUpdating();

            UpdateValidPost();

            _postRepository.Verify(r => r.UpdateEntity(It.IsAny<Post>()), Times.Once());

        }

        [TestMethod]
        public void UpdatePost_WhenPassingInvalidPostDTO_ServiceReturnFailed()
        {
            ArrangeForUpdating();

            var receivedResult = UpdateInvalidPost();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }

        [TestMethod]
        public void UpdatePost_WhenPassingCategoryDTONotExists_CategoryAddingCalled()
        {
            ArrangeForUpdating();

            UpdateValidPost(withNonexistentCategories: true);

            _postRepository.Verify(r => r.AddCategoryToPost(It.IsAny<Post>(), It.IsAny<Category>()), Times.Once());
        }

        [TestMethod]
        public void UpdatePost_WhenPassingCategoryDTONotExists_ServiceReturnSuccessful()
        {
            ArrangeForUpdating();

            var receivedResult = UpdateValidPost(withNonexistentCategories: true);

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void UpdatePost_WhenNOTPassingCategoryDTO_CategoryAddingNOTCalled()
        {
            ArrangeForUpdating();

            UpdateValidPost();

            _postRepository.Verify(r => r.AddCategoryToPost(It.IsAny<Post>(), It.IsAny<Category>()), Times.Never());
        }

        [TestMethod]
        public void UpdatePost_WhenExistingCategoryNotPassing_CategoryRelationRemovingCalled()
        {
            ArrangeForUpdating(withExistentCategories: true);

            UpdateValidPost();

            _postRepository.Verify(r => r.RemoveCategoryFromPost(It.IsAny<Post>(), It.IsAny<Category>()), Times.Once());
        }

        [TestMethod]
        public void UpdatePost_WhenExistingCategoryNotPassing_ServiceReturnSuccessful()
        {
            ArrangeForUpdating(withExistentCategories: true);

            var receivedResult = UpdateValidPost();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void UpdatePost_WhenCategoriesNotExists_CategoryRelationRemovingNotCalled()
        {
            ArrangeForUpdating();

            UpdateValidPost();

            _postRepository.Verify(r => r.RemoveCategoryFromPost(It.IsAny<Post>(), It.IsAny<Category>()), Times.Never());
        }


        [TestMethod]
        public void UpdatePost_WhenAnyException_ServiceReturnFailed()
        {
            _repositoryFactory
                .Setup(f => f.CreatePostRepository(It.IsAny<IUnitOfWork>()))
                .Throws(new Exception());

            var receivedResult = UpdateValidPost();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }
        #endregion

        #region DeletingTests
        private static void ArrangeForDeleting(bool withComments = false, bool withCategories = false)
        {
            _postRepository
                .Setup(r => r.DeleteEntity(It.IsAny<Post>()))
                .Returns(It.IsAny<int>());

            var postBuilder = new PostBuilder();
            var postToDelete = new Post("test", "test", DateTime.Now);
            postBuilder.SetPostId(postToDelete, 1);

            _postRepository
                .Setup(r => r.GetEntityById(It.IsAny<int>()))
                .Returns(postToDelete);

            _repositoryFactory
                .Setup(f => f.CreatePostRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_postRepository.Object);

            if (withCategories)
            {
                _postRepository
                    .Setup(r => r.RemoveCategoryFromPost(It.IsAny<Post>(), It.IsAny<Category>()));

                _categoryRepository
                    .Setup(r => r.GetPostCategories(It.IsAny<int>()))
                    .Returns(new List<Category> { new Category("categoryToRemove") });
            }
            else
            {
                _categoryRepository
                    .Setup(r => r.GetPostCategories(It.IsAny<int>()))
                    .Returns(new List<Category>());


            }
            _repositoryFactory
                .Setup(f => f.CreateCategoryRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_categoryRepository.Object);

            if (withComments)
            {
                _commentRepository
                    .Setup(r => r.GetPostComments(It.IsAny<int>()))
                    .Returns(new List<Comment> { new Comment("commentToDelete", DateTime.Now) });

                _commentRepository
                    .Setup(r => r.DeleteEntity(It.IsAny<Comment>()))
                    .Returns(It.IsAny<int>());
            }
            else
            {
                _commentRepository
                    .Setup(r => r.GetPostComments(It.IsAny<int>()))
                    .Returns(new List<Comment>());
            }

            _repositoryFactory
                .Setup(f => f.CreateCommentRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_commentRepository.Object);
        }

        private static AnswerStatus DeleteValidPost()
        {
            var dto = new PostDto { Id = 1 };

            return _postService.DeletePost(dto);
        }

        private static AnswerStatus DeleteInvalidPost()
        {
            return _postService.DeletePost(new PostDto());
        }


        [TestMethod]
        public void DeletePost_WhenPassingValidPostDTO_ServiceReturnSuccessful()
        {
            ArrangeForDeleting();

            var receivedResult = DeleteValidPost();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void DeletePost_WhenPassingValidPostDTO_DeletingOccured()
        {
            ArrangeForDeleting();

            DeleteValidPost();

            _postRepository.Verify(r => r.DeleteEntity(It.IsAny<Post>()), Times.Once());
        }

        [TestMethod]
        public void DeletePost_WhenPassingInvalidPostDTO_ServiceReturnFailed()
        {
            ArrangeForDeleting();

            var receivedResult = DeleteInvalidPost();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }

        [TestMethod]
        public void DeletePost_WhenCategoriesExist_ServviceReturnSuccessful()
        {
            ArrangeForDeleting(withCategories: true);

            var receivedResult = DeleteValidPost();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void DeletePost_WhenCategoriesExist_CategoryRelationDeletingCalled()
        {
            ArrangeForDeleting(withCategories: true);

            DeleteValidPost();

            _postRepository.Verify(r => r.RemoveCategoryFromPost(It.IsAny<Post>(), It.IsAny<Category>()), Times.Once());
        }

        [TestMethod]
        public void DeletePost_WhenCategoriesNOTExist_CategoryRelationDeletingNOTCalled()
        {
            ArrangeForDeleting();

            DeleteValidPost();

            _postRepository.Verify(r => r.RemoveCategoryFromPost(It.IsAny<Post>(), It.IsAny<Category>()), Times.Never());
        }

        [TestMethod]
        public void DeletePost_WhenCommentsExist_ServviceReturnSuccessful()
        {
            ArrangeForDeleting(withComments: true);

            var receivedResult = DeleteValidPost();

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult);
        }

        [TestMethod]
        public void DeletePost_WhenCommentsExist_CommentsDeletingCalled()
        {
            ArrangeForDeleting(withComments: true);

            DeleteValidPost();

            _commentRepository.Verify(r => r.DeleteEntity(It.IsAny<Comment>()), Times.Once());
        }

        [TestMethod]
        public void DeletePost_WhenCommentsNOTExist_CommentsDeletingNOTCalled()
        {
            ArrangeForDeleting();

            DeleteValidPost();

            _commentRepository.Verify(r => r.DeleteEntity(It.IsAny<Comment>()), Times.Never());
        }

        [TestMethod]
        public void DeletePost_WhenAnyException_ServiceReturnFailed()
        {
            _repositoryFactory
                .Setup(f => f.CreateCommentRepository(It.IsAny<IUnitOfWork>()))
                .Throws(new Exception());

            var receivedResult = DeleteValidPost();

            Assert.AreEqual(AnswerStatus.Failed, receivedResult);
        }

        #endregion

        #region DataSelectionTests
        [TestMethod]
        public void GetPostById_ReturningPostDto_ReturnServiceAnswerWithSuccessfulStatus()
        {
            _postRepository
                .Setup(r => r.GetEntityById(It.IsAny<int>()))
                .Returns(It.IsAny<Post>());

            _repositoryFactory
                .Setup(f => f.CreatePostRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_postRepository.Object);

            var receivedResult = _postService.GetPostById(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult.Status);
        }

        [TestMethod]
        public void GetPostById_PostRepositoryThrowsException_ReturnServiceAnswerWithFailedStatus()
        {
            _postRepository
                .Setup(r => r.GetEntityById(It.IsAny<int>()))
                .Throws(new Exception());

            _repositoryFactory
                .Setup(f => f.CreatePostRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_postRepository.Object);

            var receivedResult = _postService.GetPostById(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Failed, receivedResult.Status);
        }

        [TestMethod]
        public void GetBlogPosts_ReturningPostDtoList_ReturnServiceAnswerWithSuccessfulStatus()
        {
            _postRepository
                .Setup(r => r.GetPostsByBlogId(It.IsAny<int>()))
                .Returns(It.IsAny<List<Post>>());

            _repositoryFactory
                .Setup(f => f.CreatePostRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_postRepository.Object);

            var receivedResult = _postService.GetBlogPosts(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Successfull, receivedResult.Status);
        }

        [TestMethod]
        public void GetBlogPosts_PostRepositoryThrowsException_ReturnServiceAnswerWithFailedStatus()
        {
            _postRepository
                .Setup(r => r.GetPostsByBlogId(It.IsAny<int>()))
                .Throws(new Exception());

            _repositoryFactory
                .Setup(f => f.CreatePostRepository(It.IsAny<IUnitOfWork>()))
                .Returns(_postRepository.Object);

            var receivedResult = _postService.GetBlogPosts(It.IsAny<int>());

            Assert.AreEqual(AnswerStatus.Failed, receivedResult.Status);
        }
        #endregion

    }
}
