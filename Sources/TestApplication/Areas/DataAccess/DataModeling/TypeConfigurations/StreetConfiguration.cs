using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.DataModeling.TypeConfigurations
{
    public class StreetConfiguration : IEntityTypeConfiguration<StreetDataModel>
    {
        public void Configure(EntityTypeBuilder<StreetDataModel> builder)
        {
            builder.HasKey(f => new { f.AddressId, f.StreetName, f.StreetNumber });

            builder.Property(f => f.AddressId).IsRequired();
            builder.Property(f => f.StreetName).IsRequired();
            builder.Property(f => f.StreetNumber).IsRequired();

            builder.HasOne(f => f.Address).WithMany(f => f.Streets).IsRequired();

            builder.ToTable("Street", "Core");
        }
    }
}