using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GamesDataCollector.Entities
{
    public class Profile : BaseEntity
    {
        [Required]
        public int Id { get; set; }

        [Phone]
        public string Phone { get; set; }

        public string PhysicalDisease { get; set; }

        public string MentalDisease { get; set; }
        
        [Range(0,150)]
        public int Age { get; set; }

        public string Birthday { get; set; }

        public string Education { get; set; }
        
        public bool RightHand { get; set; }
        
        public bool Man { get; set; }

        [Required]
        public Guid? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
