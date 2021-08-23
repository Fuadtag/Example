using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Validators;
using Domain.Common;

namespace App.Common
{
    public class EntityValidator
    {
        public static readonly IEnumerable<IEntityValidator> Validators = GetEntityValidators();

        public static async Task ValidateOrFallback<TEntity>(IAppDbContext dbContext, TEntity entity)
            where TEntity : BaseEntity
        {
            foreach (var validator in GetEntityValidators())
                await validator.ValidateOrFallback(dbContext, entity);
        }

        private static IEnumerable<IEntityValidator> GetEntityValidators()
        {
            return new IEntityValidator[]
            {
                new OrdinalEntityValidator()
            };
        }
    }
}