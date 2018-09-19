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
using System.Windows.Shapes;

namespace MyAwesomeDiary
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private LoginViewModel loginVM;
        public LoginView()
        {
            InitializeComponent();
            loginVM = new LoginViewModel();
            this.DataContext = uCSignIn.DataContext = uCSignUp.DataContext = loginVM;
            this.uCSignUp.cbNation.ItemsSource = loginVM.LstNation;                        
        }
    }
}
