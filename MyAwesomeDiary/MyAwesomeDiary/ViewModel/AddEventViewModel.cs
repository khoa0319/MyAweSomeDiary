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
    public class AddEventViewModel : BaseViewModel
    {
        public List<UserEvent> lstEvent;
        public List<EventType> LstEventType { get; set; }
        public List<EventPriority> LstEventPriority { get; set; }
        private TimeSpan diff;
        public ICommand ChangeEventCommand { get; set; }
        public ICommand CreateNewEventCommand { get; set; }
        public ICommand SaveNewEventCommand { get; set; }
        public ICommand EnableEndDateCommand { get; set; }
        public ICommand EnableInDayCommand { get; set; }
        public ICommand DisableInDayCommand { get; set; }
        public ICommand ExitAddEventCommand { get; set; }
        public ICommand DeactiveEventCommand { get; set; }
        public ICommand UpdateEventCommand { get; set; }
        public ICommand SaveUpdateEventCommand { get; set; }
        public AddEventViewModel(string id, int day)
        {
            QueryForToday(id, day);
            using (var db = new MyContext())
            {
                var lstt = db.EventTypes.ToList();
                var lstp = db.EventPriorities.ToList();
                LstEventType = lstt;
                LstEventPriority = lstp;
            }
            ChangeEventCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {
                if (p is AddEventView view)
                {
                    if (view.gridEvent.SelectedItem != null)
                    {
                        view.spDate.IsEnabled = view.spRadio.IsEnabled = view.spTL.IsEnabled = view.txtDes.IsEnabled = false;
                        UserEvent ue = view.gridEvent.SelectedItem as UserEvent;
                        //view.spModify.DataContext = ue;
                        view.txtEventName.Text = ue.Name;
                        view.txtLocation.Text = ue.Location;
                        view.txtDes.Text = ue.Descriptions;
                        view.cbEventType.SelectedIndex = ue.EvenTypeID - 1;
                        view.cbPriority.SelectedIndex = ue.PriorityID - 1;
                        int dDiff = ue.EndDate.Day - ue.StartDate.Day;
                        diff = ue.EndDate - ue.StartDate;
                        int hDiff = ue.EndDate.Hour - ue.StartDate.Hour;
                        if (diff.Hours == 0)
                            view.chkAllDay.IsChecked = ue.AllDay;
                        if (diff.TotalHours > 0 && diff.TotalHours < 13)
                            view.chkInDay.IsChecked = true;
                        if (diff.TotalHours > 24)
                            view.chkManyDays.IsChecked = true;
                    }
                }
            });

            // mở ô thêm sự kiện
            CreateNewEventCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {
                if (p is AddEventView view)
                {
                    view.txtDes.Text = view.txtEventName.Text = view.txtLocation.Text = "";
                    view.chkInDay.IsChecked = true;
                    view.startDate.SelectedDate = view.endDate.SelectedDate = DateTime.Now;
                    view.btnSave.IsEnabled = true;
                    view.gridEvent.SelectedIndex = -1;
                    view.spDate.IsEnabled = view.spRadio.IsEnabled = view.spTL.IsEnabled = view.txtDes.IsEnabled = true;
                }
            });

            // save sự kiện
            SaveNewEventCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {
                if (p is AddEventView view)
                {
                    view.btnSave.IsEnabled = false;
                    view.spDate.IsEnabled = view.spRadio.IsEnabled = view.spTL.IsEnabled = view.txtDes.IsEnabled = false;
                    using (var db = new MyContext())
                    {
                        var toAdd = new UserEvent
                        {
                            UserID = id,
                            Active = true,
                            AllDay = view.chkAllDay.IsEnabled,
                            Descriptions = view.txtDes.Text,
                            Location = view.txtLocation.Text,
                            Name = view.txtEventName.Text,
                            PriorityID = view.cbPriority.SelectedIndex + 1,
                            EvenTypeID = view.cbEventType.SelectedIndex + 1
                        };
                        int year = view.startDate.SelectedDate.Value.Year;
                        int month = view.startDate.SelectedDate.Value.Month;
                        int tmpday = view.startDate.SelectedDate.Value.Day;
                        DateTime tmp = new DateTime(year, month, tmpday, view.cbHourStart.SelectedIndex, 0, 0);
                        toAdd.StartDate = tmp;
                        if ((bool)view.chkAllDay.IsChecked)                        
                            toAdd.EndDate = toAdd.StartDate;                        
                        if ((bool)view.chkInDay.IsChecked)
                        {
                            toAdd.EndDate = toAdd.StartDate;
                            toAdd.EndDate = toAdd.EndDate.AddHours(view.cbHour.SelectedIndex + 1);
                        }
                        if ((bool)view.chkManyDays.IsChecked)                        
                            toAdd.EndDate = view.endDate.SelectedDate.Value;

                        db.UserEvents.Add(toAdd);
                        db.SaveChanges();

                    }

                    QueryForToday(id, day);
                    view.gridEvent.ItemsSource = lstEvent;

                }
            });

            EnableEndDateCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {

                //MessageBox.Show("abc");
                if (p is AddEventView view)
                {
                    view.cbHour.IsEnabled = false;
                    view.endDate.IsEnabled = true;
                }
            });

            EnableInDayCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {
                if (p is AddEventView view)
                {
                    view.cbHour.IsEnabled = true;
                    view.endDate.IsEnabled = false;
                }
            });

            DisableInDayCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {
                if (p is AddEventView view)
                {
                    view.cbHour.IsEnabled = false;
                    view.endDate.IsEnabled = false;
                }
            });

            //exit command
            ExitAddEventCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {
                if (p is AddEventView view)
                {
                    view.Close();
                }
            });
            // deactive command
            DeactiveEventCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {
                if (p is AddEventView view)
                {
                    if (view.gridEvent.SelectedItem != null)
                    {

                        MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn xóa sự kiện này?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (result == MessageBoxResult.Yes)
                        {
                            int tmp = (view.gridEvent.SelectedItem as UserEvent).UserEventID;
                            using (var db = new MyContext())
                            {
                                var toFind = db.UserEvents.Find(tmp);
                                if (toFind != null)
                                {
                                    toFind.Active = false;
                                    db.SaveChanges();
                                }
                            }
                            QueryForToday(id, day);
                            view.gridEvent.ItemsSource = lstEvent;
                        }
                    }
                }
            });

            //update sự kiện
            UpdateEventCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {
                if (p is AddEventView view)
                {                    
                    view.spTL.IsEnabled = view.txtDes.IsEnabled = true;
                    view.btnSaveModify.IsEnabled = true;
                }
            });

            SaveUpdateEventCommand = new RelayCommand<Window>(p => { return true; }, p =>
            {
                if (p is AddEventView view)
                {
                    view.spDate.IsEnabled = view.spRadio.IsEnabled = view.spTL.IsEnabled = view.txtDes.IsEnabled = false;
                    view.btnSaveModify.IsEnabled = false;
                    if (view.gridEvent.SelectedIndex != -1)
                    {
                        using (var db = new MyContext())
                        {
                            var ueid = (view.gridEvent.SelectedItem as UserEvent).UserEventID;
                            var toChange = db.UserEvents.Find(ueid);
                            if (toChange != null)
                            {
                                toChange.Name = view.txtEventName.Text;
                                toChange.Location = view.txtLocation.Text;
                                toChange.Descriptions = view.txtDes.Text;
                                toChange.PriorityID = view.cbPriority.SelectedIndex + 1;
                                toChange.EvenTypeID = view.cbEventType.SelectedIndex + 1;
                            }
                            db.SaveChanges();
                        }
                        QueryForToday(id, day);
                        view.gridEvent.ItemsSource = lstEvent;
                    }
                    else
                    {
                        MessageBox.Show("Bạn phải chọn sự kiện để cập nhật");
                    }
                    
                }
            });


        }

        private void QueryForToday(string id, int day)
        {
            using (var db = new MyContext())
            {
                var lst = db.UserEvents.Where(ue => ue.UserID == id && ue.Active == true && (ue.StartDate.Day == day || ue.EndDate.Day == day)).ToList();
                lstEvent = lst;
            }
        }
    }
}
