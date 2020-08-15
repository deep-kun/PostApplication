using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Model
{
    public class SendMessageCommand
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public User Receiver { get; set; }

        public int AuthorId { get; set; }
    }
}
