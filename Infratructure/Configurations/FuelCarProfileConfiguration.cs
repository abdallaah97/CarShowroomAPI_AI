using CarShowroomAPI_AI.Domian.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShowroomAPI_AI.Configurations
{
    public class FuelCarProfileConfiguration : IEntityTypeConfiguration<FuelCarProfile>
    {
        public void Configure(EntityTypeBuilder<FuelCarProfile> builder)
        {
            builder.Property(x => x.FuelType)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
