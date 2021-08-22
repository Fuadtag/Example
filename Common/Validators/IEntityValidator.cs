using System.Threading.Tasks;
using Domain.Common;

namespace Common.Validators
{
    public interface IEntityValidator
    {
        public Task ValidateOrFallback<TEntity>(IAppDbContext dbContext, TEntity entity) where TEntity : BaseEntity;

    }
}