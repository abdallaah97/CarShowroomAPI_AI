using CarShowroomAPI_AI.Domian.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShowroomAPI_AI.Configurations
{
    public class CarFeatureConfiguration : IEntityTypeConfiguration<CarFeature>
    {
        public void Configure(EntityTypeBuilder<CarFeature> builder)
        {
            builder.HasIndex(x => new { x.CarId, x.FeatureId }).IsUnique();

            builder.HasOne(x => x.Car)
                .WithMany(x => x.CarFeatures)
                .HasForeignKey(x => x.CarId);

            builder.HasOne(x => x.Feature)
                .WithMany(x => x.CarFeatures)
                .HasForeignKey(x => x.FeatureId);
        }
    }
}
