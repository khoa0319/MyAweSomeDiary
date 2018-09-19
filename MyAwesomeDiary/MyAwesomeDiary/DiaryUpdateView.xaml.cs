using MyAwesomeDiary.Model;
using MyAwesomeDiary.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for DiaryUpdateView.xaml
    /// </summary>
    public partial class DiaryUpdateView : Window
    {
        private DiaryUpdateViewModel DUVM;
        public DiaryUpdateView(int id)
        {
            string txt;
            InitializeComponent();
            this.DataContext = DUVM = new DiaryUpdateViewModel(id);
            // Tìm đúng ID để ghi ra string
            using (var db = new MyContext())
            {
                var temp = db.Diaries.Find(id);
                txt = temp.Content;
            }
            string rtfText = txt;
            // Chuyển đổi rtf ra string
            byte[] byteArray = Encoding.ASCII.GetBytes(rtfText);
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                TextRange tr = new TextRange(txtDiary.Document.ContentStart, txtDiary.Document.ContentEnd);
                tr.Load(ms, DataFormats.Rtf);
            }
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
