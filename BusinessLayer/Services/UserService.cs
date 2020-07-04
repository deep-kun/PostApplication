using DataAccessLayer.Abstraction;
using BusinessLayer.Model;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLayer.Abstraction
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

            if (dataBaseUser is null)
            {
                return null;
            }

            return new User { 
                Login = login,
                Name = dataBaseUser.Name,
                Role = dataBaseUser.Role,
                UserId = dataBaseUser.UserId };
        }

        public User GetUserByLoginPassword(string login, string password)
        {
            var dataBaseUser = userRepository.GetUserByLoginPassword(login, ComputeSha256Hash(password));

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
                PasswordHash = ComputeSha256Hash(user.Password),
                Name = user.Name,
                Role = user.Role
            });
        }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
