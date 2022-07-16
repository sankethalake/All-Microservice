using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParticipationMicroservice.Model
{
    public class Sport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SportId { get; set; }
        [Required]
        public string SportName { get; set; }
        [Required]
        public int NoOfPlayers { get; set; }
        [Required]
        public string sportType { get; set; }
    }
}
