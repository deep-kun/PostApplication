using BusinessLayer.Model;
using PostAPI.Model;

namespace PostAPI.Auth
{
    public interface IAuthManager
    {
        AuthentificationResult Authenticate(string userName, string password);
    }
}