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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel MainWDVM { get; set; }
        public LoginView LoginViewWindow { get; set; }
        public MainWindow(User user, LoginView lgview)
        {
            InitializeComponent();
            LoginViewWindow = lgview;
            this.DataContext = MainWDVM = new MainWindowViewModel(user);
            this.lbUserName.DataContext = MainWDVM.MainUser;

            // Hiện tâm trạng người dùng
            using (var db = new MyContext())
            {
                string mainUser = user.UserID;
                int day = DateTime.Today.Day;
                int month = DateTime.Today.Month;
                int year = DateTime.Today.Year;


                var emptyDate = db.UserMoods.Where(u => u.UserID == mainUser
                                && u.Date.Day == day
                                && u.Date.Month == month
                                && u.Date.Year == year).SingleOrDefault() == null;
                // Kiểm tra có mood trùng ngày hôm nay không
                // Nếu đã có rồi
                if (emptyDate != true)
                {
                    //MessageBox.Show("Not null");
                    var userEmo = (from n in db.UserMoods
                                   where n.UserID == mainUser
                                   && n.Date.Day == day
                                   && n.Date.Month == month
                                   && n.Date.Year == year
                                   select n).First();
                    if (userEmo != null)
                    {
                        lbEmotion.Content = db.Moods.Find(userEmo.MoodID).Name;
                        imgEmotionIcon.Source = ChangeEmoImg(userEmo.MoodID);
                    }

                }
                // Nếu chưa có 
                else
                {
                    UserMood userMood = new UserMood
                    {
                        UserID = mainUser,
                        Date = DateTime.Today,
                        MoodID = 1
                    };
                    db.UserMoods.Add(userMood);
                    db.SaveChanges();
                    lbEmotion.Content = db.Moods.Find(userMood.MoodID).Name;
                    imgEmotionIcon.Source = ChangeEmoImg(userMood.MoodID);
                }
            }
        }
        public BitmapImage ChangeEmoImg(int num)
        {
            BitmapImage bitmap;
            if (num == 1)
                bitmap = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Image/1.png"));
            else if (num == 2)
                bitmap = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Image/2.png"));
            else if (num == 3)
                bitmap = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Image/3.png"));
            else if (num == 4)
                bitmap = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Image/4.png"));
            else if (num == 5)
                bitmap = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Image/5.png"));
            else if (num == 6)
                bitmap = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Image/6.png"));
            else
                bitmap = new BitmapImage(new Uri(@"pack://application:,,,/Resources/Image/7.png"));
            return bitmap;
        }
    }
}

