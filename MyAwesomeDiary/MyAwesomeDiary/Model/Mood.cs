using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAwesomeDiary.Model
{
    public class Mood
    {
        [Key]
        public int ID { get; set; }
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }
    }
    public class UserMood
    {
        public int UserMoodID { get; set; }
        [ForeignKey("User")]        
        public string UserID { get; set; }
        public User User { get; set; }
        [ForeignKey("Mood")]
        public int MoodID { get; set; }
        public Mood Mood { get; set; }
        public DateTime Date { get; set; }
       
    }
}
