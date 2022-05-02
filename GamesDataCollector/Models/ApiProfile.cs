using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesDataCollector.Models
{
    /// <summary>
    /// Profile model
    /// </summary>
    public class ApiProfile
    {
        /// <summary>
        /// Phone number
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// User physical disease
        /// </summary>
        public string PhysicalDisease { get; set; }

        /// <summary>
        /// User mental disease
        /// </summary>
        public string MentalDisease { get; set; }

        /// <summary>
        /// Age integer value
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Birth day date
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// User education
        /// </summary>
        public string Education { get; set; }

        /// <summary>
        /// Is user right hand or not?
        /// </summary>
        public bool RightHand { get; set; }

        /// <summary>
        /// Is user man or not?
        /// </summary>
        public bool Man { get; set; }

    }
}
