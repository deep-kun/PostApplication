using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostAPI.Model
{
    public class AuthauthenticationResponseDto
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}
