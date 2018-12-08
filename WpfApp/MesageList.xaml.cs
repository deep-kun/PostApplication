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
    /// Interaction logic for MesageList.xaml
    /// </summary>
    public partial class MesageList : Window
    {
        PostServiceEntities contex = new PostServiceEntities();
        readonly int curentUserId;
        public MesageList(int id)
        {
            InitializeComponent();
            curentUserId = id;
            Inicialize();
        }
        private void Inicialize()
        {

            var fromdb = contex.Users_Messages_Mapped.Where(t => t.UserId == curentUserId).ToList();

            var list = new List<ListViewModel>();
            var msgItem = new ListViewModel();
            foreach (var item in fromdb)
            {
                msgItem.Author = item.Message.User.UserName;
                msgItem.Date = item.Message.Date;
                msgItem.IsRead = item.IsRead;
                msgItem.Subject = item.Message.Subject;
                msgItem.MessageId = item.MessageId;
                list.Add(msgItem);
                msgItem = new ListViewModel();
            }
            //MesageGrid.Columns[3].Visibility = Visibility.Hidden;
            MesageGrid.ItemsSource = list;
            MesageGrid.IsReadOnly = true;
        }

        private void Row_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ListViewModel m = (ListViewModel)MesageGrid.SelectedItems[0];
                //MessageBox.Show(m.MessageId.ToString());
                int id = m.MessageId;

                var mp = contex.Messages.Where(i=>i.MessageId==id).Single();

                var cmv = new ConcreteMessageView();
                cmv.Body = mp.Body = mp.Body;
                cmv.Date = mp.Date;
                cmv.Subject = mp.Subject;
                cmv.Author = contex.Users.Where(t => t.UserId == mp.AuthorId).Single().UserName;
                var form = new ConcreteMessage(cmv);
                var read = contex.Users_Messages_Mapped.Where(t => t.MessageId == id).Single();
                read.IsRead = true;
                contex.SaveChanges();
                form.Show();
                this.Inicialize();
            }
            catch (Exception)
            {
            }
        }

        private void Button_Click_Refresh(object sender, RoutedEventArgs e)
        {
            this.Inicialize();
            MessageBox.Show("refreshed");
        }

        private void Button_Click_Compose(object sender, RoutedEventArgs e)
        {
            CreateMsg createMsg = new CreateMsg(curentUserId);
            createMsg.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MesageGrid.SelectedItems[0] == null)
                {
                    MessageBox.Show("Null selected");
                    return;
                }
            }
            catch (Exception)
            {
                return;
            }
            
            ListViewModel m = (ListViewModel)MesageGrid.SelectedItems[0];
            if (m!=null)
            {
                var entity = contex.Users_Messages_Mapped.Where(t=>t.MessageId==m.MessageId).SingleOrDefault();
                contex.Users_Messages_Mapped.Remove(entity);
                contex.SaveChanges();
                this.Inicialize();
            }
        }
    }
}
