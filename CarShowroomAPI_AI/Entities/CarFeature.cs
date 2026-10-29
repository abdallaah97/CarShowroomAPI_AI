using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShowroomAPI_AI.Entities
{
    [Index(nameof(CarId), nameof(FeatureId), IsUnique = true)]
    public class CarFeature
    {
        public int Id { get; set; }

        public int CarId { get; set; }
        [ForeignKey("CarId")]
        public Car Car { get; set; }

        public int FeatureId { get; set; }
        [ForeignKey("FeatureId")]
        public Feature Feature { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}
