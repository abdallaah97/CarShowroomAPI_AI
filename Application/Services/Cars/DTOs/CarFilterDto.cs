using CarShowroomAPI_AI.Domian.Entities;
using CarShowroomAPI_AI.DTOs;

namespace Application.Services.Cars.DTOs
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
