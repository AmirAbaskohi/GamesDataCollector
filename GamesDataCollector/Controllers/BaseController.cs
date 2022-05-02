using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesDataCollector.Entities;
using GamesDataCollector.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamesDataCollector.Controllers
{
    public class BaseController : Controller
    {
        #region Fields
        private readonly IUsersService _usersService;
        private readonly IAppService _appService;
        #endregion

        #region Ctor
        public BaseController(IUsersService usersService, IAppService appService)
        {
            _usersService = usersService;
            _appService = appService;
        }
        #endregion

    }
}