using MyAwesomeDiary.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace MyAwesomeDiary.ViewModel
{
    public class CalendarViewModel : BaseViewModel
    {
        public IEnumerable ListMonth;
        public int Year { get; set; }
        public string ThisDate { get; set; }
        public int[] Days;
        public ICommand ChangeMonthCommand { get; set; }
        public ICommand OpenYearViewCommand { get; set; }
        public ICommand IncreaseYearCommand { get; set; }
        public ICommand DecreaseYearCommand { get; set; }
        public CalendarViewModel()
        {
            Year = DateTime.Now.Year;
            ListMonth = new Dictionary<int, string>
            {
                {1, "January"},
                {2, "February"},
                {3, "March"},
                {4, "April"},
                {5, "May"},
                {6, "June"},
                {7, "July"},
                {8, "August"},
                {9, "September"},
                {10, "October"},
                {11, "November"},
                {12, "December"},
            };
            ThisDate = DateTime.Today.ToShortDateString();

            ChangeMonthCommand = new RelayCommand<UserControl>(p => { return true; }, p =>
            {
                if (p is CalendarMonthView view)
                {
                    view.Dispatcher.Invoke(new Action(() =>
                    {
                        PopulateCalendar(view.cbMonth.SelectedIndex + 1, view);
                    }), System.Windows.Threading.DispatcherPriority.Background);
                }
            });

            OpenYearViewCommand = new RelayCommand<UserControl>(p => { return true; }, p =>
            {
                if (p is CalendarMonthView view)
                {
                    int tmpDay = DateTime.Now.Day;
                    int tmpYear = int.Parse(view.lbYear.Content.ToString());
                CalendarYearView yv = new CalendarYearView(tmpYear, tmpDay);
                    yv.Show();
                }
            });

            IncreaseYearCommand = new RelayCommand<UserControl>(p => { return true; }, p =>
            {

                if (p is CalendarMonthView view)
                {

                    int tmp = int.Parse(view.lbYear.Content.ToString());
                    tmp++;
                    view.lbYear.Content = tmp.ToString();

                    view.Dispatcher.Invoke(new Action(() =>
                    {
                        PopulateCalendar(view.cbMonth.SelectedIndex + 1, view);
                    }), System.Windows.Threading.DispatcherPriority.Background);
                }
            });

            DecreaseYearCommand = new RelayCommand<UserControl>(p => { return true; }, p =>
            {

                if (p is CalendarMonthView view)
                {
                    int tmp = int.Parse(view.lbYear.Content.ToString());
                    tmp--;
                    view.lbYear.Content = tmp.ToString();

                    view.Dispatcher.Invoke(new Action(() =>
                    {
                        PopulateCalendar(view.cbMonth.SelectedIndex + 1, view);
                    }), System.Windows.Threading.DispatcherPriority.Background);
                }
            });
        }
        // -----------------------------Function

        public void PopulateCalendar(int month, CalendarMonthView view)
        {
            string id = "";
            view.spMonth.Children.RemoveRange(0, view.spMonth.Children.Count);
            FrameworkElement fe = GetWindow(view);
            if (fe != null)            
                if ((fe as MainWindow).MainWDVM.MainUser != null)                
                    id = (fe as MainWindow).MainWDVM.MainUser.UserID;

            int tmpYear = int.Parse(view.lbYear.Content.ToString());
            Days = GetCalendar(tmpYear, month);
            for (int i = 0; i < 6; i++)
            {
                StackPanel sp = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    Height = 80
                };
                for (int j = 7 * i; j < (i + 1) * 7; j++)
                {
                    Label lb = new Label()
                    {
                        Content = Days[j].ToString(),
                        Width = 100,
                        Height = 80,
                        FontSize = 15,
                        HorizontalContentAlignment = HorizontalAlignment.Right,
                        VerticalContentAlignment = VerticalAlignment.Top,
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1, 1, 1, 1),
                    };
                    if (Days[j] == 0)
                    {
                        lb.IsEnabled = false;
                        lb.Foreground = Brushes.Gray;
                    }
                    sp.Children.Add(lb);
                    lb.MouseEnter += (sender, e) =>
                    {
                        lb.Background = new SolidColorBrush(Colors.LightGoldenrodYellow);
                    };
                    lb.MouseLeave += (sender, e) =>
                    {
                        lb.Background = new SolidColorBrush(Colors.Transparent);
                    };
                    lb.MouseUp += (sender, e) =>
                    {
                        AddEventView eventView = new AddEventView(id, int.Parse(lb.Content as string));
                        eventView.ShowDialog();
                    };
                }
                view.spMonth.Children.Add(sp);
                if (i == 0)
                    Canvas.SetTop(sp, 20);
                else
                    Canvas.SetTop(sp, (i * 80) + 20);
                //Canvas.SetLeft(sp, 20);
            }

            //IEnumerable<Label> lbList = view.gridMonth.Children.OfType<Label>();

            //foreach (Label item in lbList.Where(l => (l.Content as string).CompareTo("Mon") != 0 && ((l.Content as string).CompareTo("Tue") != 0)))
            //{
            //    item.Background = Brushes.Red;
            //}
        }
        private static IEnumerable<T> FindChilder<T>(DependencyObject depoj) where T : DependencyObject
        {
            if (depoj != null)
            {
                int count = VisualTreeHelper.GetChildrenCount(depoj);
                for (int i = 0; i < count; i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depoj, i);
                    if (child != null && child is T)
                        yield return (T)child;

                    foreach (T item in FindChilder<T>(child))
                    {
                        yield return item;
                    }
                }
            }
        }

        public int[] GetCalendar(int year, int month)
        {
            DateTime dt = new DateTime(year, month, 1);
            int startIndex = (int)dt.DayOfWeek;
            var endIndex = DateTime.DaysInMonth(year, month);
            var result = new int[42];
            for (int i = startIndex; i < startIndex + endIndex; i++)
                result[i] = (i - startIndex) + 1;
            return result;
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
