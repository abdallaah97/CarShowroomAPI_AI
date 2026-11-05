using CarShowroomAPI_AI.Domian.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShowroomAPI_AI.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {

            builder.Property(x => x.Model)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Vin)
                .IsRequired()
                .HasMaxLength(17);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");

            builder.HasIndex(x => x.Vin).IsUnique();
            builder.HasIndex(x => x.FuelProfileId).IsUnique();
            builder.HasIndex(x => x.ElectricProfileId).IsUnique();

            builder.HasOne(x => x.FuelProfile)
                .WithMany()
                .HasForeignKey(x => x.FuelProfileId);

            builder.HasOne(x => x.ElectricProfile)
                .WithMany()
                .HasForeignKey(x => x.ElectricProfileId);

            builder.HasOne(x => x.Branch)
                .WithMany(x => x.Cars)
                .HasForeignKey(x => x.BranchId);
        }
    }
}
