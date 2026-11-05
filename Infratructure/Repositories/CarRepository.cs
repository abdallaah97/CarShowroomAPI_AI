using Application.Services.Cars.DTOs;
using CarShowroomAPI_AI.Context;
using CarShowroomAPI_AI.Domian.Entities;
using CarShowroomAPI_AI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CarShowroomAPI_AI.Repositories
{
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        public CarRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Car> GetCarDetails(int id)
        {
            var car = await _dbSet
                .Include(x => x.ElectricProfile)
                .Include(x => x.FuelProfile)
                .Include(x => x.CarFeatures).ThenInclude(x => x.Feature)
                .Include(x => x.Branch)
                .FirstOrDefaultAsync(x => x.Id == id);

            return car;
        }

        public async Task<PaginationDto<Car>> ListCars(CarFilterDto filter)
        {
            var query = _dbSet
                .Include(x => x.ElectricProfile)
                .Include(x => x.FuelProfile)
                .Include(x => x.Branch).AsQueryable();

            if (!string.IsNullOrEmpty(filter.Model))
            {
                query = query.Where(x => x.Model.ToLower().Contains(filter.Model.ToLower().Trim()));
            }
            if (!string.IsNullOrEmpty(filter.Vin))
            {
                query = query.Where(x => x.Vin.ToLower() == filter.Vin.ToLower());
            }
            if (filter.Price.HasValue)
            {
                query = query.Where(x => x.Price == filter.Price);
            }
            if (filter.CreatedAt.HasValue)
            {
                query = query.Where(x => x.CreatedAt.Date == filter.CreatedAt.Value.Date);
            }
            if (filter.Type.HasValue)
            {
                query = query.Where(x => x.Type == filter.Type);
            }
            if (filter.BranchId.HasValue)
            {
                query = query.Where(x => x.BranchId == filter.BranchId);
            }

            var count = await query.CountAsync();

            var list = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip(filter.PageIndex * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var result = new PaginationDto<Car>
            {
                Count = count,
                List = list
            };

            return result;
        }
    }
}
