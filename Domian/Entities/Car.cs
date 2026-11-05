using System.ComponentModel.DataAnnotations;

namespace CarShowroomAPI_AI.Domian.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Vin { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public EngineType Type { get; set; }

        public int? FuelProfileId { get; set; }
        public FuelCarProfile? FuelProfile { get; set; }

        public int? ElectricProfileId { get; set; }
        public ElectricCarProfile? ElectricProfile { get; set; }

        public int BranchId { get; set; }
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
