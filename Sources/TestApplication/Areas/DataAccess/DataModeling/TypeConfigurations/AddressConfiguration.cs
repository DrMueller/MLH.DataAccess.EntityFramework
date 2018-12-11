using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.DataModeling.TypeConfigurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<AddressDataModel>
    {
        public void Configure(EntityTypeBuilder<AddressDataModel> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(f => f.Individual).WithMany(f => f.Addresses).IsRequired();

            builder.ToTable("Address", "Core");
        }
    }
}