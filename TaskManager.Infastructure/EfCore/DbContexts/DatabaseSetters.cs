using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.DAL.Entities;

namespace TaskManager.Infastructure.EfCore.DbContexts
{
    public partial class ApplicationDbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Organization> MyProperty { get; set; }
    }
}
