using bizapps_test.BLL.DTO;

namespace bizapps_test.BLL.Services
{
    public interface IUserService 
    {
        AnswerStatus CreateUser(BlogUserDto userDTO);

        AnswerStatus UpdateUser(BlogUserDto userDTO);

        AnswerStatus DeleteUser(BlogUserDto userDTO);

        ServiceAnswer<BlogUserDto> GetUserById(int userId);

        ServiceAnswer<BlogUserDto> GetUserByName(string userName);
    }
}