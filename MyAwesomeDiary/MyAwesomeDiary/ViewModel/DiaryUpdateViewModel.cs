using MyAwesomeDiary.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace MyAwesomeDiary.ViewModel
{
    class DiaryUpdateViewModel : BaseViewModel
    {
        private List<int> fontSize = GetFontSize();
        public ICommand SaveDiaryCommand { get; set; }
        public ICommand ChangeFontColorCommand { get; set; }
        public List<int> FontSize { get => fontSize; set => fontSize = value; }

        public DiaryUpdateViewModel(int id)
        {
            SaveDiaryCommand = new RelayCommand<DiaryUpdateView>(p => { return true; }, p =>
            {
                // Tạo một diary mới
                string rtfText;
                // Chuyển đổi string sang rtf
                TextRange tr = new TextRange(p.txtDiary.Document.ContentStart, p.txtDiary.Document.ContentEnd);
                using (MemoryStream ms = new MemoryStream())
                {
                    tr.Save(ms, DataFormats.Rtf);
                    rtfText = Encoding.ASCII.GetString(ms.ToArray());
                }
                // Khởi tạo một Diary
                using (var db = new MyContext())
                {
                    db.Diaries.Find(id).Content = rtfText;
                    db.SaveChanges();
                }
                p.Close();
            });
            ChangeFontColorCommand = new RelayCommand<DiaryUpdateView>(p => { return true; }, p =>
            {
                string colorFont = "#FFF";
                colorFont = p.clrFont.SelectedColor.ToString();
                p.txtDiary.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, (Brush)(new BrushConverter().ConvertFrom(colorFont)));
            });
        }
        static List<int> GetFontSize()
        {
            var size = new List<int> { 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34 };
            return size;
        }
    }
}
