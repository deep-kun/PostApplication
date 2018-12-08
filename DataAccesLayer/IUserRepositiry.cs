using System.Collections.Generic;
using DataAccesLayer.Model;

namespace DataAccesLayer
{
    public interface IUserRepositiry
    {
        bool CheckUser(string nick);
        MessageBody GetMessageById(int id);
        List<Message> GetMessagesForUser(int id);
        int GetUserByLoginPassword(string login, string password);
        int RegisterUser(User u);
        void RemoveMsg(int id);
        void SendMsg(SendedMessage sendedMessage);
        void SetMessageRead(int id);
    }
}