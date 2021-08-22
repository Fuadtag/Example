using System.Threading;
using System.Threading.Tasks;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Common
{
    public interface IAppDbContext
    {
        public DatabaseFacade GetDatabase();
        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : BaseEntity;
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
    }
}