using DataAccessLayer.Abstraction;
using BusinessLayer.Model;
using System.Security.Cryptography;
using System.Text;
using BusinessLayer.Abstraction;
using AutoMapper;
using BusinessLayer.Model.Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.mapper = new MapperConfiguration(t => t.AddProfile<BusinessMappingProfile>()).CreateMapper();
        }

        public IEnumerable<User> GetAll()
        {
            var dataBaseUsers = this.userRepository.GetAll();

            return this.mapper.Map<IEnumerable<User>>(dataBaseUsers);
        }

        public User GetUserByLogin(string login)
        {
            var dataBaseUser = this.userRepository.GetUserByLogin(login);

            if (dataBaseUser is null)
            {
                throw new NotFoundExeption(login);
            }

            return this.mapper.Map<User>(dataBaseUser);
        }

        public User GetUserByLoginPassword(string login, string password)
        {
            var dataBaseUser = this.userRepository.GetUserByLoginPassword(login, ComputeSha256Hash(password));

            if (dataBaseUser is null)
            {
                throw new NotFoundExeption(login);
            }

            return this.mapper.Map<User>(dataBaseUser);
        }

        public int RegisterUser(User user)
        {
            var dataBaseUser = this.userRepository.GetUserByLogin(user.Login);
            if (dataBaseUser != null)
            {
                throw new AlredyExistsException(user.Login);
            }
          
            return this.userRepository.AddUser(new DataAccessLayer.PostService.User
            {
                UserLogin= user.Login,
                PasswordHash = ComputeSha256Hash(user.Password),
                UserName = user.Name,
                RoleId = user.RoleId
            });
        }

        public async Task UpdateUser(User user)
        {
            if (!(await this.IsUserValid(user)))
            {
                throw new InvalidEntityException(user.Login);
            }

            await this.userRepository.UpdateUserAsync(this.mapper.Map<DataAccessLayer.PostService.User>(user));
        }

        private async Task<bool> IsUserValid(User user)
        {
            var roles = await this.roleRepository.GetAllRolesAsync();

            bool isValid = true;

            isValid &= roles.Select(t => t.RoleId).Any(ri => ri == user.RoleId);

            return isValid;
        }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using SHA256 sha256Hash = SHA256.Create();
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
