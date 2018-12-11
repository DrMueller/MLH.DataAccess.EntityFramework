using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mmu.Mlh.DataAccess.EntityFramework.TestApplication.Areas.DataAccess.DataModeling.TypeConfigurations
{
    public class IndividualConfiguration : IEntityTypeConfiguration<IndividualDataModel>
    {
        public void Configure(EntityTypeBuilder<IndividualDataModel> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(f => f.Birthdate).IsRequired();
            builder.Property(f => f.FirstName).IsRequired();
            builder.Property(f => f.LastName).IsRequired();

            builder.ToTable("Individual", "Core");
        }
    }
}