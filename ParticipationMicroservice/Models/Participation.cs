using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParticipationMicroservice.Model
{
    public class Participation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParticipationId { get; set; }

        [ForeignKey("EventId")]
        public int EventId { get; set; }
        public virtual Event Events{ get; set; }

        //Commented due to transitive dependancy
        //[ForeignKey("Sport")]
        //public Sport Sports { get; set; }

        [Required(ErrorMessage = "Players/Players are required")]
        public virtual IList<Player> Player { get; set; }

        [Required(ErrorMessage = "Required")]
        [EnumDataType(typeof(ParticipationStatus))]
        public ParticipationStatus Status { get; set; }
    }

            public enum ParticipationStatus
            {
                pending = 1,
                approved = 2,
                declined = 3
            }
        
    
}
