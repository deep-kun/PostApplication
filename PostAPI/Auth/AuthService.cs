using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace PostAPI.Auth
{
    public class AuthService : IAuthService
    {
        readonly JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly IUserRepository userRepository;
        private readonly IOptions<AppSettings> options;

        public AuthService(IUserRepository userRepository, IOptions<AppSettings> options)
        {
            this.userRepository = userRepository;
            this.options = options;
        }

        public User Authenticate(string userName, string password)
        {
            var user = this.userRepository.GetUserByLoginPassword(userName, password);
            if (user == null)
            {
                return null;
            }

            var tkey = Encoding.ASCII.GetBytes(options.Value.JwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tkey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = this.jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            user.Token = jwtSecurityTokenHandler.WriteToken(token);

            return user;
        }
    }
}
