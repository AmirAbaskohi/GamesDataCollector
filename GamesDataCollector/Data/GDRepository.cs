using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesDataCollector.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamesDataCollector.Data
{
    /// <summary>
    /// Represents the Entity Framework  game data repository
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public class GDRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region Fields
        private readonly AppDbContext _dbContext;
        #endregion

        #region Ctor
        public GDRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public T GetById(object id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        /// <summary>
        /// Get entities list
        /// </summary>
        /// <returns>List of Entities</returns>
        public List<T> List()
        {
            return _dbContext.Set<T>().ToList();
        }

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Inserted Entity</returns>
        public T Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        public void Insert(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public DbSet<T> Table()
        {
            return _dbContext.Set<T>();
        }

        DbSet<T> IRepository<T>.Table()
        {
            return _dbContext.Set<T>();
        }

        public void Detach(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }
        #endregion
    }
}

