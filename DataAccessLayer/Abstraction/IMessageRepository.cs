using DataAccessLayer.Model;
using DataAccessLayer.PostService;
using System.Collections.Generic;

namespace DataAccessLayer.Abstraction
{
    public interface IMessageRepository
    {
        MessageBody GetMessageById(int messageId, int userId);
        IList<Message> GetMessagesForUser(int id);
        void RemoveMessage(int id, int userId);
        void SendMessage(SendMessageCommandDb sentMessage);
        void SetMessageRead(int id, int userId);
    }
}
