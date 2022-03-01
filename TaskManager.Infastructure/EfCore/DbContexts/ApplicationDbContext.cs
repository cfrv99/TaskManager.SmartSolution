using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Core.Entities;
using TaskManager.DAL.Entities;
using TaskManager.Infastructure.Helpers;

namespace TaskManager.Infastructure.EfCore.DbContexts
{
    public partial class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext(DbContextOptions options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            var entries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted);
            foreach (var change in entries)
            {
                if (change.Entity is EntityBase)
                {
                    EntityBase entity = change.Entity as EntityBase;
                    if (change.State == EntityState.Added)
                    {
                        entity.CreatedDate = DateTime.Now;
                        entity.Creator = _currentUserService.GetCurrentUserId();
                    }
                    else if (change.State == EntityState.Modified)
                    {
                        entity.ModifiedDate = DateTime.Now;
                        entity.Modifier = _currentUserService.GetCurrentUserId();
                    }
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
