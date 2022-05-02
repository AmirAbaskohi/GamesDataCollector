using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesDataCollector.Models
{
    /// <summary>
    /// Game Data Model
    /// </summary>
    public class ApiData
    {
        /// <summary>
        /// Data identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Database storage Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Raw Data
        /// </summary>
        public string RawData { get; set; }

        /// <summary>
        /// Name of file
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Size of file
        /// </summary>
        public string FileSize { get; set; }

        /// <summary>
        /// File location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// User identifier
        /// </summary>
        public Guid? UserId { get; set; }

    }
}
