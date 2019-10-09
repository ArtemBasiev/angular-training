using bizapps_test.BLL.DTO;
using bizapps_test.Domain.Models;

namespace bizapps_test.BLL.Mappers
{
    public class BlogMapper
    {
        public BlogDto MapToBlogDto(Blog entityMapFrom)
        {
            var blogDtoToMap = new BlogDto
            {
                Id = entityMapFrom.Id,
                BlogTitle = entityMapFrom.BlogTitle,
                CreationDate = entityMapFrom.CreationDate
            };
            return blogDtoToMap;
        }
    }
}