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
using System.Windows.Shapes;

namespace MyAwesomeDiary
{
    /// <summary>
    /// Interaction logic for DiaryWriteView.xaml
    /// </summary>
    public partial class DiaryWriteView : Window
    {
        private DiaryWriteViewModel DWVM;
        public DiaryWriteView(User user)
        {
            InitializeComponent();
            this.DataContext = DWVM = new DiaryWriteViewModel(user);
        }
        private void cbFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                txtDiary.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cbFontSize.Text);
            }
            catch
            {
                MessageBox.Show("Chi nhập số" + "\n" + "Vui lòng nhập lại", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Error);
                cbFontSize.Text = 10.ToString();
            }

        }
        private void btnDiaryDel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
