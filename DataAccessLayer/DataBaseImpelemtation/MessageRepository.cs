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

        public MessageBody GetMessageById(int id)
        {
            var loadedMessage = this.postServiceContext.Messages.Where(m => m.MessageId == id)
                .Include("Author")
                .Include("UsersMessagesMappeds")
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
                ReceiverId = loadedMessage.
            };

            return result;
        }

        public void SetMessageRead(int id)
        {
        }

        public void RemoveMsg(int id)
        {
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
