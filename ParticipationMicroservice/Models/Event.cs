using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParticipationMicroservice.Model
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }

        [ForeignKey("SportId")]
        public int SportId { get; set; }
        public virtual Sport Sports { get; set; }

        [Required(ErrorMessage = "Required")]
        public string EventName { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage ="Required")]
        public int NoOfSlots { get; set; }
    }
}
