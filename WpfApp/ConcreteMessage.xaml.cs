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
    /// Interaction logic for ConcreteMessage.xaml
    /// </summary>
    public partial class ConcreteMessage : Window
    {
        public ConcreteMessage(ConcreteMessageView cm)
        {
            InitializeComponent();
            BoxForAuthor.Text = cm.Author;
            BoxForBody.Text = cm.Body;
            BoxForDate.Text = cm.Date.ToString();
            BoxForSubject.Text = cm.Subject;
        }
    }
}
