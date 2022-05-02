using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GamesDataCollector.Entities
{
    public class GameData : BaseEntity

    {
        [Required]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string RawData { get; set; }

        public string FileName { get; set; }

        public string FileSize { get; set; }
        
        public string Location { get; set; }

        [Required]
        public Guid? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public Guid? AppId { get; set; }

        [ForeignKey("AppId")]
        public virtual Application App { get; set; }
    }
}
