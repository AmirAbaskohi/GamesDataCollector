using AutoMapper;
using GamesDataCollector.Entities;
using GamesDataCollector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesDataCollector.Tools
{
    /// <summary>
    /// AutoMapper configuration for admin area models
    /// </summary>
    public class MappingEntity : AutoMapper.Profile
    {
        public MappingEntity()
        { 
            //To Model
            CreateMap<ApiUser, User>();
            CreateMap<ApiParent, Parent>();
            CreateMap<ApiProfile, Entities.Profile>();
            CreateMap<ApiData, GameData>();
            CreateMap<ApiScore, Score>();

            //ToEntity
            CreateMap<User, ApiUser>();
            CreateMap<Parent, ApiParent>();
            CreateMap<Entities.Profile, ApiProfile>();
            CreateMap<GameData, ApiData>();
            CreateMap<Score, ApiScore>();

        }
    }
}
