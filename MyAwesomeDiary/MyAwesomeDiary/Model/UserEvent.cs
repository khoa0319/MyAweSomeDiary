using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAwesomeDiary.Model
{
    public class UserEvent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserEventID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [MaxLength(50)]
        public string Location { get; set; }
        public bool AllDay { get; set; }
        public bool Active { get; set; }
        [MaxLength(200)]
        public string Descriptions { get; set; }
        [ForeignKey("User")]
        public string UserID { get; set; }
        public User User { get; set; }
        [ForeignKey("EventType")]
        public int EvenTypeID { get; set; }
        public EventType EventType { get; set; }
        [ForeignKey("EventPriority")]
        public int PriorityID { get; set; }
        public EventPriority EventPriority { get; set; }
       
    }
    public class EventType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventTypeID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }

    public class EventPriority
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventPriorityID { get; set; } 
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
