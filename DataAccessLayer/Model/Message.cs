using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class Message
    {
        public int MessageId { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public bool IsRead { get; set; }
        public bool IsStared { get; set; }
        public string Author { get; set; }
    }
}
