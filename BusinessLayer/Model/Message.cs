using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Model
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
    }
}
