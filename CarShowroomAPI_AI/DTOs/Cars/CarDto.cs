using CarShowroomAPI_AI.Entities;

namespace CarShowroomAPI_AI.DTOs.Cars
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Vin { get; set; }
        public decimal Price { get; set; }
        public EngineType Type { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public ElectricCarProfileDto? ElectricCarProfile { get; set; }
        public FuelCarProfileDto? FuelCarProfile { get; set; }
    }

    public class ElectricCarProfileDto
    {
        public int? Id { get; set; }
        public double BatteryCapacityKwh { get; set; }
        public int RangeKm { get; set; }
        public string ChargingPort { get; set; }
    }

    public class FuelCarProfileDto
    {
        public int? Id { get; set; }
        public int EngineCapacityCc { get; set; }
        public string FuelType { get; set; }
        public double ConsumptionPer100Km { get; set; }
    }
}
