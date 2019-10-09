using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Interfaces;


namespace bizapps_test.MVC.Global.Auth
{
    /// <summary>
    /// Реализация интерфейса для идентификации пользователя
    /// </summary>
    public class UserIndentity : IIdentity, IUserProvider
    {
        /// <summary>
        /// Текщий пользователь
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Тип класса для пользователя
        /// </summary>
        public string AuthenticationType
        {
            get
            {
                return typeof(User).ToString();
            }
        }

        /// <summary>
        /// Авторизован или нет
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return User != null;
            }
        }

        /// <summary>
        /// Имя пользователя (уникальное) [у нас это счас Email]
        /// </summary>
        public string Name
        {
            get
            {
                if (User != null)
                {
                    return User.Login;
                }
                //иначе аноним
                return "anonym";
            }
        }

        /// <summary>
        /// Инициализация по имени
        /// </summary>
        /// <param name="login">имя пользователя [email]</param>
        public void Init(string login, IBlogUserService blogUserService)
        {
            if (!string.IsNullOrEmpty(login))
            {
                BlogUserDto userDto = blogUserService.GetBlogUserByName(login);
                User = new User(userDto.UserName, userDto.UserPassword, true);
            }
        }
    }
}