using Api.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Common;

namespace Api.Infrastructure
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category), CatalogContext.DefaultSchema);
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasConversion(x => x.Value, l => new EntityId(l));
            builder.OwnsOne(c => c.Name).Property(p => p.Value).HasColumnName("Name").IsRequired();
            builder.OwnsOne(c => c.Description).Property(p => p.Value).HasColumnName("Description").IsRequired();
            builder.Property<DateTime>("CreateDate");
            builder.Property<DateTime>("UpdateDate");
            builder.Property<DateTime?>("DeleteDate");
        }
    }
}
