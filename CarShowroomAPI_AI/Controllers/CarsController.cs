using CarShowroomAPI_AI.Context;
using CarShowroomAPI_AI.DTOs;
using CarShowroomAPI_AI.DTOs.Cars;
using CarShowroomAPI_AI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CarShowroomAPI_AI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public CarsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("CreateCar")]
        public IActionResult CreateCar([FromBody] SaveCarDto input)
        {
            ElectricCarProfile ElectricCarProfile = new ElectricCarProfile();
            FuelCarProfile? FuelCarProfile = new FuelCarProfile();
            if (input.Type == EngineType.Electric)
            {
                ElectricCarProfile = new ElectricCarProfile
                {
                    BatteryCapacityKwh = input.ElectricCarProfile.BatteryCapacityKwh,
                    RangeKm = input.ElectricCarProfile.RangeKm,
                    ChargingPort = input.ElectricCarProfile.ChargingPort,
                };

                _dbContext.ElectricCarProfiles.Add(ElectricCarProfile);
            }
            else if (input.Type == EngineType.Fuel)
            {
                FuelCarProfile = new FuelCarProfile
                {
                    EngineCapacityCc = input.FuelCarProfile.EngineCapacityCc,
                    FuelType = input.FuelCarProfile.FuelType,
                    ConsumptionPer100Km = input.FuelCarProfile.ConsumptionPer100Km,
                };
                _dbContext.FuelCarProfiles.Add(FuelCarProfile);
            }

            _dbContext.SaveChanges();

            var car = new Car
            {
                Model = input.Model,
                Vin = input.Vin,
                Price = input.Price,
                Type = input.Type,
                BranchId = input.BranchId,
                ElectricProfileId = input.Type == EngineType.Electric ? ElectricCarProfile.Id : null,
                FuelProfileId = input.Type == EngineType.Fuel ? FuelCarProfile.Id : null
            };

            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();

            var carFeaturesToAdd = new List<CarFeature>();
            if (input.Features != null && input.Features.Count > 0)
            {
                foreach (var featureId in input.Features)
                {
                    var carFeature = new CarFeature
                    {
                        CarId = car.Id,
                        FeatureId = featureId,
                        AssignedAt = DateTime.UtcNow
                    };

                    carFeaturesToAdd.Add(carFeature);
                }

                _dbContext.CarFeatures.AddRange(carFeaturesToAdd);
                _dbContext.SaveChanges();
            }
            var carResult = GetCar(car.Id);
            return Ok(MapToDto(carResult));
        }

        [HttpPut("UpdateCar/{id}")]
        public IActionResult UpdateCar(int id, [FromBody] SaveCarDto input)
        {
            var car = GetCar(id);

            if (car == null)
            {
                return NotFound("Car not exist");
            }

            car.Model = input.Model;
            car.Vin = input.Vin;
            car.Price = input.Price;
            car.BranchId = input.BranchId;

            _dbContext.Cars.Update(car);

            if (car.Type == EngineType.Electric)
            {
                car.ElectricProfile.BatteryCapacityKwh = input.ElectricCarProfile.BatteryCapacityKwh;
                car.ElectricProfile.RangeKm = input.ElectricCarProfile.RangeKm;
                car.ElectricProfile.ChargingPort = input.ElectricCarProfile.ChargingPort;

                _dbContext.ElectricCarProfiles.Update(car.ElectricProfile);
            }
            else if (car.Type == EngineType.Fuel)
            {
                car.FuelProfile.EngineCapacityCc = input.FuelCarProfile.EngineCapacityCc;
                car.FuelProfile.FuelType = input.FuelCarProfile.FuelType;
                car.FuelProfile.ConsumptionPer100Km = input.FuelCarProfile.ConsumptionPer100Km;

                _dbContext.FuelCarProfiles.Update(car.FuelProfile);
            }

            if (input.Features != null)
            {
                var carFeaturesToAdd = new List<CarFeature>();
                _dbContext.CarFeatures.RemoveRange(car.CarFeatures);

                foreach (var featureId in input.Features)
                {
                    var carFeature = new CarFeature
                    {
                        CarId = car.Id,
                        FeatureId = featureId,
                        AssignedAt = DateTime.UtcNow
                    };

                    carFeaturesToAdd.Add(carFeature);
                }

                _dbContext.CarFeatures.AddRange(carFeaturesToAdd);
            }

            _dbContext.SaveChanges();
            var carResult = GetCar(car.Id);
            return Ok(MapToDto(carResult));

        }


        //Pagination
        [HttpPost("ListCars")]
        public IActionResult ListCars([FromBody] CarFilterDto filter)
        {
            //Quarble

            var query = _dbContext.Cars
                .Include(x => x.ElectricProfile)
                .Include(x => x.FuelProfile)
                .Include(x => x.Branch).AsQueryable();


/*            query = query.Where
                (x =>
                    !string.IsNullOrEmpty(filter.Model) ? x.Model == filter.Model : true &&
                    !string.IsNullOrEmpty(filter.Vin) ? x.Vin == filter.Vin : true &&
                    filter.Price.HasValue ? x.Price == filter.Price : true &&
                    filter.CreatedAt.HasValue ? x.CreatedAt.Date == filter.CreatedAt.Value.Date : true &&
                );*/

            if (!string.IsNullOrEmpty(filter.Model))
            {
                query = query.Where(x => x.Model == filter.Model);
            }
            if (!string.IsNullOrEmpty(filter.Vin))
            {
                query = query.Where(x => x.Vin == filter.Vin);
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

            var count = query.Count();

            query = query.OrderByDescending(x => x.CreatedAt);

            var list = query.Skip(filter.PageIndex * filter.PageSize).Take(filter.PageSize);

            var listResult = list.Select(MapToDto).ToList();

            var result = new PaginationDto<CarDto>
            {
                Count = count,
                List = listResult
            };

            return Ok(result);
        }

        [HttpGet("GetCarDetails/{id}")]
        public IActionResult GetCarDetails(int id)
        {
            var car = GetCar(id);

            var result = MapToDetailsDto(car);

            return Ok(result);
        }

        [HttpDelete("DeleteCar/{id}")]
        public IActionResult DeleteCar(int id)
        {
            var car = GetCar(id);
            if (car.Type == EngineType.Electric)
            {
                var profile = car.ElectricProfile;
                _dbContext.ElectricCarProfiles.Remove(profile);
            }
            else
            {
                var profile = car.FuelProfile;
                _dbContext.FuelCarProfiles.Remove(profile);
            }

            var carFeatures = car.CarFeatures;
            if (carFeatures.Any())
            {
                _dbContext.RemoveRange(carFeatures);
            }

            _dbContext.Cars.Remove(car);

            _dbContext.SaveChanges();
            return Ok();
        }


        private CarDto MapToDto(Car car)
        {
            var result = new CarDto
            {
                Id = car.Id,
                Vin = car.Vin,
                Price = car.Price,
                Type = car.Type,
                BranchId = car.BranchId,
                BranchName = car.Branch.Name,
                ElectricCarProfile = car.Type == EngineType.Electric ? new ElectricCarProfileDto
                {
                    Id = car.ElectricProfileId,
                    BatteryCapacityKwh = car.ElectricProfile.BatteryCapacityKwh,
                    RangeKm = car.ElectricProfile.RangeKm,
                    ChargingPort = car.ElectricProfile.ChargingPort,
                } : null,
                FuelCarProfile = car.Type == EngineType.Fuel ? new FuelCarProfileDto
                {
                    Id = car.FuelProfileId,
                    EngineCapacityCc = car.FuelProfile.EngineCapacityCc,
                    FuelType = car.FuelProfile.FuelType,
                    ConsumptionPer100Km = car.FuelProfile.ConsumptionPer100Km,
                } : null,
            };

            return result;
        }

        private GetCarDetailsDto MapToDetailsDto(Car car)
        {
            var result = new GetCarDetailsDto
            {
                Id = car.Id,
                Vin = car.Vin,
                Price = car.Price,
                Type = car.Type,
                BranchId = car.BranchId,
                BranchName = car.Branch.Name,
                ElectricCarProfile = car.Type == EngineType.Electric ? new ElectricCarProfileDto
                {
                    Id = car.ElectricProfileId,
                    BatteryCapacityKwh = car.ElectricProfile.BatteryCapacityKwh,
                    RangeKm = car.ElectricProfile.RangeKm,
                    ChargingPort = car.ElectricProfile.ChargingPort,
                } : null,
                FuelCarProfile = car.Type == EngineType.Fuel ? new FuelCarProfileDto
                {
                    Id = car.FuelProfileId,
                    EngineCapacityCc = car.FuelProfile.EngineCapacityCc,
                    FuelType = car.FuelProfile.FuelType,
                    ConsumptionPer100Km = car.FuelProfile.ConsumptionPer100Km,
                } : null,
                Features = car.CarFeatures.Select(x => new CarFeaturesDto
                {
                    Id = x.Feature.Id,
                    Name = x.Feature.Name
                }).ToList()
            };

            return result;
        }

        private Car GetCar(int id)
        {
            var car = _dbContext.Cars
            .Include(x => x.ElectricProfile)
            .Include(x => x.FuelProfile)
            .Include(x => x.CarFeatures).ThenInclude(x => x.Feature)
            .Include(x => x.Branch)
            .FirstOrDefault(x => x.Id == id);


            return car;
        }
    }
}
