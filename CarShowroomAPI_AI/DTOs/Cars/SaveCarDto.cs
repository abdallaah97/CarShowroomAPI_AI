using CarShowroomAPI_AI.Entities;

namespace CarShowroomAPI_AI.DTOs.Cars
{
    public class SaveCarDto
    {
        public string Model { get; set; }
        public string Vin { get; set; }
        public decimal Price { get; set; }
        public EngineType Type { get; set; }
        public int BranchId { get; set; }
        public SaveElectricCarProfileDto? ElectricCarProfile { get; set; }
        public SaveFuelCarProfileDto? FuelCarProfile { get; set; }
        public List<int> Features { get; set; }
    }

    public class SaveElectricCarProfileDto
    {
        public double BatteryCapacityKwh { get; set; }
        public int RangeKm { get; set; }
        public string ChargingPort { get; set; }
    }

    public class SaveFuelCarProfileDto
    {
        public int EngineCapacityCc { get; set; }
        public string FuelType { get; set; }
        public double ConsumptionPer100Km { get; set; }
    }
}
