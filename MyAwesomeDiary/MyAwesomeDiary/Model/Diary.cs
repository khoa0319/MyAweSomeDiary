using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAwesomeDiary.Model
{
    public class Diary
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiaryID { get; set; }
        [ForeignKey("User")]
        [Required]
        public string UserID { get; set; }
        public User User { get; set; }
        public DateTime WritingDate { get; set; }
        public string Content { get; set; }
        public bool Active { get; set; }
       
    }
}
