using System.Collections.Generic;
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
    public class MailController : PostControllerBase
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
            var currentUserId = this.GetUserId();
            return messageRepository.GetMessagesForUser(currentUserId);
        }

        [Route("{id}")]
        [HttpGet]
        public MessageBody Get(int id)
        {
            var currentUserId = this.GetUserId();
            var msg = messageRepository.GetMessageById(id, currentUserId);

            messageRepository.SetMessageRead(id, currentUserId);

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

            var currentUserId = this.GetUserId();

            smv.AuthorId = currentUserId;
            messageRepository.SendMessage(smv);
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var currentUserId = this.GetUserId();

            messageRepository.RemoveMsg(id, currentUserId);
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
