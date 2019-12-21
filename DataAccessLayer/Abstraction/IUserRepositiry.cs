using DataAccessLayer.Model;

namespace DataAccessLayer.Abstraction
{
    public interface IUserRepository
    {
        bool CheckUserExsits(string login);
        User GetUserByLoginPassword(string login, string password);
        int RegisterUser(User u);
    }
}