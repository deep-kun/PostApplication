using DataAccessLayer.Model;

namespace PostAPI.Auth
{
    public interface IAuthService
    {
        User Authenticate(string userName, string password);
    }
}