using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsEventsMicroService.Database
{
    public class Sport
    {
        public int SportId { get; set; }
        [Required(ErrorMessage = "Required !")]
        public string SportName { get; set; }
        [Required(ErrorMessage = "Required !")]
        public int NoOfPlayers { get; set; }
        [Required(ErrorMessage = "Required !")]
        public string SportType { get; set; }
    }
}
