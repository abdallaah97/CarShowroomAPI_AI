using CarShowroomAPI_AI.Domian.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShowroomAPI_AI.Configurations
{
    public class ElectricCarProfileConfiguration : IEntityTypeConfiguration<ElectricCarProfile>
    {
        public void Configure(EntityTypeBuilder<ElectricCarProfile> builder)
        {
            builder.Property(x => x.ChargingPort)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
