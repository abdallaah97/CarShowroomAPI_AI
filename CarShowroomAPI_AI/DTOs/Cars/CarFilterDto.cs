using CarShowroomAPI_AI.Entities;

namespace CarShowroomAPI_AI.DTOs.Cars
{
    public class CarFilterDto : PaginationInput
    {
        public string? Model { get; set; }
        public string? Vin { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public EngineType? Type { get; set; }
        public int? BranchId { get; set; }
    }
}
