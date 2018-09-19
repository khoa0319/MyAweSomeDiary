using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAwesomeDiary.Model
{
    public class UserTask
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserTaskID { get; set; }
        [Required]        
        public string Name { get; set; }
        [MaxLength(200, ErrorMessage = "Max length of description is 200")]
        public string TaskDescription { get; set; }
        public bool Active { get; set; }
        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }
        [ForeignKey("TaskState")]
        public int TaskStateID { get; set; }
        public TaskState TaskState { get; set; }
        [ForeignKey("TaskPriority")]
        public int PriorityID { get; set; }
        public EventPriority TaskPriority { get; set; }

        
    }
    public class TaskState
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
