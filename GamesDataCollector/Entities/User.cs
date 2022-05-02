using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GamesDataCollector.Entities
{
    public class User : BaseEntity
    {
        [Required(ErrorMessage = "Identifier is required")]
        public Guid Id { get; set; }

        public string UserName { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Password { get; set; }
        
        public DateTime LastActivity { set; get; }
        
        public DateTime FirstActivity { set; get; }

        
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { set; get; }

        public virtual Parent Parent { get; set; }
        
        public virtual Profile Profile { get; set; }

        public Guid? AppId { get; set; }

        [ForeignKey("AppId")]
        public virtual Application App { get; set; }
    }
}
