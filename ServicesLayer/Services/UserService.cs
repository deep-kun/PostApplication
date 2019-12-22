using DataAccessLayer.Abstraction;
using ServicesLayer.Model;

namespace ServicesLayer.Abstraction
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User GetUserByLogin(string login)
        {
            var dataBaseUser = userRepository.GetUserByLogin(login);
            return new User { Login = login,
                Name = dataBaseUser.Name,
                Role = dataBaseUser.Role,
                UserId = dataBaseUser.UserId };
        }

        public User GetUserByLoginPassword(string login, string password)
        {
            var dataBaseUser = userRepository.GetUserByLoginPassword(login, password);
            if (dataBaseUser is null)
            {
                throw new NotFoundExeption(login);
            }

            return new User
            {
                Login = login,
                Name = dataBaseUser.Name,
                Role = dataBaseUser.Role,
                UserId = dataBaseUser.UserId
            };
        }

        public int RegisterUser(User user)
        {
            var dataBaseUser = userRepository.GetUserByLogin(user.Login);
            if (dataBaseUser != null)
            {
                throw new AlredyExistsException(user.Login);
            }

            return userRepository.RegisterUser(new DataAccessLayer.Model.User
            {
                Login = user.Login,
                Password = user.Password,
                Name = user.Name,
                Role = user.Role
            });
        }
    }
}
