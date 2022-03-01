using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Repository
{
    public interface IRepository<TEntity>
    {
        Task Add(TEntity entity);
        Task AddRange(List<TEntity> entities);
        Task Update(TEntity entity);
        Task Delete(int id);
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(int id);
        Task<int> Commit();
    }
}
