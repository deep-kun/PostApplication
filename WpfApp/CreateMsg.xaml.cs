using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp.Model;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for CreateMsg.xaml
    /// </summary>
    public partial class CreateMsg : Window
    {
        readonly int cUrentId;
        PostServiceEntities context = new PostServiceEntities();
        public CreateMsg(int id)
        {
            cUrentId = id;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var msg = new Message();
            msg.AuthorId = cUrentId;
            msg.Subject = BoxForSubject.Text;
            msg.Body = BoxForBody.Text;
            msg.Date = DateTime.Now;
            context.Messages.Add(msg);
            context.SaveChanges();
            int idmsg = msg.MessageId;

                    
            var reciver = context.Users.Where(t => t.UserLogin == BoxForReciver.Text).SingleOrDefault();

            if (reciver==null)
            {
                ValidationR.Content = "REciver dont exist";
                return;
            }
            ValidationR.Content = "";
            var umm = new Users_Messages_Mapped
            {
                IsRead = false,
                IsStarred = false,
                MessageId = idmsg,
                UserId = reciver.UserId,
                PlaceHolderId = 1                              
            };
            context.Users_Messages_Mapped.Add(umm);
            context.SaveChanges();
            MessageBox.Show("Sended");
            this.Close();
        }
    }
}
