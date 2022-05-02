using GamesDataCollector.Entities;
using GamesDataCollector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesDataCollector.Services
{
    /// <summary>
    /// User Service
    /// </summary>
    public interface IUsersService
    {
        User InsertUser(User user);

        void UpdateUser(User baseUser, User updateUser);

        void UpdateUserActivity(Guid userId);

        User GetUserById(Guid userId);

        User GetUserByAppIdAndUserName(Guid appId, string userName);

        void Detach(User userObj);
    }
}