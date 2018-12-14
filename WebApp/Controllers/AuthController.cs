using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
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
        [Route("ami")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                int uid = Int32.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                return Ok();
            }
            catch (Exception e)
            {
                return Unauthorized();
            }
        }

        // GET: api/Auth/5
        [Route("{id}")]
        public string Get(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var uid = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            return "value";
        }

        [Route("singout")]
        [HttpGet]
        public void SingOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
        }

        [Route("log")]
        [HttpPost]
        public IHttpActionResult LoActionResult(LogInModel model)
        {
            if (!ModelState.IsValid)
            {
                var stsBuilder = new StringBuilder();
                foreach (var err in ModelState.Values)
                {
                    stsBuilder.Append(err.Errors.FirstOrDefault().ErrorMessage);
                    stsBuilder.Append(Environment.NewLine);
                }
                return BadRequest(stsBuilder.ToString());
            }

            int id = ur.GetUserByLoginPassword(model.Login, model.Password);
            if (id >= 1)
            {
                var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, model.Login),
                        new Claim(ClaimTypes.NameIdentifier, id.ToString())
                    },
                    "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                return Ok();

            }

            // user authN failed
            return BadRequest("Wrong creadentials");
        }

        // POST: api/Auth
        [Route("reg")]
        [HttpPost]
        public IHttpActionResult RegActionResult(UserView userView)
        {
            if (!ModelState.IsValid)
            {
                var stsBuilder = new StringBuilder();
                foreach (var err in ModelState.Values)
                {
                    stsBuilder.Append(err.Errors.FirstOrDefault().ErrorMessage);
                    stsBuilder.Append(Environment.NewLine);
                }
                return BadRequest(stsBuilder.ToString());
            }
            var u = new User
            {
                Login = userView.Login,
                Name = userView.Name,
                Password = userView.Password,
                Role = 1
            };
            ur.RegisterUser(u);
            return Ok();
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
