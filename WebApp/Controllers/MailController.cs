using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccesLayer;
using DataAccesLayer.Model;
using System.Security.Claims;
using WebApp.Models;

namespace WebApp.Controllers
{   [Authorize]
    public class MailController : Controller
    {
        UserRepositiry userRepositiry = new UserRepositiry();
        public ActionResult Index()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            int id = Int32.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            return View(userRepositiry.GetMessagesForUser(id));
        }

        public ActionResult Delete(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            int uid = Int32.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            userRepositiry.RemoveMsg(id);
            return View("Index", userRepositiry.GetMessagesForUser(uid));
        }

        public ActionResult Read(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            int uid = Int32.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            var msg = userRepositiry.GetMessageById(id);
            if (msg.ReciverId!=uid)
            {
                return View("Error");
            }
            userRepositiry.SetMessageRead(id);
            return View(msg);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SendedMessageView smv)
        {
            if (!ModelState.IsValid)
            {
                return View(smv);
            }

            var claimsIdentity = User.Identity as ClaimsIdentity;
            int id = Int32.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            var sm = new SendedMessage
            {
                Body = smv.Body,
                Receiver = smv.Receiver,
                Subject = smv.Subject
            };
            sm.AuthorId = id;
            userRepositiry.SendMsg(sm);
            return View("Index",userRepositiry.GetMessagesForUser(id));
        }


    }
}