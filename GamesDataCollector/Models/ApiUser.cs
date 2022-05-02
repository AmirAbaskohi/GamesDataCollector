using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GamesDataCollector.Models
{
    /// <summary>
    /// User Api Model
    /// </summary>
    public class ApiUser
    {
        /// <summary>
        /// User identifier
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// User application identifier
        /// </summary>
        [Required]
        public Guid AppId { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Password (notice: not hashed you can hash it in your program and send it to the server)
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        public string Email { set; get; }

        /// <summary>
        /// Parent object
        /// </summary>
        public ApiParent Parent { set; get; }

        /// <summary>
        /// Profile object
        /// </summary>
        public ApiProfile Profile  { set; get; }
    }
}
