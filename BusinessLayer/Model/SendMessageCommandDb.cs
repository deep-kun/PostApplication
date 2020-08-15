using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Model
{
    public class SendMessageCommandDb
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public int AuthorId { get; set; }

        public User Revicer { get; set; }
    }
}
