using DataAccessLayer.Model;

namespace DataAccessLayer.Abstraction
{
    public interface IUserRepository
    {
        User GetUserByLogin(string login);
        User GetUserByLoginPassword(string login, string password);
        int RegisterUser(User u);
    }
}