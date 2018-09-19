using MyAwesomeDiary.Model;
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
    /// Interaction logic for ReadingDiaryView.xaml
    /// </summary>
    public partial class ReadingDiaryView : Window
    {
        string txt;
        public ReadingDiaryView(int id)
        {
            InitializeComponent();
            txtReading.IsReadOnly = true;
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
                TextRange tr = new TextRange(txtReading.Document.ContentStart, txtReading.Document.ContentEnd);
                tr.Load(ms, DataFormats.Rtf);
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
