using DataAccessLayer.PostService;
using System.Collections.Generic;

namespace DataAccessLayer.Abstraction
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetUserByLogin(string login);
        User GetUserByLoginPassword(string login, string password);
        int RegisterUser(User u);
    }
}