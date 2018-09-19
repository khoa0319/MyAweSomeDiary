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
    /// Interaction logic for TodoListView.xaml
    /// </summary>
    public partial class TodoListView : UserControl
    {
        public User mainUser;
        private TodoListViewModel TLVM;
        public TodoListView()
        {
            InitializeComponent();
            this.DataContext = TLVM = new TodoListViewModel();
        }
        public void TodolistShow()
        {
            using (var db = new MyContext())
            {
                var m = (from n in db.UserTasks
                         where n.UserID == mainUser.UserID
                         && n.Active == true
                         && n.TaskStateID == 1
                         select n).ToList();
                lwToDo.ItemsSource = m;
            }
            using (var db = new MyContext())
            {
                var m = (from n in db.UserTasks
                         where n.UserID == mainUser.UserID
                         && n.Active == true
                         && n.TaskStateID == 2
                         select n).ToList();
                lwDoing.ItemsSource = m;
            }
            using (var db = new MyContext())
            {
                var m = (from n in db.UserTasks
                         where n.UserID == mainUser.UserID
                         && n.Active == true
                         && n.TaskStateID == 3
                         select n).ToList();
                lwDone.ItemsSource = m;
            }
        }
    }
}
