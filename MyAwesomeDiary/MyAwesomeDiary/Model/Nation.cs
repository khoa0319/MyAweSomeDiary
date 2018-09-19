using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAwesomeDiary.Model
{
    public class Nation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NationID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }

    public class SpecialDay
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SpecialDayID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
    }

    public class NationalDay
    {
        [ForeignKey("Nation")]
        [Key]
        [Column(Order = 0)]
        public int NationID { get; set; }
        public Nation Nation { get; set; }

        [ForeignKey("SpecialDay")]
        [Key]
        [Column(Order = 1)]
        public int SpecialDayID { get; set; }
        public SpecialDay SpecialDay { get; set; }
        public DateTime Date { get; set; }
        
    }
}
