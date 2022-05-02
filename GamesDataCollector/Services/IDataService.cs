using GamesDataCollector.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesDataCollector.Services
{
    /// <summary>
    /// Game Data Service
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Create and insert a GameData
        /// </summary>
        /// <param name="appid">App identifier</param>
        /// <param name="userid">User identifier</param>
        /// <param name="location">Location</param>
        /// <param name="rawdata">Raw game data</param>
        /// <param name="filesize">size of stored file</param>
        /// <param name="fileName">Name of file</param>
        /// <returns>Game data</returns>
        GameData InsertData(Guid appid, Guid userid, string location, string rawdata, string filesize, string fileName);

        /// <summary>
        /// Return user game data
        /// </summary>
        /// <param name="appid">App identifier</param>
        /// <param name="userid">User identifier</param>
        /// <returns>Game data</returns>
        List<GameData> GetUserData(Guid userid, Guid appid);

        /// <summary>
        /// Return app game data
        /// </summary>
        /// <param name="appid">App identifier</param>
        /// <returns>Game data</returns>
        List<GameData> GetAppData(Guid appid);

        /// <summary>
        /// Return user game data
        /// </summary>
        /// <param name="dataid">App identifier</param>
        /// <returns>Game data</returns>
        GameData GetData(int dataid);
    }
}
