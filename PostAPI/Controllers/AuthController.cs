using AutoMapper;
using BusinessLayer.Abstraction;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostAPI.Auth;
using PostAPI.Model;
using PostAPI.Model.Mapping;

namespace PostAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : PostControllerBase
    {
        private readonly IAuthManager authManager;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public AuthController(IAuthManager authManager, IUserService userService)
        {
            this.authManager = authManager;
            this.userService = userService;
            this.mapper = new MapperConfiguration(t => t.AddProfile<ApiMappingProfile>()).CreateMapper();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody] UserDto userInput)
        {
            var authentificationResult = this.authManager.Authenticate(userInput.Login, userInput.Password);

            return authentificationResult.IsSuccess
                  ? Ok(new AuthauthenticationResponseDto { Token = authentificationResult.Token, User = this.mapper.Map<UserDto>(authentificationResult.User) })
                  : (IActionResult)BadRequest(new BadRequestResponseDto { ErrorMessage = authentificationResult.ErrorMessage });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("reg")]
        public IActionResult RegActionResult([FromBody] UserDto userView)
        {
            var u = new User
            {
                Login = userView.Login,
                Name = userView.Name,
                Password = userView.Password,
                RoleId = 1
            };

            try
            {
                this.userService.RegisterUser(u);

                var authentificationResult = this.authManager.Authenticate(userView.Login, userView.Password);

                return Ok(new AuthauthenticationResponseDto { Token = authentificationResult.Token, User = this.mapper.Map<UserDto>(authentificationResult.User) });
            }
            catch (PostException ex)
            {
                return BadRequest(new BadRequestResponseDto { ErrorMessage = ex.Message });
            }
        }
    }
}
