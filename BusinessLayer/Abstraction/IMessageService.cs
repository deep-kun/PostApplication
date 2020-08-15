using BusinessLayer.Model;

namespace BusinessLayer.Abstraction
{
    public interface IMessageService
    {
        Message GetMessageById(int messageId, int userId);
    }
}
