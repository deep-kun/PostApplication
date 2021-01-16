using System;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostAPI.Model.Mapping;

namespace PostAPI.Controllers
{
    public abstract class PostControllerBase : Controller
    {
        protected int GetUserId()
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                int id = int.Parse(claim.Value);

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        protected IMapper GetMapper<T>() where T : Profile, new()
        {
            return new MapperConfiguration(t => t.AddProfile<T>()).CreateMapper();
        }
    }
}
