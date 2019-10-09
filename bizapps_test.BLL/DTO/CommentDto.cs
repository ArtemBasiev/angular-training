using System;

namespace bizapps_test.BLL.DTO
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public string UserName { get; set; }
        public DateTime CreationDate { get; set; }
        public PostDto RelatedTo { get; set; }
        public BlogUserDto CreatedBy { get; set; }
    }
}