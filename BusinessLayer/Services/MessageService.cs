using AutoMapper;
using BusinessLayer.Abstraction;
using BusinessLayer.Model;
using BusinessLayer.Model.Mapping;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Model;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepository;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public MessageService(IMessageRepository messageRepository, IUserService userService)
        {
            this.messageRepository = messageRepository;
            this.userService = userService;
            this.mapper = new MapperConfiguration(t => t.AddProfile<BusinessMappingProfile>()).CreateMapper();
        }

        public Message GetMessageById(int messageId, int userId)
        {
            var dbMessage = this.messageRepository.GetMessageById(messageId, userId);
            messageRepository.SetMessageRead(messageId, userId);

            return this.mapper.Map<Message>(dbMessage);
        }

        public IEnumerable<Message> GetMessagesForUser(int userId)
        {
            var dataFromDb = this.messageRepository.GetMessagesForUser(userId);

            return this.mapper.Map<IEnumerable<Message>>(dataFromDb);
        }

        public void DeleteMessage(int messageId, int userId)
        {
            this.messageRepository.RemoveMessage(messageId, userId);
        }

        public void SendMessage(SendMessageCommand messageCommand)
        {
            if(!IsValid(messageCommand, out var error))
            {
                throw new PostException(error);
            }

            var mappedCommand = this.mapper.Map<DataAccessLayer.Model.SendMessageCommandDb>(messageCommand);

            this.messageRepository.SendMessage(mappedCommand);
        }

        private bool IsValid(SendMessageCommand sentMessage, out string errors)
        {
            var valid = true;
            errors = "";
            var receiverFromDb = this.userService.GetUserByLogin(sentMessage.Receiver.Login);

            if (receiverFromDb == null)
            {
                errors = $"User {sentMessage.Receiver} not found.";
                return false;
            }

            sentMessage.Receiver = receiverFromDb;

            return valid;
        }
    }
}
