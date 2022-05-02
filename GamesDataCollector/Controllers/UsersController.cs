using GamesDataCollector.Entities;
using GamesDataCollector.Models;
using GamesDataCollector.Services;
using GamesDataCollector.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    /// User Controller
    /// </summary>
    public class UsersController : Controller
    {
        #region Fields
        private readonly IUsersService _usersService;
        private readonly IAppService _appService;

        #endregion

        #region Ctor
        public UsersController(IUsersService usersService, IAppService appService)
        {
            _usersService = usersService;
            _appService = appService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Create a New User
        /// </summary>
        /// <param name="user">User Api model</param>
        /// <returns>User</returns>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">If the item is null or duplicated</response> 
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody]ApiUser user)
        {
            try
            {
                //Null user
                if (user == null || user.Id==null)
                    return BadRequest($"Error: User is null");

                //Duplicate user
                var userM = _usersService.GetUserById(user.Id);
                if (userM != null)
                    return BadRequest($"Error: User exist in database");

                userM = _usersService.GetUserByAppIdAndUserName(user.AppId, user.UserName);
                if (userM != null)
                    return BadRequest($"Error: User exist with such username in given app");




                //Check application id
                if (user.AppId == null || _appService.GetAppById(user.AppId)==null)
                    return BadRequest($"Error: Wrong application id");

                //Insert user
                User tempUser = user.ToEntity<User>();
                tempUser.FirstActivity = DateTime.Now;
                tempUser.LastActivity = DateTime.Now;
                var result = _usersService.InsertUser(tempUser);

                var passwordHasher = new PasswordHasher<User>();
                User tUser = user.ToEntity<User>();
                User userObj = _usersService.GetUserById(user.Id);
                tUser.Id = user.Id;
                _usersService.Detach(tUser);
                tUser.Password = passwordHasher.HashPassword(tUser, user.Password);
                _usersService.UpdateUser(userObj, tUser);

                var response = tempUser.ToModel<ApiUser>();
                response.Password = user.Password;
                return CreatedAtAction(nameof(User), new { id = tempUser.Id }, response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update User Info. You can send only parameters you want to update
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="user">User Api model</param>
        /// <response code="200">Update Succeess</response>
        /// <response code="400">If the item is null</response>
        /// <response code="404">If the item not found</response>
        /// <response code="500">Internal server error</response>
        /// <returns>Updated user</returns>
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(Guid id, [FromBody]ApiUser user)
        {
            try
            {
                //Null user
                if (id == null || user == null)
                    return BadRequest($"Error: User is null");
                
                //Check user id
                User userObj = _usersService.GetUserById(id);
                if (userObj == null)
                    return NotFound($"Error: User not found");

                //Check id and user.id
                if (user.Id != id)
                    return BadRequest($"Error: Id is not for the user");

                //Check application id
                if (user.AppId == null || _appService.GetAppById(user.AppId) == null)
                    return BadRequest($"Error: Wrong application id");

                //Duplicate user
                if (userObj.UserName != user.UserName && _usersService.GetUserByAppIdAndUserName(user.AppId, user.UserName) != null)
                    return BadRequest($"Error: User exist with such username in given app");

                var passwordHasher = new PasswordHasher<User>();
                User tempUser = user.ToEntity<User>();
                tempUser.Id = id;
                _usersService.Detach(tempUser);
                tempUser.Password = passwordHasher.HashPassword(userObj, user.Password);
                _usersService.UpdateUser(userObj,tempUser);
                _usersService.UpdateUserActivity(tempUser.Id);

                var response = userObj.ToModel<ApiUser>();
                response.Password = user.Password;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get User Info
        /// </summary>
        /// <param name="appid">AppId</param>
        /// <param name="userName">UserName</param>
        /// <param name="password">Password</param>
        /// <response code="200">return wanted user </response>
        /// <response code="400">app id or username or password is null</response> 
        /// <response code="500">internal server error</response>
        /// <returns>Get user</returns>
        [HttpGet("apps/{appid}/users/{userName}/{password}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(Guid appid, string userName, string password)
        {
            //Null info
            if (userName == null || password == null)
                BadRequest($"Error: Null username or password");

            //App not found
            if (appid == null || _appService.GetAppById(appid) == null)
                return BadRequest($"Error: Wrong application id");

            var userObj = _usersService.GetUserByAppIdAndUserName(appid, userName);

            //User not found
            if (userObj == null)
                return BadRequest($"Error: There is no user with such username and given app");

            //Check password
            var passwordHasher = new PasswordHasher<User>();

            if (passwordHasher.VerifyHashedPassword(userObj, userObj.Password, password) == PasswordVerificationResult.Failed)
                return BadRequest($"Error: Wrong password");

            var response = userObj.ToModel<ApiUser>();
            response.Password = password;
            return Ok(response);
        }

        #endregion
    }
}
