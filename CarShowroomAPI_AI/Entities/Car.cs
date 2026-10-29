using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShowroomAPI_AI.Entities
{
    [Index(nameof(Vin), IsUnique = true)]
    [Index(nameof(FuelProfileId), IsUnique = true)]
    [Index(nameof(ElectricProfileId), IsUnique = true)]
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Vin { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public EngineType Type { get; set; }

        public int? FuelProfileId { get; set; }
        [ForeignKey("FuelProfileId")]
        public FuelCarProfile? FuelProfile { get; set; }

        public int? ElectricProfileId { get; set; }
        [ForeignKey("ElectricProfileId")]
        public ElectricCarProfile? ElectricProfile { get; set; }

        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }

        public ICollection<CarReview> Reviews { get; set; }

        public ICollection<CarFeature> CarFeatures { get; set; }
    }

    public enum EngineType
    {
        Fuel = 1,
        Electric = 2
    }
}
