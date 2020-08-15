using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLayer.Abstraction;
using BusinessLayer.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PostAPI.Model;

namespace PostAPI.Auth
{
    public class AuthManager : IAuthManager
    {
        readonly JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly IUserService userService;
        private readonly IOptions<AppSettings> options;

        public AuthManager(IUserService userService, IOptions<AppSettings> options)
        {
            this.userService = userService;
            this.options = options;
        }

        public AuthentificationResult Authenticate(string userName, string password)
        {
            var result = new AuthentificationResult();
            User user;
            try
            {
                user = this.userService.GetUserByLoginPassword(userName, password);
                result.User = user;
            }
            catch (PostException)
            {
                result.ErrorMessage = "Invalid user name or password.";
                return result;
            }

            var tkey = Encoding.ASCII.GetBytes(options.Value.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tkey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = this.jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            result.Token = jwtSecurityTokenHandler.WriteToken(token);

            return result;
        }
    }
}
