using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GamesDataCollector.Entities
{
    public class Score : BaseEntity
    {
        [Required]
        public int Id { get; set; }

        public int ScoreVal { get; set; }

        public int Level { get; set; }

        [Required]
        public Guid? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
