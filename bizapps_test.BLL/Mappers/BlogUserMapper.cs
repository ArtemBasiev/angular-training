using bizapps_test.BLL.DTO;
using bizapps_test.Domain.Models;

namespace bizapps_test.BLL.Mappers
{
    public class BlogUserMapper
    {
        public BlogUserDto MapToBlogUserDto(BlogUser entityMapFrom)
        {
            var blogUserDtoToMap = new BlogUserDto
            {
                Id = entityMapFrom.Id,
                UserName = entityMapFrom.UserName,
                UserPassword = entityMapFrom.UserPassword
            };
            return blogUserDtoToMap;
        }
    }
}