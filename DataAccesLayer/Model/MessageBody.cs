using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Model
{
    public class MessageBody
    {
        public int MessageId { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public int ReciverId { get; set; }  
    }
}
