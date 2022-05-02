using GamesDataCollector.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesDataCollector.Services
{
    public interface IAppService
    {
        User CheckUserAndAppid(Guid userid, Guid appid);
        Application GetAppById(Guid appId);
    }
}
