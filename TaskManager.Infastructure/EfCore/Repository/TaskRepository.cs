using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.Core.Repository;
using TaskManager.DAL.Entities;
using TaskManager.Infastructure.EfCore;
using TaskManager.Infastructure.EfCore.DbContexts;

namespace TaskManager.DAL.Repository
{
    public interface ITaskRepository : IRepository<Task>
    {
        //System.Threading.Tasks.Task AddListOfTask
    }
    public class TaskRepository : EfRepository<Task>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
