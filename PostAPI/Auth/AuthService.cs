﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Model;
using Microsoft.IdentityModel.Tokens;

namespace PostAPI.Auth
{
    public class AuthService : IAuthService
    {
        private static readonly string key = "KEYKEYKEYKEYKEYKEYKEYKEYKEYKEYKEYKEY";

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly IUserRepository userRepository;

        public AuthService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User Authenticate(string userName, string password)
        {
            var user = this.userRepository.GetUserByLoginPassword(userName, password);
            if (user == null)
            {
                return null;
            }

            var tkey = Encoding.ASCII.GetBytes(key);
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
