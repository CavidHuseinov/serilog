using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serilog.WebAPI.Entities;

namespace Serilog.WebAPI.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x=>x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
            builder.Property(x=>x.Description).IsRequired().HasMaxLength(20);
            builder.Property(x=>x.Price).IsRequired();
        }
    }
}
