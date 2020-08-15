using BusinessLayer.Model;
using System.Collections.Generic;

namespace BusinessLayer.Abstraction
{
    public interface IMessageService
    {
        void DeleteMessage(int messageId, int userId);
        Message GetMessageById(int messageId, int userId);
        IEnumerable<Message> GetMessagesForUser(int userId);
        void SendMessage(SendMessageCommand sendMessageCommand);
    }
}
