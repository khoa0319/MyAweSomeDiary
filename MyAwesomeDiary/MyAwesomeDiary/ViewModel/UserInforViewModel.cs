using MyAwesomeDiary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyAwesomeDiary.ViewModel
{
    class UserInforViewModel
    {
        public List<Nation> Nations;
        public ICommand UpdateInfoCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }
        public UserInforViewModel()
        {
            using (var db = new MyContext())
            {
                Nations = db.Nations.ToList();
            }
            UpdateInfoCommand = new RelayCommand<UserControl>(p => { return true; }, p =>
            {
                if (p is UserInforView view)
                {

                    FrameworkElement fe = GetWindow(p);
                    string id = (fe as MainWindow).MainWDVM.MainUser.UserID;
                    if (id != null)
                    {
                        using (var db = new MyContext())
                        {
                            db.Users.Find(id).FirstName = view.txtFirstName.Text;
                            db.Users.Find(id).LastName = view.txtLastName.Text;
                            db.Users.Find(id).BirthDate = view.txtBirthday.SelectedDate;
                            db.Users.Find(id).Detail.PhoneNumber = view.txtPhone.Text;
                            db.Users.Find(id).Detail.Email = view.txtEmail.Text;
                            db.Users.Find(id).Detail.Work = view.txtWork.Text;
                            db.Users.Find(id).Detail.Intro = view.txtIntro.Text;
                            db.Users.Find(id).NationID = view.cbNation.SelectedIndex + 1;
                            db.SaveChanges();
                        }
                        MessageBox.Show("Cập nhật thành công");
                    }

                }
                //MessageBox.Show("Updated");
            });

            ChangePasswordCommand = new RelayCommand<UserControl>(p => { return true; }, p =>
            {
                if (p is UserInforView view)
                {                                   
                    FrameworkElement fe = GetWindow(p);
                    if (fe is MainWindow wd)
                    {
                        //MessageBox.Show(wd.MainWDVM.MainUser.Password);
                        if (new PasswordSecurityProvider().Validate(wd.MainWDVM.MainUser.UserID, view.txtOldPass.Text))
                        {
                            using (var db = new MyContext())
                            {
                                User tmp = db.Users.Find(wd.MainWDVM.MainUser.UserID);
                                tmp.Password = new PasswordSecurityProvider().GetHashPassword(view.txtNewPass.Password);
                                db.SaveChanges();
                            }
                            MessageBox.Show("Đổi mật khẩu thành công");
                        }
                        else
                        {

                            MessageBox.Show("Bạn đã nhập sai mật khẩu");
                        }
                    }
                }
            });
        }

        //---------------------------
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
