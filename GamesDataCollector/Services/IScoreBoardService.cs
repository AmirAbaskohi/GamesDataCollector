using GamesDataCollector.Entities;
using System;

namespace GamesDataCollector.Services
{
    public interface IScoreBoardService
    {
        System.Linq.IQueryable<Score> GetScoreByUserId(System.Guid userId);
        Score InserScore(Score tempScore);
        void UpdateScoreByUserId(Guid userid, Score score);
    }
}