using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GamesDataCollector.Entities
{
    public class Application: BaseEntity
    {
        private ICollection<User> _users;
        [Required]
        public Guid ApplicationId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string DeveloperName { get; set; }

        
        /// <summary>
        /// Gets or sets the users
        /// </summary>
        public virtual ICollection<User> Users
        {
            get => _users ?? (_users = new List<User>());
            protected set => _users = value;
        }
    }
}
