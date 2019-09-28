using System.Collections.Generic;
using DataAccessLayer.Model;

namespace DataAccessLayer
{
    public interface IUserRepository
    {
        bool CheckUser(string nick);
        MessageBody GetMessageById(int id);
        List<Message> GetMessagesForUser(int id);
        User GetUserByLoginPassword(string login, string password);
        int RegisterUser(User u);
        void RemoveMsg(int id);
        void SendMsg(SendedMessage sentMessage);
        void SetMessageRead(int id);
    }
}