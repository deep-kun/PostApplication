using AutoMapper;
using BusinessLayer.Abstraction;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostAPI.Model;
using PostAPI.Model.Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            this.mapper = GetMapper<ApiMappingProfile>();
        }

        [HttpPut]
        public async Task Update(UserDto user)
        {
            await this.userService.UpdateUser(this.mapper.Map<User>(user));
        }

        [HttpGet]
        public IEnumerable<UserDto> Get()
        {
            var users = this.userService.GetAll();
            return this.mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}
