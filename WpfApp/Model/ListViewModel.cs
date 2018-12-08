using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Model
{
    class ListViewModel
    {
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
        public string Author { get; set; }
        internal int MessageId { get; set; }
    }
}
