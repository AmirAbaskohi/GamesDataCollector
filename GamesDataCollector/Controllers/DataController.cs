using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GamesDataCollector.Entities;
using GamesDataCollector.Models;
using GamesDataCollector.Services;
using GamesDataCollector.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamesDataCollector.Controllers
{

    [ApiController]
    [Route("[controller]")]
    /// <summary>
    /// Data Controller
    /// </summary>
    public class DataController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IDataService _dataService;
        private readonly IUsersService _usersService;
        private readonly IAppService _appService;

        public DataController(IUsersService usersService, IFileService fileService, IDataService dataService, IAppService appService)
        {
            _fileService = fileService;
            _dataService = dataService;
            _usersService = usersService;
            _appService = appService;
        }

        /// <summary>
        /// Insert file and data into database
        /// </summary>
        /// <param name="file">File data</param>
        /// <param name="location">relative directory for save file</param>
        /// <param name="rawdata">Raw data</param>
        /// <param name="userid">user identifier of type Guid</param>
        /// <param name="appid">application identifier of type Guid</param>
        /// <returns>Game Data</returns>
        /// <response code="201">Returns the newly created Game Data object </response>
        /// <response code="400">user id or app id is null or saving file problem</response> 
        /// <response code="500">Internal server error</response>
        [HttpPost("apps/{appid}/users/{userid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(IFormFile file, [FromForm] string location, [FromForm] string rawdata, Guid userid, Guid appid )
        {
            try
            {

                User user = _appService.CheckUserAndAppid(userid, appid);
                //TODO
                Application app = _appService.GetAppById(appid);

                //Check user is for given app
                if (user.AppId != appid)
                    return BadRequest($"Error: User not for the app");

                //save file
                GameData data;
                if (file != null)
                {
                    string fileLoc = (location == null || location == "") ? app.Name + "\\" + userid : app.Name + "\\" + userid + "\\" + location;
                    _fileService.SaveFileAsync(file, fileLoc);

                    //save data file exist
                     data= _dataService.InsertData(appid, userid, location, rawdata, FileService.SizeConverter(file.Length), file.Name);
                }
                else
                {
                    //save data no file
                    data = _dataService.InsertData(appid, userid, "", rawdata, "0", "");
                }

                return CreatedAtAction(nameof(User), new { id = data.Id }, data.ToModel<ApiData>());
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

        /// <summary>
        /// get file and data of specific user
        /// </summary>
        /// <param name="userid">user identifier of type guid</param>
        /// <param name="appid">application identifier of type guid</param>
        /// <param name="startDate">start date filter</param>
        /// <param name="endDate">end date filter</param>
        /// <returns>game data</returns>
        /// <response code="200">return wanted data </response>
        /// <response code="400">user id or app id is null or saving file problem</response> 
        /// <response code="500">internal server error</response>
        [HttpGet("apps/{appid}/users/{userid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetUserGameData(Guid userid, Guid appid, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                User user = _usersService.GetUserById(userid);
                //check user id
                if (userid == null || user == null)
                    return BadRequest($"Error: Wrong user id");

                //check application id
                Application app = _appService.GetAppById(appid);
                if (appid == null || app == null)
                    return BadRequest($"Error: Wrong application id");

                //check user is for given app
                if (user.AppId != appid)
                    return BadRequest($"Error: User not for the app");

                //get user game data entities
                var userGameData = _dataService.GetUserData(userid, appid);

                //filter by start date and end date
                if (startDate != null)
                    userGameData = userGameData.Where(data => data.Date >= startDate).ToList();

                if (endDate != null)
                    userGameData = userGameData.Where(data => data.Date <= endDate).ToList();

                return Ok(userGameData.Select(data => new { data.Id, data.RawData, data.FileName, data.FileSize, data.Location, data.Date, data.UserId, data.AppId }));
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

        /// <summary>
        /// get file and data of specific app
        /// </summary>
        /// <param name="appid">application identifier of type guid</param>
        /// <param name="startDate">start date filter</param>
        /// <param name="endDate">end date filter</param>
        /// <returns>game data</returns>
        /// <response code="200">return wanted data </response>
        /// <response code="400">user id or app id is null or saving file problem</response> 
        /// <response code="500">internal server error</response>
        [HttpGet("apps/{appid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAppGameData(Guid appid, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                //check application id
                Application app = _appService.GetAppById(appid);
                if (appid == null || app == null)
                    return BadRequest($"Error: Wrong application id");

                //get user game data entities
                var userGameData = _dataService.GetAppData(appid);

                //filter by start date and end date
                if (startDate != null)
                    userGameData = userGameData.Where(data => data.Date >= startDate).ToList();

                if (endDate != null)
                    userGameData = userGameData.Where(data => data.Date <= endDate).ToList();

                return Ok(userGameData.Select(data => new { data.Id, data.RawData, data.FileName, data.FileSize, data.Location, data.Date, data.UserId, data.AppId }));
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

        /// <summary>
        /// get specific data file
        /// </summary>
        /// <param name="userid">user identifier of type guid</param>
        /// <param name="appid">application identifier of type guid</param>
        /// <param name="dataid">game data identifier</param>
        /// <returns>game data file</returns>
        /// <response code="200">return wanted data </response>
        /// <response code="400">user id or app id is null or saving file problem</response> 
        /// <response code="500">internal server error</response>
        [HttpGet("apps/{appid}/users/{userid}/gamedatafile/{dataid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetGameDataFile(Guid userid, Guid appid, int dataid)
        {
            try
            {
                User user = _usersService.GetUserById(userid);
                //check user id
                if (userid == null || user == null)
                    return BadRequest($"Error: Wrong user id");

                //check application id
                Application app = _appService.GetAppById(appid);
                if (appid == null || app == null)
                    return BadRequest($"Error: Wrong application id");

                //check user is for given app
                if (user.AppId != appid)
                    return BadRequest($"Error: User not for the app");

                //get wanted data entities
                var data = _dataService.GetData(dataid);
                if (data == null || data.AppId != appid || data.UserId != userid)
                    return BadRequest($"Error: Wrong game data id");

                //check file existance
                if (data.Location == null || data.FileName == null)
                    return BadRequest($"Error: This Data does not have file");

                var address = Path.Combine(Directory.GetCurrentDirectory(), $"uploads\\" + app.Name + "\\" + userid + "\\" + data.Location + "\\" + data.FileName);
                var memory = new MemoryStream();
                using (var stream = new FileStream(address, FileMode.Open))
                {
                    stream.CopyTo(memory);
                }
                memory.Position = 0;
                return File(memory, "application/" + data.FileName.Split('.')[data.FileName.Split('.').Length - 1], Path.GetFileName(data.FileName));

            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

        /// <summary>
        /// get specific data
        /// </summary>
        /// <param name="userid">user identifier of type guid</param>
        /// <param name="appid">application identifier of type guid</param>
        /// <param name="dataid">game data identifier</param>
        /// <returns>game data</returns>
        /// <response code="200">return wanted data </response>
        /// <response code="400">user id or app id is null or saving file problem</response> 
        /// <response code="500">internal server error</response>
        [HttpGet("apps/{appid}/users/{userid}/gamedata/{dataid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetGameData(Guid userid, Guid appid, int dataid)
        {
            try
            {
                User user = _usersService.GetUserById(userid);
                //check user id
                if (userid == null || user == null)
                    return BadRequest($"Error: wrong user id");

                //check application id
                Application app = _appService.GetAppById(appid);
                if (appid == null || app == null)
                    return BadRequest($"Error: wrong application id");

                //check user is for given app
                if (user.AppId != appid)
                    return BadRequest($"Error: user not for the app");

                //get wanted data entities
                var data = _dataService.GetData(dataid);
                if (data == null || data.AppId != appid || data.UserId != userid)
                    return BadRequest($"Error: wrong game data id");

                return Ok(new { data.Id, data.RawData, data.FileName, data.FileSize, data.Location, data.Date, data.UserId, data.AppId });

            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }
    }
}