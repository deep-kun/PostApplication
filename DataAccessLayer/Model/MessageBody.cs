using System;

namespace DataAccessLayer.Model
{
    public class MessageBody
    {
        public int MessageId { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
    }
}
