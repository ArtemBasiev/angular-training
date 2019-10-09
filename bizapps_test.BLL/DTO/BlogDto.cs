using System;
using System.Collections.Generic;

namespace bizapps_test.BLL.DTO
{
    public class BlogDto
    {
        public int Id { get; set; }

        public string BlogTitle { get; set; }

        public DateTime CreationDate { get; set; }

        public BlogUserDto CreatedBy { get; set; }

        public IEnumerable<CategoryDto> BlogCategories { get; set; } = new List<CategoryDto>();

        public IEnumerable<PostDto> BlogPosts { get; set; } = new List<PostDto>();
    }
}