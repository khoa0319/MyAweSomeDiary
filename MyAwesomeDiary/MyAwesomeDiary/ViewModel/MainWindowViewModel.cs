using MyAwesomeDiary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MyAwesomeDiary.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public List<UserEvent> lstEvents;
        public User MainUser { get; set; }
        public ICommand OpenCalendarEventCommand { get; set; }
        public ICommand SignOutCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand OpenDiaryCommand { get; set; }
        public ICommand OpenTodolistCommand { get; set; }
        public ICommand OpenInformationCommand { get; set; }
        public ICommand LeftCommand { get; set; }
        public ICommand RightCommand { get; set; }
        public ICommand ShowEmotionCommand { get; set; }
        public ICommand OpenComingEventCommand { get; set; }

        public MainWindowViewModel(User user)
        {
            MainUser = user;            
            using (var db = new MyContext())
            {
                lstEvents = db.UserEvents.Where(ue => ue.UserID == MainUser.UserID).ToList();
            }
            //Command
            OpenCalendarEventCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {
                if (p is MainWindow mw)
                {
                    mw.ucCalendar.Visibility = Visibility.Visible;
                    mw.ucDiary.Visibility = Visibility.Hidden;
                    mw.ucTodolist.Visibility = Visibility.Hidden;
                    mw.ucUserInfor.Visibility = Visibility.Hidden;
                    mw.ucCalendar.calendarVM.PopulateCalendar(DateTime.Now.Month, mw.ucCalendar);
                    mw.ucCalendar.cbMonth.SelectedIndex = DateTime.Now.Month - 1;
                }
            });
            OpenDiaryCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {
                if (p is MainWindow mw)
                {
                    mw.ucDiary.Visibility = Visibility.Visible;
                    mw.ucCalendar.Visibility = Visibility.Hidden;
                    mw.ucTodolist.Visibility = Visibility.Hidden;
                    mw.ucUserInfor.Visibility = Visibility.Hidden;
                    mw.ucDiary.mainUser = this.MainUser;
                    mw.ucDiary.DiaryShow();
                }
            });
            OpenTodolistCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {
                if (p is MainWindow mw)
                {
                    mw.ucTodolist.Visibility = Visibility.Visible;
                    mw.ucDiary.Visibility = Visibility.Hidden;
                    mw.ucCalendar.Visibility = Visibility.Hidden;
                    mw.ucUserInfor.Visibility = Visibility.Hidden;
                    mw.ucTodolist.mainUser = this.MainUser;
                    mw.ucTodolist.TodolistShow();
                }
            });
            OpenInformationCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {
                if (p is MainWindow mw)
                {
                    mw.ucUserInfor.Visibility = Visibility.Visible;
                    mw.ucTodolist.Visibility = Visibility.Hidden;
                    mw.ucDiary.Visibility = Visibility.Hidden;
                    mw.ucCalendar.Visibility = Visibility.Hidden;

                    mw.ucUserInfor.txtFirstName.Text = MainUser.FirstName;
                    mw.ucUserInfor.txtLastName.Text = MainUser.LastName;
                    mw.ucUserInfor.txtBirthday.SelectedDate = MainUser.BirthDate;
                    mw.ucUserInfor.cbNation.SelectedIndex = MainUser.NationID - 1;
                    mw.ucUserInfor.txtPhone.Text = MainUser.Detail.PhoneNumber;
                    mw.ucUserInfor.txtIntro.Text = MainUser.Detail.Intro;
                    mw.ucUserInfor.txtEmail.Text = MainUser.Detail.Email;
                    mw.ucUserInfor.txtWork.Text = MainUser.Detail.Work;
                    mw.ucUserInfor.txtAddress.Text = MainUser.Detail.UserAddress;                   
                }
            });
            SignOutCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {
                if (p is MainWindow mw)
                {
                    mw.Close();
                    mw.LoginViewWindow.Visibility = Visibility.Visible;
                    mw.LoginViewWindow.uCSignIn.txtUserName.Text = "";
                    mw.LoginViewWindow.uCSignIn.pwSignIn.Password = "";
                }
            });

            OpenComingEventCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {
                UserEventView ueview = new UserEventView(MainUser);
                ueview.ShowDialog();
            });
            // Tâm trạng người dùng
            RightCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {
                if (p is MainWindow mw)
                {
                    int day = DateTime.Today.Day;
                    int month = DateTime.Today.Month;
                    int year = DateTime.Today.Year;
                    using (var db = new MyContext())
                    {
                        var userEmo = (from n in db.UserMoods
                                       where n.UserID == MainUser.UserID
                                       && n.Date.Day == day
                                       && n.Date.Month == month
                                       && n.Date.Year == year
                                       select n).First();
                        if (userEmo != null)
                        {
                            mw.lbEmotion.Content = db.Moods.Find(userEmo.MoodID).Name;
                        }
                        if (userEmo.MoodID == 7)
                        {
                            userEmo.MoodID = 1;
                            db.SaveChanges();
                            mw.lbEmotion.Content = db.Moods.Find(userEmo.MoodID).Name;
                        }
                        else
                        {
                            userEmo.MoodID++;
                            db.SaveChanges();
                            mw.lbEmotion.Content = db.Moods.Find(userEmo.MoodID).Name;
                        }
                        mw.imgEmotionIcon.Source = ChangeEmoImg(userEmo.MoodID);
                    }
                }
            });
            LeftCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {
                if (p is MainWindow mw)
                {
                    int day = DateTime.Today.Day;
                    int month = DateTime.Today.Month;
                    int year = DateTime.Today.Year;
                    using (var db = new MyContext())
                    {
                        var userEmo = (from n in db.UserMoods
                                       where n.UserID == MainUser.UserID
                                       && n.Date.Day == day
                                       && n.Date.Month == month
                                       && n.Date.Year == year
                                       select n).First();
                        if (userEmo != null)
                        {
                            mw.lbEmotion.Content = db.Moods.Find(userEmo.MoodID).Name;
                        }
                        if (userEmo.MoodID == 1)
                        {
                            userEmo.MoodID = 7;
                            db.SaveChanges();
                            mw.lbEmotion.Content = db.Moods.Find(userEmo.MoodID).Name;
                        }
                        else
                        {
                            userEmo.MoodID--;
                            db.SaveChanges();
                            mw.lbEmotion.Content = db.Moods.Find(userEmo.MoodID).Name;
                        }
                        mw.imgEmotionIcon.Source = ChangeEmoImg(userEmo.MoodID);
                    }
                }
            });
            ShowEmotionCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {
                if (p is MainWindow mw)
                {
                    EmotionView tlw = new EmotionView(MainUser);
                    tlw.ShowDialog();
                }
            });
            ExitCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {
                if (p is MainWindow wd)
                {
                    wd.Close();
                    App.Current.Shutdown();
                }
            });
          
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
