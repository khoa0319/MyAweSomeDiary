using MyAwesomeDiary.Model;
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
    /// Interaction logic for EmotionView.xaml
    /// </summary>
    public partial class EmotionView : Window
    {
        public EmotionView(User user)
        {
            InitializeComponent();
            using (var db = new MyContext())
            {
                var lstEmo = (from n in db.UserMoods
                              join m in db.Moods on n.MoodID equals m.ID
                              where n.UserID == user.UserID
                              select new
                              {
                                  n.Date,
                                  m.Name
                              }).ToList();
                lwEmotionShow.ItemsSource = lstEmo;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
