using GamesDataCollector.Entities;
using GamesDataCollector.Models;
using GamesDataCollector.Services;
using GamesDataCollector.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesDataCollector.Controllers
{
    [ApiController]
    [Route("[controller]")]
    /// <summary>
    /// Score Board Controller
    /// </summary>
    public class ScoresController : Controller
    {
        #region Fields
        private readonly IUsersService _usersService;
        private readonly IAppService _appService;
        private readonly IScoreBoardService _scoreBoardService;
        #endregion

        #region Ctor
        public ScoresController(IUsersService usersService, IAppService appService, IScoreBoardService scoreBoardService)
        {
            _usersService = usersService;
            _appService = appService;
            _scoreBoardService = scoreBoardService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Create a New Score
        /// </summary>
        /// <param name="score">ScoreBoard Api model</param>
        /// <param name="userid">User identifier</param>
        /// <param name="appid">Application identifier</param>
        /// <returns>Created score </returns>
        /// <response code="201">Returns the newly created score</response>
        /// <response code="400">If the item is null or duplicated</response> 
        /// <response code="500">Internal server error</response>
        [HttpPost("apps/{appid}/users/{userid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody]ApiScore score, Guid userid, Guid appid)
        {
            try
            {
                User user = _appService.CheckUserAndAppid(userid, appid);

                //Insert score
                Score tempScore = score.ToEntity<Score>();
                tempScore.User = user;
                var result = _scoreBoardService.InserScore(tempScore);
                return CreatedAtAction(nameof(Score), new { id = tempScore.Id }, tempScore.ToModel<ApiScore>());
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }



        /// <summary>
        /// Update Score and level by user id
        /// </summary>
        /// <param name="appid">Application identifier</param>
        /// <param name="userid">User identifier</param>
        /// <param name="score">Score</param>
        /// <response code="200">Update Success</response>
        /// <response code="400">If the item is null</response>
        /// <response code="404">If the item not found</response>
        [HttpPut("apps/{appid}/users/{userid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(Guid appid, Guid userid, [FromBody]ApiScore score)
        {
            _appService.CheckUserAndAppid(userid, appid);

            Score tempScore = score.ToEntity<Score>();
            
            _scoreBoardService.UpdateScoreByUserId(userid,tempScore);
            return Ok();
        }

        /// <summary>
        /// Get Score and level by user id
        /// </summary>
        /// <param name="appid">Application identifier</param>
        /// <param name="userid">User identifier</param>
        /// <response code="200">Returns score</response>
        /// <response code="400">If the item is null</response>
        /// <response code="404">If the item not found</response>
        [HttpGet("apps/{appid}/users/{userid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(Guid appid, Guid userid)
        {
            _appService.CheckUserAndAppid(userid, appid);

            Score tempScore = _scoreBoardService.GetScoreByUserId(userid).FirstOrDefault();
            if (tempScore == null)
                return NotFound();
            return Ok(tempScore.ToModel<ApiScore>());
        }
        #endregion
    }
}
