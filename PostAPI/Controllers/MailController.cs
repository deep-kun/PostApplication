using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using DataAccessLayer;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Model;
using DataAccessLayer.PostService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostAPI.Model;

namespace PostAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class MailController : Controller
    {
        private readonly IMessageRepository messageRepository;
        private readonly IUserRepository userRepository;
        public MailController(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            this.messageRepository = messageRepository;
            this.userRepository = userRepository;
        }

        // GET: api/Mail
        [Route("")]
        [HttpGet]
        public IEnumerable<Message> Get()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            int id = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            return messageRepository.GetMessagesForUser(id);
        }

        // GET: api/Mail/5
        [Route("{id}")]
        [HttpGet]
        public MessageBody Get(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            int uid = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            var msg = messageRepository.GetMessageById(id);
            if (msg.ReceiverId != uid)
            {
                throw new NullReferenceException();
                ///return View("Error");
            }
            messageRepository.SetMessageRead(id);
            return msg;
        }

        // POST: api/Mail
        [Route("send")]
        [HttpPost]
        public IActionResult Post([FromBody]SentMessage smv)
        {
            if (!IsValid(smv, out var erros))
            {
                return BadRequest(new BadRequestResponseDto { ErrorMessage = erros });
            }

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var id = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
            smv.AuthorId = id;
            messageRepository.SendMessage(smv);
            return Ok();
        }

        // DELETE: api/Mail/5
        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            messageRepository.RemoveMsg(id);
            return Ok();
        }

        private bool IsValid(SentMessage sentMessage, out string errors)
        {
            var valid = true;
            errors = "";
            var user = this.userRepository.GetUserByLogin(sentMessage.Receiver);

            if (user == null)
            {
                errors = $"User {sentMessage.Receiver} not found.";
                return false;
            }

            sentMessage.ReceiverId = user.UserId;

            return valid;
        }
    }
}
