using GamesDataCollector.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesDataCollector.Data
{
    public class AppDbContext: DbContext
    {
        #region Ctor
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        #endregion

        #region Tables
        public DbSet<Application> Applications { get; set; }
        public DbSet<GameData> GameData { get; set; }
        public DbSet<Parent> Parent { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Score> Scores { get; set; }
        #endregion
    }
}
