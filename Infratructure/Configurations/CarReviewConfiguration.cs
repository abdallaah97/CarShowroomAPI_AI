using CarShowroomAPI_AI.Domian.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarShowroomAPI_AI.Configurations
{
    public class CarReviewConfiguration : IEntityTypeConfiguration<CarReview>
    {
        public void Configure(EntityTypeBuilder<CarReview> builder)
        {
            builder.Property(x => x.ReviewerName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Comment)
                .IsRequired()
                .HasMaxLength(1000);

            builder.HasOne(x => x.Car)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.CarId);
        }
    }
}
