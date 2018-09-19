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
    /// Interaction logic for DiaryReadView.xaml
    /// </summary>
    public partial class DiaryReadView : UserControl
    {
        DiaryReadViewModel DRVM;
        public User mainUser;
        public DiaryReadView()
        {
            InitializeComponent();
            this.DataContext = DRVM = new DiaryReadViewModel();
        }
        public void DiaryShow()
        {
            using (var db = new MyContext())
            {
                var diaries = (from n in db.Diaries
                               where n.UserID == mainUser.UserID
                               && n.Active == true
                               select n).ToList();
                lwListDiary.ItemsSource = diaries;
            }
        }
    }
}
