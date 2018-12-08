using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using System.Security.Claims;
using DataAccesLayer;
using DataAccesLayer.Model;

namespace WebApp.Controllers.old
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        UserRepositiry ur = new UserRepositiry();
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult LogIn(LogInModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            int id = ur.GetUserByLoginPassword(model.Login,model.Password);
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

                return RedirectToAction("Index", "Mail");

            }

            // user authN failed
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("index", "Mail");
        }
        public ActionResult Regisiter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Regisiter(UserView userView)
        {
            if (!ModelState.IsValid)
            {
                return View(userView);
            }
            var u = new User
            {
                Login = userView.Login,
                Name = userView.Name,
                Password = userView.Password,
                Role = 1
            };
            ur.RegisterUser(u);
            ViewBag.Status= "You have been registered";
            return RedirectToAction("LogIn");
        }
    }
}