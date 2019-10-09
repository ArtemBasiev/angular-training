using bizapps_test.BLL.DTO;
using Moq;
using bizapps_test.BLL.Interfaces;
using bizapps_test.SL.Wcf_Services.DataContracts;
using NUnit;
using NUnit.Framework;


namespace bizapps_test.SL.Wcf_Services.Tests
{
    [TestFixture]
    public class WcfBlogUserServiceTest
    {
        private WcfBlogUserService WcfBlogUserService { get; set; }

        [SetUp]
        public void SetWcfBlogUserService()
        {
            WcfBlogUserService = new WcfBlogUserService();
        }


        [Test]
        public void CreateBlogUser_WhenAddNewUser_Working()
        {
            int expectedUserId = 1;
            IBlogUserService bloguserService = Mock.Of<IBlogUserService>(user => user.CreateBlogUser(It.IsAny<BlogUserDto>()) == expectedUserId);
            WcfBlogUserService.BlogUserService = bloguserService;
            int resultUserId = WcfBlogUserService.CreateBlogUser(new BlogUserDC());

            Assert.AreEqual(expectedUserId, resultUserId);
        }

        //[TestMethod]
        //[ExpectedException(typeof(ApplicationException))]
        //public void CreateBlogUser_WhenAddNewUser_CatchException()
        //{
        //    Mock<IBlogUserRepository> bloguserRepository = new Mock<IBlogUserRepository>(MockBehavior.Strict);
        //    bloguserRepository.Setup(id => id.CreateBlogUser(It.IsAny<BlogUser>())).Throws(new ApplicationException());

        //    BlogUserService bloguserService = new BlogUserService(bloguserRepository.Object);
        //    bloguserService.CreateBlogUser(BloguserDto);
        //}


        //[TestMethod]
        //public void UpdateBlogUser_WhenUpdateUser_Working()
        //{
        //    int expectedUserId = 1;
        //    IBlogUserRepository bloguserRepository = Mock.Of<IBlogUserRepository>(id => id.UpdateBlogUser(It.IsAny<BlogUser>()) == expectedUserId);

        //    BlogUserService bloguserService = new BlogUserService(bloguserRepository);
        //    int resultUserId = bloguserService.UpdateBlogUser(BloguserDto);

        //    Assert.AreEqual(expectedUserId, resultUserId);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ApplicationException))]
        //public void UpdateBlogUser_WhenUpdateUser_CatchException()
        //{
        //    Mock<IBlogUserRepository> bloguserRepository = new Mock<IBlogUserRepository>(MockBehavior.Strict);
        //    bloguserRepository.Setup(id => id.UpdateBlogUser(It.IsAny<BlogUser>())).Throws(new ApplicationException());

        //    BlogUserService bloguserService = new BlogUserService(bloguserRepository.Object);
        //    bloguserService.UpdateBlogUser(BloguserDto);
        //}


        //[TestMethod]
        //public void DeleteBlogUser_WhenDeleteUser_Working()
        //{
        //    int expectedUserId = 1;
        //    IBlogUserRepository bloguserRepository = Mock.Of<IBlogUserRepository>(id => id.DeleteBlogUser(It.IsAny<BlogUser>()) == expectedUserId);

        //    BlogUserService bloguserService = new BlogUserService(bloguserRepository);
        //    int resultUserId = bloguserService.DeleteBlogUser(BloguserDto);

        //    Assert.AreEqual(expectedUserId, resultUserId);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ApplicationException))]
        //public void DeleteBlogUser_WhenDeleteUser_CatchException()
        //{
        //    Mock<IBlogUserRepository> bloguserRepository = new Mock<IBlogUserRepository>(MockBehavior.Strict);
        //    bloguserRepository.Setup(id => id.DeleteBlogUser(It.IsAny<BlogUser>())).Throws(new ApplicationException());

        //    BlogUserService bloguserService = new BlogUserService(bloguserRepository.Object);
        //    bloguserService.DeleteBlogUser(BloguserDto);
        //}


        //[TestMethod]
        //public void GetBlogUserById_WhenGetUser_Working()
        //{
        //    int expectedUserId = 1;
        //    IBlogUserRepository bloguserRepository = Mock.Of<IBlogUserRepository>(bloguser => bloguser.GetBlogUserById(It.IsAny<int>()) == new BlogUser(expectedUserId));

        //    BlogUserService bloguserService = new BlogUserService(bloguserRepository);
        //    int resultUserId = bloguserService.GetBlogUserById(expectedUserId).Id;

        //    Assert.AreEqual(expectedUserId, resultUserId);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ApplicationException))]
        //public void GetBlogUserById_WhenGetUser_CatchException()
        //{
        //    int TestingId = 0;
        //    Mock<IBlogUserRepository> bloguserRepository = new Mock<IBlogUserRepository>(MockBehavior.Strict);
        //    bloguserRepository.Setup(user => user.GetBlogUserById(It.IsAny<int>())).Throws(new ApplicationException());

        //    BlogUserService bloguserService = new BlogUserService(bloguserRepository.Object);
        //    bloguserService.GetBlogUserById(TestingId);
        //}

        //[TestMethod]
        //public void GetAllUsers_WhenGetUsers_Working()
        //{
        //    int expectedUserId = 1;
        //    IBlogUserRepository bloguserRepository = Mock.Of<IBlogUserRepository>(blogusers => blogusers.GetAllBlogUsers() == new List<BlogUser> { new BlogUser(expectedUserId) });

        //    BlogUserService bloguserService = new BlogUserService(bloguserRepository);
        //    List<BlogUserDto> gettedBlogUsers = (List<BlogUserDto>)bloguserService.GetAllUsers();
        //    int resultUserId = gettedBlogUsers[0].Id;

        //    Assert.AreEqual(expectedUserId, resultUserId);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ApplicationException))]
        //public void GetAllUsers_WhenGetUsers_CatchException()
        //{
        //    Mock<IBlogUserRepository> bloguserRepository = new Mock<IBlogUserRepository>(MockBehavior.Strict);
        //    bloguserRepository.Setup(user => user.GetAllBlogUsers()).Throws(new ApplicationException());

        //    BlogUserService bloguserService = new BlogUserService(bloguserRepository.Object);
        //    bloguserService.GetAllUsers();
        //}


        //[TestMethod]
        //public void GetBlogUserNameAndPassword_WhenGetNameAndPassword_Working()
        //{
        //    string expectedUserName = "user";
        //    IBlogUserRepository bloguserRepository = Mock.Of<IBlogUserRepository>(bloguser => bloguser.GetBlogUserByNameAndPassword(It.IsAny<string>(), It.IsAny<string>()) == new BlogUser(expectedUserName));

        //    BlogUserService bloguserService = new BlogUserService(bloguserRepository);
        //    BlogUserDto gettedBlogUser = bloguserService.GetBlogUserNameAndPassword(new BlogUserDto());
        //    string resultUserName = gettedBlogUser.UserName;

        //    Assert.AreEqual(expectedUserName, resultUserName);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ApplicationException))]
        //public void GetBlogUserNameAndPassword_WhenGetNameAndPassword_CatchException()
        //{
        //    Mock<IBlogUserRepository> bloguserRepository = new Mock<IBlogUserRepository>(MockBehavior.Strict);
        //    bloguserRepository.Setup(user => user.GetBlogUserByNameAndPassword(It.IsAny<string>(), It.IsAny<string>())).Throws(new ApplicationException());

        //    BlogUserService bloguserService = new BlogUserService(bloguserRepository.Object);
        //    bloguserService.GetBlogUserNameAndPassword(new BlogUserDto());
        //}

        //[TestMethod]
        //public void GetAdminPermission_WhenGetPermission_Working()
        //{
        //    int expectedUserId = 1;
        //    IBlogUserRepository bloguserRepository = Mock.Of<IBlogUserRepository>(bloguser => bloguser.GetAdminPermission(It.IsAny<string>()) == expectedUserId);

        //    BlogUserService bloguserService = new BlogUserService(bloguserRepository);
        //    int resultUserId = bloguserService.GetAdminPermission(It.IsAny<string>());

        //    Assert.AreEqual(expectedUserId, resultUserId);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ApplicationException))]
        //public void GetAdminPermission_WhenGetPermission_CatchException()
        //{

        //    Mock<IBlogUserRepository> bloguserRepository = new Mock<IBlogUserRepository>(MockBehavior.Strict);
        //    bloguserRepository.Setup(user => user.GetAdminPermission(It.IsAny<string>())).Throws(new ApplicationException());

        //    BlogUserService bloguserService = new BlogUserService(bloguserRepository.Object);
        //    bloguserService.GetAdminPermission(It.IsAny<string>());
        //}
    }
}
