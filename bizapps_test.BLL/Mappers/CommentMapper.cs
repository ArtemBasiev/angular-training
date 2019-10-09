using System.Collections.Generic;
using bizapps_test.BLL.DTO;
using bizapps_test.Domain.Models;

namespace bizapps_test.BLL.Mappers
{
    public class CommentMapper
    {
        private readonly BlogUserMapper _blogUserMapper;
        private readonly PostMapper _postMapper;

        public CommentMapper()
        {
           _blogUserMapper = new BlogUserMapper();
           _postMapper = new PostMapper();
        }


        public IEnumerable<CommentDto> MapToCommentDtoList(IEnumerable<Comment> commentListMapFrom)
        {
            var commentDtoList = new List<CommentDto>();
            foreach (var comment in commentListMapFrom)
            {
                commentDtoList.Add(MapToCommentDto(comment));
            }

            return commentDtoList;
        }


        public CommentDto MapToCommentDto(Comment entityMapFrom)
        {
            var commentDtoMapTo = new CommentDto
            {
                Id = entityMapFrom.Id,
                CommentText = entityMapFrom.CommentText,
                CreationDate = entityMapFrom.CreationDate
            };
            return commentDtoMapTo;
        }
    }
}
