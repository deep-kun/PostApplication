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
        public void Post([FromBody]string value)
        {
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
