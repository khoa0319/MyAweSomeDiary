using MyAwesomeDiary.Model;
using MyAwesomeDiary.ViewModel;
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

namespace MyAwesomeDiary
{
    /// <summary>
    /// Interaction logic for UserInforView.xaml
    /// </summary>
    public partial class UserInforView : UserControl
    {
        //public User mainUser;
        private UserInforViewModel UserInfoVM;
        public UserInforView()
        {
            InitializeComponent();
            this.DataContext = UserInfoVM = new UserInforViewModel();
            this.cbNation.ItemsSource = UserInfoVM.Nations;
        }
    }
}
