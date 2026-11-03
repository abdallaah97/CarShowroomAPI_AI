using Application.Services.Cars.DTOs;
using CarShowroomAPI_AI.DTOs;

namespace CarShowroomAPI_AI.Services.Cars
{
    public interface ICarService
    {
        public Task<CarDto> CreateCar(SaveCarDto input);
        public Task<CarDto> UpdateCar(int id, SaveCarDto input);
        public Task<CarDto> GetCarById(int id);
        public Task<PaginationDto<CarDto>> ListCars(CarFilterDto filter);
        public Task DeleteCar(int id);
    }
}
