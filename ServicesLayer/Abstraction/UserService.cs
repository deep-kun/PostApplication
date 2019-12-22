using ServicesLayer.Model;

namespace ServicesLayer.Abstraction
{
    public interface IUserService
    {
        User GetUserByLogin(string login);
        User GetUserByLoginPassword(string login, string password);
        int RegisterUser(User user);
    }
}
