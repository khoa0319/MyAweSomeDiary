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
    /// Interaction logic for CalendarMonthView.xaml
    /// </summary>
    public partial class CalendarMonthView : UserControl
    {
        public CalendarViewModel calendarVM;
        public CalendarMonthView()
        {
            InitializeComponent();
            this.DataContext = calendarVM = new CalendarViewModel();
            this.cbMonth.ItemsSource = calendarVM.ListMonth;
        }

        
    }
}
