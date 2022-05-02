using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GamesDataCollector.Entities
{
    public class Parent : BaseEntity
    {
        [Required]
        public int Id { get; set; }

        public int FatherAge { get; set; }

        public int MotherAge { get; set; }

        public string MotherEducation { get; set; }

        public string FatherEducation { get; set; }

        public string MotherJob { get; set; }

        public string FatherJob { get; set; }

        [Required]
        public Guid? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
