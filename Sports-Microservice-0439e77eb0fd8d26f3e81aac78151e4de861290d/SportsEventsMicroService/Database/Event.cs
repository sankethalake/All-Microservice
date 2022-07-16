using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsEventsMicroService.Database
{
    public class Event
    {
        
        public int EventId { get; set; }
        [Required(ErrorMessage ="Required !")]
        public int SportId { get; set; }
        public virtual Sport Sport { get; set; }
        [Required(ErrorMessage = "Required !")] 
        public string EventName { get; set; }
        [Required(ErrorMessage = "Required !")]
        public int NoOfSlots { get; set; }
        [Required(ErrorMessage = "Required !")]
        public DateTime Date { get; set; }


    }
}
