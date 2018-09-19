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
    /// Interaction logic for AddEventView.xaml
    /// </summary>
    public partial class AddEventView : Window
    {
        public AddEventViewModel addEventVM;
        public AddEventView(string id, int day)
        {
            InitializeComponent();
            this.DataContext = addEventVM = new AddEventViewModel(id, day);
            this.gridEvent.ItemsSource = addEventVM.lstEvent;
            this.cbEventType.ItemsSource = addEventVM.LstEventType;
            this.cbPriority.ItemsSource = addEventVM.LstEventPriority;
        }
    }
}
