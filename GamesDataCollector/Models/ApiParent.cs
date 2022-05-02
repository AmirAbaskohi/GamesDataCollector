using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesDataCollector.Models
{
    /// <summary>
    /// Parent model
    /// </summary>
    public class ApiParent
    {
        /// <summary>
        /// User father age
        /// </summary>
        public int FatherAge { get; set; }

        /// <summary>
        /// User mother age
        /// </summary>
        public int MotherAge { get; set; }

        /// <summary>
        /// User mother education
        /// </summary>
        public string MotherEducation { get; set; }

        /// <summary>
        /// User father education
        /// </summary>
        public string FatherEducation { get; set; }

        /// <summary>
        /// User mother job
        /// </summary>
        public string MotherJob { get; set; }

        /// <summary>
        /// User father job
        /// </summary>
        public string FatherJob { get; set; }
    }
}
