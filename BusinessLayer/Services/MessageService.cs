using AutoMapper;
using BusinessLayer.Abstraction;
using BusinessLayer.Model;
using BusinessLayer.Model.Mapping;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{

    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepository;
        private readonly IMapper mapper;

        public MessageService(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
            this.mapper = new MapperConfiguration(t => t.AddProfile<MessageMappingProfile>()).CreateMapper();
        }

        public Message GetMessageById(int messageId, int userId)
        {
            var dbMessage = this.messageRepository.GetMessageById(messageId, userId);
            messageRepository.SetMessageRead(messageId, userId);

            return this.mapper.Map<Message>(dbMessage);
        }
    }
}
