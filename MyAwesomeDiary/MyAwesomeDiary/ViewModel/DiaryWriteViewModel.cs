using MyAwesomeDiary.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;


namespace MyAwesomeDiary.ViewModel
{
    class DiaryWriteViewModel
    {
        private string dateAndTime = GetDate();
        private List<int> fontSize = GetFontSize();
        public ICommand SaveDiaryCommand { get; set; }
        public ICommand ChangeFontColorCommand { get; set; }
        public ICommand AddImage { get; set; }

        public string DateAndTime { get => dateAndTime; set => dateAndTime = value; }
        public List<int> FontSize { get => fontSize; set => fontSize = value; }

        public DiaryWriteViewModel(User user)
        {
            SaveDiaryCommand = new RelayCommand<DiaryWriteView>(p => { return true; }, p =>
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
                // Ngày thêm nhật kí
                DateTime todayDate = DateTime.Today;
                // Khởi tạo một Diary
                var diary = new Diary
                {
                    Content = rtfText,
                    WritingDate = todayDate,
                    UserID = user.UserID,
                    Active = true
                };
                using (var db = new MyContext())
                {
                    db.Diaries.Add(diary);
                    db.SaveChanges();
                }
                p.Close();
            });
            ChangeFontColorCommand = new RelayCommand<DiaryWriteView>(p => { return true; }, p =>
            {
                string colorFont = "#FFF";
                colorFont = p.clrFont.SelectedColor.ToString();
                p.txtDiary.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, (Brush)(new BrushConverter().ConvertFrom(colorFont)));
            });
            AddImage = new RelayCommand<DiaryWriteView>(p => { return true; }, p =>
            {
                // Mở file Dialog
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                // Chỉnh địa điểm file
                dlg.DefaultExt = "*.bmp,*.jpg,*.gif,*.png";
                dlg.Filter = "Image Files (*.bmp;*.jpg;*.gif;*.png)|*.bmp;*.jpg;*.gif;*.png|All files (*.*)|*.*";
                Nullable<bool> result = dlg.ShowDialog();
                // kiểm tra đã mở dlg 
                if (result == true)
                {
                    // Thay đổi hình ảnh
                    string filename = dlg.FileName;
                    Paragraph para = new Paragraph();

                    BitmapImage bitmap = new BitmapImage(new Uri(@filename));
                    Image image = new Image
                    {
                        Source = bitmap,
                        Width = 100
                    };
                    para.Inlines.Add(image);
                    p.fld.Blocks.Add(para);

                }
            });
        }
        static string GetDate()
        {
            string day, month, year;
            DateTime thisday = DateTime.Today;
            day = thisday.ToString("dd ");
            month = thisday.ToString("MM ");
            year = thisday.ToString("yyyy ");
            string todayDate = "Ngày " + day + "Tháng " + month + "Năm " + year;
            return todayDate;
        }
        static List<int> GetFontSize()
        {
            var size = new List<int> { 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34 };
            return size;
        }
    }
}
