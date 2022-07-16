using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParticipationMicroservice.Model
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }

        [ForeignKey("SportId")]
        public int SportId { get; set; }
        public virtual Sport Sports { get; set; }

        [Required(ErrorMessage = "Player Name is required")]
        public string PlayerName { get; set; }

        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [DataType(DataType.PhoneNumber)]
        public string ContactNumber { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [EnumDataType(typeof(GenderLevel))]
        public GenderLevel Gender { get; set; }
    }
        public enum GenderLevel
        {
            Male = 1,
            Female = 2
        }
    
}
