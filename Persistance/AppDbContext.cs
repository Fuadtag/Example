using System;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Persistance
{
    public class AppDbContext:DbContext, IAppDbContext
    {
        // private readonly ICurrentUserService _currentUserService;

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        // public AppDbContext(DbContextOptions options, ICurrentUserService currentUserService) : base(options)
        // {
        //     _currentUserService = currentUserService;
        // }

        public DatabaseFacade GetDatabase()
        {
            return base.Database;
        }

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : BaseEntity
        {
            return Set<TEntity>();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<AuditingEntity>())
            {
                // var userId = _currentUserService.UserId;
                var utcNow = DateTime.Now;

                switch (entry.State)
                {
                    case EntityState.Added:
                        // entry.Entity.CreatedBy = userId;
                        entry.Entity.CreatedAt = utcNow;
                        // entry.Entity.LastModifiedBy = userId;
                        entry.Entity.LastModifiedAt = utcNow;
                        break;
                    case EntityState.Modified:
                        entry.Property(e => e.CreatedBy).IsModified = false;
                        entry.Property(e => e.CreatedAt).IsModified = false;
                        // entry.Entity.LastModifiedBy = userId;
                        entry.Entity.LastModifiedAt = utcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // make sure PostGIS extension is installed in database
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            
        }
    }
}