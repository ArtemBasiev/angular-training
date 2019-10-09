namespace bizapps_test.BLL.DTO
{
    public class BlogUserDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public BlogDto UserBlog { get; set; }
    }
}