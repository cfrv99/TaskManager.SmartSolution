using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Entities;
using TaskManager.Core.Repository;
using TaskManager.Infastructure.EfCore.DbContexts;

namespace TaskManager.Infastructure.EfCore
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly ApplicationDbContext _context;

        public EfRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var data = await GetById(id);
            _context.Set<TEntity>().Remove(data);
        }

        public IQueryable<TEntity> GetAll()
        {
            var data = _context.Set<TEntity>();
            return data;
        }

        public async Task<TEntity> GetById(int id)
        {
            var data = await _context.Set<TEntity>().FindAsync(id);
            return data;
        }

        public async Task Update(TEntity entity)
        {
            if (entity.Id <= 0)
                throw new ArgumentNullException("Id can not be null");

            _context.Set<TEntity>().Update(entity);
            await Commit();
        }
        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task AddRange(List<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
        }
    }
}
