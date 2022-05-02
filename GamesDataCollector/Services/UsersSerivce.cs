using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesDataCollector.Data;
using GamesDataCollector.Entities;
using GamesDataCollector.Models;

namespace GamesDataCollector.Services
{
    public class UsersSerivce : IUsersService
    {

        #region Fields
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Profile> _profileRepository;
        private readonly IRepository<Parent> _parentRepository;

        #endregion

        #region Ctor
        public UsersSerivce(IRepository<User> userRepository, IRepository<Profile> profileRepository, IRepository<Parent> parentRepository)
        {
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _parentRepository = parentRepository;
        }
        #endregion
        public void Detach(User userObj)
        {
            _userRepository.Detach(userObj);
        }
        

        #region Methods
        public User GetUserById(Guid userId)
        {
            return _userRepository.GetById(userId);
        }

        public User InsertUser(User user)
        {
            _userRepository.Insert(user);
            return user;
        }

        public User GetUserByAppIdAndUserName(Guid appId, string userName)
        {
            return _userRepository.List().FirstOrDefault(user => user.AppId == appId && user.UserName == userName);
        }

        public void UpdateUser(User baseUser,User updateUser)
        {
            baseUser = UpdateNonNullValues<User>(updateUser, baseUser);
            baseUser.Profile = UpdateNonNullValues<Profile>(updateUser.Profile, baseUser.Profile);
            baseUser.Parent = UpdateNonNullValues<Parent>(updateUser.Parent, baseUser.Parent);

            _userRepository.Update(baseUser);
        }

        private T UpdateNonNullValues<T>( T updateEntity, T baseEntity) where T : BaseEntity
        {
            if (updateEntity != null && baseEntity != null)
                // For each property in the model
                foreach (var p in typeof(T).GetProperties())
                {
                    // Get the value of the property
                    var v = p.GetValue(updateEntity, null);

                    // Assume null means that the property wasn't passed from the client
                    if (v != null && p.Name != "Id")
                        p.SetValue(baseEntity, p.GetValue(updateEntity));

                }
            else
                if (updateEntity != null)
                    baseEntity = updateEntity;
            return baseEntity;
        }

        public void UpdateUserActivity(Guid userId)
        {
            User user = GetUserById(userId);
            user.LastActivity = DateTime.Now;
            _userRepository.Update(user);
        }
        #endregion

    }
}
