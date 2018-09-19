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
    class TodoListViewModel
    {

        public ICommand AddTaskCommand { get; set; }
        public ICommand TodolstFocus { get; set; }
        public ICommand DoinglstFocus { get; set; }
        public ICommand DonelstFocus { get; set; }
        public ICommand ToTodo { get; set; }
        public ICommand ToDoing { get; set; }
        public ICommand ToDone { get; set; }
        public ICommand Delete { get; set; }

        public TodoListViewModel()
        {
            AddTaskCommand = new RelayCommand<TodoListView>(p => { return true; }, p =>
            {
                var ut = new UserTask
                {
                    UserID = p.mainUser.UserID,
                    Active = true,
                    TaskDescription = p.txtAddItem.Text,
                    Name = p.txtAddItem.Text,
                    PriorityID = 1,
                    TaskStateID = 1

                };
                using (var db = new MyContext())
                {
                    db.UserTasks.Add(ut);
                    db.SaveChanges();
                }
                p.lwToDo.ItemsSource = GetTodoList(p.mainUser);
                MessageBox.Show("Added");
            });
            ToTodo = new RelayCommand<TodoListView>(p => { return true; }, p =>
            {
                int id = GetTaskID(p.lwToDo, p.lwDoing, p.lwDone);
                using (var db = new MyContext())
                {
                    try
                    {
                        db.UserTasks.Find(id).TaskStateID = 1;
                        db.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }
                p.lwToDo.ItemsSource = GetTodoList(p.mainUser);
                p.lwDoing.ItemsSource = GetDoingList(p.mainUser);
                p.lwDone.ItemsSource = GetDoneList(p.mainUser);
            });
            ToDoing = new RelayCommand<TodoListView>(p => { return true; }, p =>
            {
                int id = GetTaskID(p.lwToDo, p.lwDoing, p.lwDone);
                using (var db = new MyContext())
                {
                    try
                    {
                        db.UserTasks.Find(id).TaskStateID = 2;
                        db.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }
                p.lwToDo.ItemsSource = GetTodoList(p.mainUser);
                p.lwDoing.ItemsSource = GetDoingList(p.mainUser);
                p.lwDone.ItemsSource = GetDoneList(p.mainUser);
            });
            ToDone = new RelayCommand<TodoListView>(p => { return true; }, p =>
            {
                int id = GetTaskID(p.lwToDo, p.lwDoing, p.lwDone);
                using (var db = new MyContext())
                {
                    try
                    {
                        db.UserTasks.Find(id).TaskStateID = 3;
                        db.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }
                p.lwToDo.ItemsSource = GetTodoList(p.mainUser);
                p.lwDoing.ItemsSource = GetDoingList(p.mainUser);
                p.lwDone.ItemsSource = GetDoneList(p.mainUser);
            });
            Delete = new RelayCommand<TodoListView>(p => { return true; }, p =>
            {
                int id = GetTaskID(p.lwToDo, p.lwDoing, p.lwDone);
                using (var db = new MyContext())
                {
                    try
                    {
                        db.UserTasks.Find(id).Active = false;
                        db.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }
                p.lwToDo.ItemsSource = GetTodoList(p.mainUser);
                p.lwDoing.ItemsSource = GetDoingList(p.mainUser);
                p.lwDone.ItemsSource = GetDoneList(p.mainUser);
            });

            TodolstFocus = new RelayCommand<TodoListView>(p => { return true; }, p =>
            {
                p.btnTodoToDoing.Visibility = Visibility.Visible;
                p.btnTodoToDone.Visibility = Visibility.Visible;
                p.btnDoingToTodo.Visibility = Visibility.Hidden;
                p.btnDoingToDone.Visibility = Visibility.Hidden;
                p.btnDoneToTodo.Visibility = Visibility.Hidden;
                p.btnDoneToDoing.Visibility = Visibility.Hidden;
                p.lwDoing.SelectedValue = null;
                p.lwDone.SelectedValue = null;
            });
            DoinglstFocus = new RelayCommand<TodoListView>(p => { return true; }, p =>
            {
                p.btnDoingToTodo.Visibility = Visibility.Visible;
                p.btnDoingToDone.Visibility = Visibility.Visible;
                p.btnTodoToDoing.Visibility = Visibility.Hidden;
                p.btnTodoToDone.Visibility = Visibility.Hidden;
                p.btnDoneToTodo.Visibility = Visibility.Hidden;
                p.btnDoneToDoing.Visibility = Visibility.Hidden;
                p.lwToDo.SelectedValue = null;
                p.lwDone.SelectedValue = null;
            });
            DonelstFocus = new RelayCommand<TodoListView>(p => { return true; }, p =>
            {
                p.btnDoneToTodo.Visibility = Visibility.Visible;
                p.btnDoneToDoing.Visibility = Visibility.Visible;
                p.btnTodoToDoing.Visibility = Visibility.Hidden;
                p.btnTodoToDone.Visibility = Visibility.Hidden;
                p.btnDoingToTodo.Visibility = Visibility.Hidden;
                p.btnDoingToDone.Visibility = Visibility.Hidden;
                p.lwToDo.SelectedValue = null;
                p.lwDoing.SelectedValue = null;
            });
        }
        private static List<UserTask> GetTodoList(User user)
        {
            List<UserTask> userTasks;
            using (var db = new MyContext())
            {
                userTasks = (from n in db.UserTasks
                             where n.UserID == user.UserID
                             && n.Active == true
                             && n.TaskStateID == 1
                             select n).ToList();
            }
            return userTasks;
        }
        private static List<UserTask> GetDoingList(User user)
        {
            List<UserTask> userTasks;
            using (var db = new MyContext())
            {
                userTasks = (from n in db.UserTasks
                             where n.UserID == user.UserID
                             && n.Active == true
                             && n.TaskStateID == 2
                             select n).ToList();
            }
            return userTasks;
        }
        private static List<UserTask> GetDoneList(User user)
        {
            List<UserTask> userTasks;
            using (var db = new MyContext())
            {
                userTasks = (from n in db.UserTasks
                             where n.UserID == user.UserID
                             && n.Active == true
                             && n.TaskStateID == 3
                             select n).ToList();
            }
            return userTasks;
        }
        private static int GetTaskID(ListView todo, ListView doing, ListView done)
        {
            int utid;
            if (todo.SelectedValue != null)
            {
                return utid = ((UserTask)todo.SelectedItem).UserTaskID;
            }
            if (doing.SelectedValue != null)
            {
                return utid = ((UserTask)doing.SelectedItem).UserTaskID;
            }
            if (done.SelectedValue != null)
            {
                return utid = ((UserTask)done.SelectedItem).UserTaskID;
            }
            return 0;
        }
    }
}
