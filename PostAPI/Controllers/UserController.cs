using AutoMapper;
using BusinessLayer.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostAPI.Model;
using PostAPI.Model.Mapping;
using System.Collections.Generic;

namespace PostAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class UserController : PostControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IUserService userService)
        {
            this.userService = userService;
            this.mapper = new MapperConfiguration(t => t.AddProfile<ApiMappingProfile>()).CreateMapper();
        }

        [HttpGet]
        public IEnumerable<UserDto> Get()
        {
            var users = this.userService.GetAll();
            return this.mapper.Map<IEnumerable<UserDto>>(users);
        }

        [HttpGet]
        public IActionResult Authenticate()
        {
            return Ok("you are the best Admin");
        }
    }
}
