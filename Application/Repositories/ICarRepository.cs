using Application.Services.Cars.DTOs;
using CarShowroomAPI_AI.Domian.Entities;
using CarShowroomAPI_AI.DTOs;

namespace CarShowroomAPI_AI.Repositories
{
    public interface ICarRepository : IGenericRepository<Car>
    {
        Task<Car> GetCarDetails(int id);
        Task<PaginationDto<Car>> ListCars(CarFilterDto filter);
    }
}
