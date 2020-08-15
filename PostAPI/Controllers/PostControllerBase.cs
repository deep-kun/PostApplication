using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace PostAPI.Controllers
{
    public abstract class PostControllerBase : Controller
    {
        protected int GetUserId()
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                int id = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
