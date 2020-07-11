using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.PostService
{
    public class SentMessage
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public int ReceiverId { get; set; }
        public string Receiver { get; set; }
        public int AuthorId { get; set; }
    }
}
