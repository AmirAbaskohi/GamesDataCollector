using GamesDataCollector.Data;
using GamesDataCollector.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Game Data Service
/// </summary>
namespace GamesDataCollector.Services
{
    public class DataService: IDataService
    {
        #region Fields
        private readonly IRepository<GameData> _dataRepository;

        #endregion

        #region Ctor
        public DataService(IRepository<GameData> dataRepository)
        {
            _dataRepository = dataRepository; 
        }
        #endregion
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
        public GameData InsertData(Guid appid, Guid userid, string location, string rawdata, string filesize, string fileName)
        {
            GameData data = new GameData();
            data.AppId = appid;
            data.Date = DateTime.Now;
            data.Location = location;
            data.RawData = rawdata;
            data.UserId = userid;
            data.FileSize = filesize;
            data.FileName = fileName;

            _dataRepository.Insert(data);
            return data;

        }

        /// <summary>
        /// Return user game data
        /// </summary>
        /// <param name="appid">App identifier</param>
        /// <param name="userid">User identifier</param>
        /// <returns>Game data</returns>
        public List<GameData> GetUserData(Guid userid, Guid appid)
        {
            return _dataRepository.List().Where(data => data.AppId == appid && data.UserId == userid).ToList();
        }

        /// <summary>
        /// Return app game data
        /// </summary>
        /// <param name="appid">App identifier</param>
        /// <returns>Game data</returns>
        public List<GameData> GetAppData(Guid appid)
        {
            return _dataRepository.List().Where(data => data.AppId == appid).ToList();
        }

        /// <summary>
        /// Return user game data
        /// </summary>
        /// <param name="dataid">App identifier</param>
        /// <returns>Game data</returns>
        public GameData GetData(int dataid)
        {
            return _dataRepository.GetById(dataid);
        }

    }
}
