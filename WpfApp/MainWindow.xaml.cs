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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp.Model;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PostServiceEntities contex = new PostServiceEntities();
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            MesageList mesageList = new MesageList(1);
            mesageList.Show();
            this.Close();
            string login = Login.Text;
            string password = Password.Text;
            var user = contex.Users.Where(l => l.UserLogin == login).Where(p=>p.Password==password).SingleOrDefault();
            if (user==null)
            {
                //MessageBox.Show("all bad");
                mesageList.Show();
                return;
            }
            MessageBox.Show("All ok");
        }
    }
}
