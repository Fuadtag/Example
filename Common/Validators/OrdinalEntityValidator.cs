using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Validators
{
    public class OrdinalEntityValidator: IEntityValidator
    {
        public const int DefaultOrdinal = 100;

        /// <summary>
        ///     Validates if the ordinal of given entity overlaps with the ordinal of an existing entity.
        ///     If yes, changes the ordinal of the existing entity to default value.
        ///     This methods operates only when the ordinal of given entity is different than default value
        /// </summary>
        /// <param name="dbContext">Current database context</param>
        /// <param name="entity">Entity that ordinal of needs to be checked</param>
        /// <typeparam name="TEntity">BaseEntity, IOrdinalEntity</typeparam>
        public async Task ValidateOrFallback<TEntity>(IAppDbContext dbContext, TEntity entity)
            where TEntity : BaseEntity
        {
            if (entity is IOrdinalEntity ordinalEntity)
            {
                var entities = await dbContext.GetDbSet<TEntity>()
                    .Where(e => e.Id != entity.Id) // other than given entity
                    .Where(e => ((IOrdinalEntity)e).Ordinal == ordinalEntity.Ordinal) // if exists any
                    .ToListAsync();

                if (entities.Count > 0)
                {
                    foreach (var baseEntity in entities) ((IOrdinalEntity)baseEntity).Ordinal = DefaultOrdinal;

                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}