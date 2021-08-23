using System;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Common
{
    public static class ConfigurationFactory
    {
        public static EntityTypeBuilder<TEntity> ConfigureBaseEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : BaseEntity
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.SoftDeleted).IsRequired().HasDefaultValue(false);

            builder.HasQueryFilter(e => !e.SoftDeleted);

            return builder;
        }

        public static EntityTypeBuilder<TEntity> ConfigureAuditingEntity<TEntity>(
            this EntityTypeBuilder<TEntity> builder)
            where TEntity : AuditingEntity
        {
            builder.ConfigureBaseEntity();

            builder.Property(e => e.CreatedBy).IsRequired();
            builder.Property(e => e.CreatedAt).IsRequired()
                .ValueGeneratedOnAdd()
                .HasDefaultValue(DateTime.Now);

            builder.Property(e => e.LastModifiedBy).IsRequired();
            builder.Property(e => e.LastModifiedAt).IsRequired()
                .ValueGeneratedOnAdd()
                .HasDefaultValue(DateTime.Now);

            return builder;
        }
    }
}