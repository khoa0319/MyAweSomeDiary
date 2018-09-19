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
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyAwesomeDiary
{
    /// <summary>
    /// Interaction logic for CalendarYearView.xaml
    /// </summary>
    public partial class CalendarYearView : Window
    {
        private Dictionary<int, string> lstWeek;
        private Dictionary<int, string> lstMonth;
        public CalendarYearView(int year, int day)
        {

            InitializeComponent();
            lstWeek = new Dictionary<int, string>
            {
                {0, "CN"},
                {1, "Hai"},
                {2, "Ba"},
                {3, "Tư"},
                {4, "Năm"},
                {5, "Sáu"},
                {6, "Bảy"},
            };
            lstMonth = new Dictionary<int, string>
            {
                {0, "Tháng 1"},
                {1, "Tháng 2"},
                {2, "Tháng 3"},
                {3, "Tháng 4"},
                {4, "Tháng 5"},
                {5, "Tháng 6"},
                {6, "Tháng 7"},
                {7, "Tháng 8"},
                {8, "Tháng 9"},
                {9, "Tháng 10"},
                {10, "Tháng 11"},
                {11, "Tháng 12"},
            };
            this.Dispatcher.Invoke(new Action(() =>
            {
                PopulateYear(year, day);
            }), DispatcherPriority.Background);            
        }
        private void PopulateYear(int year, int day)
        {
            for (int m = 0; m < 12; m++)
            {
                int[] Days = GetCalendar(year, m + 1);
                StackPanel wrapper = new StackPanel()
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
                this.gridMain.Children.Add(wrapper);
                var lbMonth = new Label
                {
                    Width = 80,
                    Height = 35,
                    Content = lstMonth[m],
                    FontSize = 13,
                    HorizontalAlignment = HorizontalAlignment.Left
                };                
                wrapper.Children.Add(lbMonth);
                StackPanel secondWrapper = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                wrapper.Children.Add(secondWrapper);
                for (int k = 0; k < 7; k++)
                {
                    Label lb = new Label()
                    {
                        Content = lstWeek[k],
                        Width = 38,
                        Height = 28,
                        //FontSize = 15,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Top,
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1, 1, 1, 1),
                    };
                    secondWrapper.Children.Add(lb);
                }
                for (int i = 0; i < 6; i++)
                {
                    StackPanel sp = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    for (int j = 7 * i; j < (i + 1) * 7; j++)
                    {
                        Label lb = new Label()
                        {
                            Content = Days[j].ToString(),
                            Width = 38,
                            Height = 28,
                            //FontSize = 15,
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
                            lb.Background = new SolidColorBrush(Colors.LightBlue);
                        };
                        lb.MouseLeave += (sender, e) =>
                        {
                            lb.Background = new SolidColorBrush(Colors.Transparent);
                        };

                    }
                    wrapper.Children.Add(sp);                    
                }
                if (m < 4)
                {
                    Grid.SetRow(wrapper, 0);
                    Grid.SetColumn(wrapper, m);
                }
                if (m >= 4 && m <8)
                {
                    Grid.SetRow(wrapper, 1);
                    Grid.SetColumn(wrapper, m - 4);
                }
                if (m >= 8)
                {
                    Grid.SetRow(wrapper, 2);
                    Grid.SetColumn(wrapper, m - 8);
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

    }
}
