using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlayersMicroService.Database
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
        public string SportType { get; set; }


    }
}
