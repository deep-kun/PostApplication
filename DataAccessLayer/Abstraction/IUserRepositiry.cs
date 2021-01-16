using DataAccessLayer.PostService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstraction
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetUserByLogin(string login);
        User GetUserByLoginPassword(string login, string password);
        int AddUser(User u);
        Task UpdateUserAsync(User user);
    }
}