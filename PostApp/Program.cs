using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesLayer;
using DataAccesLayer.Model;

namespace PostApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var rep = new UserRepositiry();
            var u = rep.GetUserByLoginPassword("deep","123");
            //var l = rep.GetMessagesForUser(3);
            //var m = rep.GetMessageById(1);
            //rep.SetMessageRead(2);

            var sm = new SendedMessage();
            sm.AuthorId = 1;
            sm.Body = "msg  c# from 1 to 2";
            sm.Receiver = "vanya";
            sm.Subject = "new topic";
            //rep.SendMsg(sm);
            Console.WriteLine();
        }
    }
}
