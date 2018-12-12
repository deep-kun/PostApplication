using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using DataAccesLayer;
using DataAccesLayer.Model;
using WebApp.Models;

namespace WebApp.Controllers
{
    [RoutePrefix("api/auth")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        private IUserRepositiry ur = new UserRepositiry();

        // GET: api/Auth
        [Route("")]
        public IEnumerable<string> Get()
        {
            var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, "Name"),
                    new Claim(ClaimTypes.NameIdentifier, "1")},
                    "ApplicationCookie");

            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignIn(identity);

            var r = HttpContext.Current.Request;

            return new string[] { "value1", "value2" };
        }

        // GET: api/Auth/5
        [Route("{id}")]
        public string Get(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var uid = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            return "value";
        }

        // POST: api/Auth
        [Route("")]
        [HttpPost]
        public void Post(UserView userView)
        {
            if (!ModelState.IsValid)
            {
                //return View(userView);
            }
            var u = new User
            {
                Login = userView.Login,
                Name = userView.Name,
                Password = userView.Password,
                Role = 1
            };
            ur.RegisterUser(u);
            //ViewBag.Status = "You have been registered";
            //return RedirectToAction("LogIn");
        }

        // PUT: api/Auth/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Auth/5
        public void Delete(int id)
        {
        }
    }
}
