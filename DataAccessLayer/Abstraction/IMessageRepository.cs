using DataAccessLayer.Model;
using DataAccessLayer.PostService;
using System.Collections.Generic;

namespace DataAccessLayer.Abstraction
{
    public interface IMessageRepository
    {
        MessageBody GetMessageById(int messageId, int userId);
        IList<Message> GetMessagesForUser(int id);
        void RemoveMsg(int id, int userId);
        void SendMessage(SentMessage sentMessage);
        void SetMessageRead(int id, int userId);
    }
}
