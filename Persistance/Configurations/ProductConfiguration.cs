using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistance.Common;

namespace Persistance.Configurations
{
    public class ProductConfiguration:IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ConfigureAuditingEntity();

            builder.Property(p => p.Name).HasMaxLength(255);

            builder.Property(p => p.Barcode).IsRequired();

            builder.Property(p => p.Price).IsRequired().HasColumnType("money");

            builder.ToTable("Products");
        }
    }
}