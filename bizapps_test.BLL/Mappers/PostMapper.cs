using System.Collections.Generic;
using System.Linq;
using bizapps_test.BLL.DTO;
using bizapps_test.Domain.Models;

namespace bizapps_test.BLL.Mappers
{
    public class PostMapper
    {

        public IEnumerable<PostDto> MapToPostDtoList(IEnumerable<Post> postListMapFrom)
        {
            var postDtoList = new List<PostDto>();
            foreach (var post in postListMapFrom)
            {
                postDtoList.Add(MapToPostDto(post));
            }

            return postDtoList;
        }


        public PostDto MapToPostDto(Post entityMapFrom)
        {
            var postDtoMapTo = new PostDto
            {
                Id = entityMapFrom.Id,
                PostTitle = entityMapFrom.PostTitle,
                PostContent = entityMapFrom.PostContent,
                CreationDate =entityMapFrom.CreationDate
            };

            return postDtoMapTo;
        }
    }
}