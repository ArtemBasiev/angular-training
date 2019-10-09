using System;
using System.Collections.Generic;

namespace bizapps_test.BLL.DTO
{
    public class PostDto
    {
        public int Id { get; set; }

        public string PostTitle { get; set; }

        public string PostContent { get; set; }

        public DateTime CreationDate { get; set; }

        public BlogDto RelatedTo { get; set; }

        public IEnumerable<CategoryDto> PostCategories { get; set; } = new List<CategoryDto>();

        public IEnumerable<CommentDto> PostComments { get; set; } = new List<CommentDto>();
    }
}