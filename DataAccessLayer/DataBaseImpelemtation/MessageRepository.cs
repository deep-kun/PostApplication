using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Model;
using DataAccessLayer.PostService;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DataBaseImpelemtation
{
    public class MessageRepository : IMessageRepository
    {
        private readonly PostServiceContext postServiceContext;

        public MessageRepository(PostServiceContext postServiceContext)
        {
            this.postServiceContext = postServiceContext;
        }

        public IList<Message> GetMessagesForUser(int id)
        {
            return this.postServiceContext.UsersMessagesMappeds.Where(mp => mp.UserId == id).Select(umm => umm.Message).ToList();
        }

        public MessageBody GetMessageById(int messageId,int userId)
        {
            var loadedMessage = this.postServiceContext.Messages.Where(m => m.MessageId == messageId)
                .Join(
                    this.postServiceContext.UsersMessagesMappeds,
                    o => o.MessageId,
                    i => i.MessageId,
                    (o, i) => o
                    )
                .Include(t => t.Author)
                .Include(um => um.UsersMessagesMappeds)
                .AsNoTracking()
                .FirstOrDefault();

            if (loadedMessage is null)
            {
                return null;    
            }

            var result = new MessageBody()
            {
                Author = loadedMessage.Author.UserName,
                Body = loadedMessage.Body,
                Date = loadedMessage.SentDate.Date,
                MessageId = loadedMessage.MessageId,
                Subject = loadedMessage.Subject,
            };

            return result;
        }

        public void SetMessageRead(int messageId, int userId)
        {
            var message = this.postServiceContext.UsersMessagesMappeds.FirstOrDefault(t => t.MessageId == messageId && t.UserId == userId);

            message.IsRead = true;

            this.postServiceContext.SaveChanges();
        }

        public void RemoveMsg(int messageId, int userId)
        {
            var message = this.postServiceContext.UsersMessagesMappeds.FirstOrDefault(t => t.MessageId == messageId && t.UserId == userId);

            this.postServiceContext.UsersMessagesMappeds.Remove(message);
            this.postServiceContext.SaveChanges();
        }

        public void SendMessage(SentMessage sentMessage)
        {
            var newMessage = new Message
            {
                AuthorId = sentMessage.AuthorId,
                Body = sentMessage.Body,
                SentDate = DateTime.Now,
                Subject = sentMessage.Subject,
            };

            this.postServiceContext.Messages.Add(newMessage);
            this.postServiceContext.SaveChanges();
            
            var mappedMessage = new UsersMessagesMapped
            {
                IsRead = false,
                IsStarred = false,
                MessageId = newMessage.MessageId,
                PlaceHolderId = 1,
                UserId = sentMessage.ReceiverId,
            };

            this.postServiceContext.UsersMessagesMappeds.Add(mappedMessage);
            this.postServiceContext.SaveChanges();
        }
    }
}
