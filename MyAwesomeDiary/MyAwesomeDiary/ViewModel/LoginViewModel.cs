using MyAwesomeDiary.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyAwesomeDiary.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public MainWindow MainWD { get; set; }
        public IEnumerable<Nation> LstNation;
        public ICommand LoginCommand { get; set; }
        public ICommand ToSignUpViewCommand { get; set; }
        public ICommand ToSignInViewCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public LoginViewModel()
        {
            using (var db = new MyContext())
            {
                LstNation = db.Nations.ToList();
            }
            LoginCommand = new RelayCommand<UserControl>(p => { return true; }, p =>
            {
                if (p is SignInView si)
                {
                    PasswordSecurityProvider psp = new PasswordSecurityProvider();
                    if (psp.Validate(si.txtUserName.Text, si.pwSignIn.Password))
                    {
                        User toGet;
                        using (var db = new MyContext())
                        {
                            toGet = db.Users.Find(si.txtUserName.Text);
                        }
                        FrameworkElement fe = GetWindow(p);
                        (fe as LoginView).Visibility = Visibility.Hidden;
                        MainWD = new MainWindow(toGet, fe as LoginView);
                        MainWD.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Sai tài khoản hoặc mật khẩu");
                    }
                }
            });
            SignUpCommand = new RelayCommand<UserControl>(p => { return true; }, p =>
            {
                if (p is SignUpView su)
                {
                    User result = PasswordSecurityProvider.Find(su.txtUserName.Text);
                    if (result == null)
                    {
                        PasswordSecurityProvider psp = new PasswordSecurityProvider();
                        string hashedpassword = psp.GetHashPassword(su.pwSignUp.Password);
                        var ToAdd = new User
                        {
                            UserID = su.txtUserName.Text,
                            Password = hashedpassword,
                            BirthDate = (DateTime)su.dpSignUp.SelectedDate,
                            NationID = su.cbNation.SelectedIndex + 1,
                            PrivateAnswer = su.txtAns.Text
                        };
                        using (var db = new MyContext())
                        {
                            try
                            {
                                db.Users.Add(ToAdd);
                                db.SaveChanges();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Đăng ký thất bại");
                            }
                        }
                        MessageBox.Show("Đăng ký Thành công");
                    }
                    else
                    {

                        MessageBox.Show("Đã tồn tại UserID này");
                    }
                    

                }
            });

            ToSignUpViewCommand = new RelayCommand<UserControl>(p => { return true; }, p =>
            {
                try
                {
                    FrameworkElement fe = GetWindow(p);
                    (fe as LoginView).uCSignIn.Visibility = Visibility.Hidden;
                    (fe as LoginView).uCSignUp.Visibility = Visibility.Visible;
                }
                catch (Exception e)
                {

                    MessageBox.Show(e.StackTrace.ToString());
                    return;
                }
            });
            ToSignInViewCommand = new RelayCommand<UserControl>(p => { return true; }, p =>
            {
                try
                {
                    FrameworkElement fe = GetWindow(p);
                    (fe as LoginView).uCSignUp.Visibility = Visibility.Hidden;
                    (fe as LoginView).uCSignIn.Visibility = Visibility.Visible;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.StackTrace.ToString());
                    return;
                }
            });
        }      
        
        private FrameworkElement GetWindow(UserControl p)
        {
            if (p is FrameworkElement fe)
            {
                while (fe.Parent != null)
                    fe = fe.Parent as FrameworkElement;
                return fe;
            }
            return null;
        }
    }    
}
