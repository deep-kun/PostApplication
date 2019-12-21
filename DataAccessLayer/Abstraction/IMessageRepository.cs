using DataAccessLayer.Model;
using System.Collections.Generic;

namespace DataAccessLayer.Abstraction
{
    public interface IMessageRepository
    {
        MessageBody GetMessageById(int id);
        IList<Message> GetMessagesForUser(int id);
        void RemoveMsg(int id);
        void SendMessage(SendedMessage sentMessage);
        void SetMessageRead(int id);
    }
}
