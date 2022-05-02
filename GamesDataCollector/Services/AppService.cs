using GamesDataCollector.Data;
using GamesDataCollector.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace GamesDataCollector.Services
{
    public class AppService : IAppService
    {
        #region Fields
        private readonly IRepository<Application> _appRepository;
        private readonly IUsersService _usersService;
        #endregion

        #region Ctor
        public AppService(IUsersService usersService, IRepository<Application> appRepository)
        {
            _usersService = usersService;
            _appRepository = appRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get application by identifier return null if wasnot exist
        /// </summary>
        /// <param name="appId">application identifier</param>
        /// <returns></returns>
        public Application GetAppById(Guid appId)
        {
            var schedule = _appRepository.GetById(appId);
            return schedule;
        }

        public User CheckUserAndAppid(Guid userid, Guid appid)
        {
            User user = _usersService.GetUserById(userid);
            //Check user id
            if (userid == null || user == null)
                throw new Exception($" Wrong User id");

            //Check application id
            Application app = GetAppById(appid);
            if (appid == null || app == null)
                throw new Exception($"Wrong application id");


            //Check application id
            if (user.AppId == null || GetAppById((Guid)user.AppId) == null)
                throw new Exception($"Wrong application id");

            return user;
        }
        #endregion
    }
}
