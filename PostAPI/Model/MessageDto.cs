using System;

namespace PostAPI.Model
{
    public class MessageDto
    {
        public int MessageId { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
    }
}
