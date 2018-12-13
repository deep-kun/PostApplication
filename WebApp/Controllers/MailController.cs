using DataAccesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using DataAccesLayer.Model;
using WebApp.Models;

namespace WebApp.Controllers
{
    [System.Web.Http.Authorize]
    [RoutePrefix("api/mail")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MailController : ApiController
    {

        private IUserRepositiry userRepositiry = new UserRepositiry();

        // GET: api/Mail
        [Route("")]
        [HttpGet]
        public IEnumerable<Message> Get()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            int id = Int32.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            return userRepositiry.GetMessagesForUser(id);
        }

        // GET: api/Mail/5
        [Route("{id}")]
        [HttpGet]
        public MessageBody Get(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            int uid = Int32.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            var msg = userRepositiry.GetMessageById(id);
            if (msg.ReciverId != uid)
            {
                throw new NullReferenceException();
                ///return View("Error");
            }
            userRepositiry.SetMessageRead(id);
            return msg;
        }

        // POST: api/Mail
        [Route("/send")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]SendedMessageView smv)
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
            return Ok();
        }

        // DELETE: api/Mail/5
        [Route("")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            int uid = Int32.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            userRepositiry.RemoveMsg(id);
            return Ok();
        }
    }
}
