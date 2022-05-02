using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesDataCollector.Data;
using GamesDataCollector.Entities;

namespace GamesDataCollector.Services
{
    public class ScoreBoardService : IScoreBoardService
    {
        #region Fields
        private readonly IRepository<Score> _scoreRepository;

        #endregion

        #region Ctor
        public ScoreBoardService(IRepository<Score> scoreRepository)
        {
            _scoreRepository = scoreRepository;
        }
        #endregion
        public Score InserScore(Score score)
        {
            _scoreRepository.Insert(score);
            return score;
        }

        public void UpdateScoreByUserId(Guid userid, Score score)
        {
            Score old = GetScoreByUserId(userid).FirstOrDefault();
            if (old == null)
                throw new Exception($"User has no score");
            old.ScoreVal = score.ScoreVal;
            old.Level = score.Level;
            _scoreRepository.Update(old);
        }

        public IQueryable<Score> GetScoreByUserId(Guid userId)
        {
            var query = from b in _scoreRepository.Table()
                        where b.UserId == userId
                        select b;
            return query;
        }
    }
}
