using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class SendedMessage
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Receiver { get; set; }
        public int AuthorId { get; set; }
    }
}
