﻿using BusinessLayer.Abstraction;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostAPI.Auth;
using PostAPI.Model;

namespace PostAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager authManager;
        private readonly IUserService userService;

        public AuthController(IAuthManager authManager, IUserService userService)
        {
            this.authManager = authManager;
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userInput)
        {
            var authentificationResult = this.authManager.Authenticate(userInput.Login, userInput.Password);

            return authentificationResult.IsSuccess
                  ? Ok(new { authentificationResult.Token })
                  : (IActionResult)BadRequest(new BadRequestResponseDto { ErrorMessage = authentificationResult.ErrorMessage });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("reg")]
        public IActionResult RegActionResult([FromBody]UserDto userView)
        {
            try
            {
                var u = new User
                {
                    Login = userView.Login,
                    Name = userView.Name,
                    Password = userView.Password,
                    Role = 1
                };

                this.userService.RegisterUser(u);

                var authentificationResult = this.authManager.Authenticate(userView.Login, userView.Password);

                return Ok(new { authentificationResult.Token });
            }
            catch (PostException ex)
            {
                return BadRequest(new BadRequestResponseDto { ErrorMessage = ex.Message });
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult Authenticate()
        {
            return Ok("you are the best");
        }
    }
}
