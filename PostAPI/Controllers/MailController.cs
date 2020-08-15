using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Abstraction;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostAPI.Model;
using PostAPI.Model.Mapping;

namespace PostAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class MailController : PostControllerBase
    {
        private readonly IMessageService messageService;
        private readonly IMapper mapper;

        public MailController(IMessageService messageService)
        {
            this.messageService = messageService;
            this.mapper = new MapperConfiguration(t => t.AddProfile<ApiMappingProfile>()).CreateMapper();
        }

        // GET: api/Mail
        [Route("")]
        [HttpGet]
        public IEnumerable<MessageDto> Get()
        {
            var currentUserId = this.GetUserId();

            var messages = messageService.GetMessagesForUser(currentUserId);

            return this.mapper.Map<IEnumerable<MessageDto>>(messages);
        }

        [Route("{id}")]
        [HttpGet]
        public MessageDto Get(int id)
        {
            var currentUserId = this.GetUserId();
            var returnMsg = this.messageService.GetMessageById(id, currentUserId);

            return this.mapper.Map<MessageDto>(returnMsg);
        }

        // POST: api/Mail
        [Route("send")]
        [HttpPost]
        public IActionResult Post([FromBody]SendMessageCommandDto message)
        {
            var command = this.mapper.Map<SendMessageCommand>(message);
            command.AuthorId = this.GetUserId();
            try
            {
                this.messageService.SendMessage(command);
            }
            catch (PostException ex)
            {
                return BadRequest(new BadRequestResponseDto { ErrorMessage = ex.Message });
            }

            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var currentUserId = this.GetUserId();

            this.messageService.DeleteMessage(id, currentUserId);
            return Ok();
        }
    }
}
