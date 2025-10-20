using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store_X.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_X.Persistence.Data.Configurations
{
    internal class PoductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name).HasColumnType("nvarchar").HasMaxLength(256);
            builder.Property(P => P.Description).HasColumnType("nvarchar").HasMaxLength(512);
            builder.Property(P => P.PictureUrl).HasColumnType("nvarchar").HasMaxLength(256);
            builder.Property(P => P.Price).HasColumnType("decimal(18,2)");

            builder.HasOne(P => P.Brand)
                   .WithMany()
                   .HasForeignKey(P => P.BrandId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(P => P.Type)
                   .WithMany()
                   .HasForeignKey(P => P.TypeId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
