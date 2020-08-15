using BusinessLayer.Model;
using System.Collections.Generic;

namespace BusinessLayer.Abstraction
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetUserByLogin(string login);
        User GetUserByLoginPassword(string login, string password);
        int RegisterUser(User user);
    }
}
