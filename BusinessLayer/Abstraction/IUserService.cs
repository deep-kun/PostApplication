using BusinessLayer.Model;

namespace BusinessLayer.Abstraction
{
    public interface IUserService
    {
        User GetUserByLogin(string login);
        User GetUserByLoginPassword(string login, string password);
        int RegisterUser(User user);
    }
}
