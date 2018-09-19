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
    /// Interaction logic for UserEventView.xaml
    /// </summary>
    public partial class UserEventView : Window
    {
        private MainWindowViewModel mainWindowVM;
        public UserEventView(User user)
        {
            InitializeComponent();
            mainWindowVM = new MainWindowViewModel(user);
            this.DataContext = mainWindowVM;
            this.gridEvents.ItemsSource = mainWindowVM.lstEvents;
        }
    }
}
