using DataAccessLayer;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostAPI.Auth;
using System.Text;

namespace PostAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IUserRepository userRepositiry;

        public AuthController(IAuthService authService, IUserRepository userRepositiry)
        {
            this.authService = authService;
            this.userRepositiry = userRepositiry;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody]User userInput)
        {
            var user = this.authService.Authenticate(userInput.Login, userInput.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("reg")]
        public IActionResult RegActionResult([FromBody]User userView)
        {
            if (!IsValid(userView, out var error))
            {
                return BadRequest(error);
            }
            var u = new User
            {
                Login = userView.Login,
                Name = userView.Name,
                Password = userView.Password,
                Role = 1
            };
            userRepositiry.RegisterUser(u);
            var user = this.authService.Authenticate(userView.Login, userView.Password);
            return Ok(user);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Authenticate()
        {
            return Ok("you are the best");
        }

        private bool IsValid(User user, out string errors)
        {
            var valid = true;
            errors = "";
            if (this.userRepositiry.CheckUserExsits(user.Login))
            {
                errors = $"{user.Login} is already taken.";
                return false;
            }

            return valid;
        }
    }
}
