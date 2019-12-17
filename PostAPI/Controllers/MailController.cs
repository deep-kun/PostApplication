using System;
using System.Collections.Generic;
using System.Security.Claims;
using DataAccessLayer;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace PostAPI.Controllers
{
    [Route("api/[controller]")]
    public class MailController : Controller
    {
        private readonly IUserRepository userRepositiry;
        public MailController(IUserRepository userRepositiry)
        {
            this.userRepositiry = userRepositiry;
        }

        // GET: api/Mail
        [Route("")]
        [HttpGet]
        public IEnumerable<Message> Get()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            int id = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            return userRepositiry.GetMessagesForUser(id);
        }

        // GET: api/Mail/5
        [Route("{id}")]
        [HttpGet]
        public MessageBody Get(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            int uid = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            var msg = userRepositiry.GetMessageById(id);
            if (msg.ReceiverId != uid)
            {
                throw new NullReferenceException();
                ///return View("Error");
            }
            userRepositiry.SetMessageRead(id);
            return msg;
        }

        // POST: api/Mail
        [Route("send")]
        [HttpPost]
        public IActionResult Post([FromBody]SendedMessage smv)
        {
            //if (!ModelState.IsValid)
            //{
            //    var stsBuilder = new StringBuilder();
            //    foreach (var err in ModelState.Values)
            //    {
            //        stsBuilder.Append(err.Errors.FirstOrDefault().ErrorMessage);
            //        stsBuilder.Append(Environment.NewLine);
            //    }
            //    return BadRequest(stsBuilder.ToString());
            //}

            var claimsIdentity = User.Identity as ClaimsIdentity;
            int id = Int32.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            smv.AuthorId = id;
            userRepositiry.SendMsg(smv);
            return Ok();
        }

        // DELETE: api/Mail/5
        [Route("")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            userRepositiry.RemoveMsg(id);
            return Ok();
        }
    }
}
