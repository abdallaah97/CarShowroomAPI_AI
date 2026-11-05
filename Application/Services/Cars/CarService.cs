using Application.Services.Cars.DTOs;
using CarShowroomAPI_AI.Domian.Entities;
using CarShowroomAPI_AI.DTOs;
using CarShowroomAPI_AI.Repositories;

namespace CarShowroomAPI_AI.Services.Cars
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IGenericRepository<ElectricCarProfile> _electricCarProfileRepository;
        private readonly IGenericRepository<FuelCarProfile> _fuelCarProfileRepository;
        private readonly IGenericRepository<CarFeature> _carFeatureRepository;
        public CarService(ICarRepository carRepository, IGenericRepository<ElectricCarProfile> electricCarProfileRepository, IGenericRepository<FuelCarProfile> fuelCarProfileRepository, IGenericRepository<CarFeature> carFeatureRepository)
        {
            _carRepository = carRepository;
            _electricCarProfileRepository = electricCarProfileRepository;
            _fuelCarProfileRepository = fuelCarProfileRepository;
            _carFeatureRepository = carFeatureRepository;
        }

        public async Task<CarDto> CreateCar(SaveCarDto input)
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

                await _electricCarProfileRepository.Insert(ElectricCarProfile);
            }
            else if (input.Type == EngineType.Fuel)
            {
                FuelCarProfile = new FuelCarProfile
                {
                    EngineCapacityCc = input.FuelCarProfile.EngineCapacityCc,
                    FuelType = input.FuelCarProfile.FuelType,
                    ConsumptionPer100Km = input.FuelCarProfile.ConsumptionPer100Km,
                };
                await _fuelCarProfileRepository.Insert(FuelCarProfile);
            }

            await _fuelCarProfileRepository.SaveChanges();

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

            await _carRepository.Insert(car);
            await _carRepository.SaveChanges();

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

                await _carFeatureRepository.InsertRange(carFeaturesToAdd);
                await _carFeatureRepository.SaveChanges();
            }

            var carResult = await GetCar(car.Id);
            return MapToDto(carResult);
        }

        public async Task<CarDto> UpdateCar(int id, SaveCarDto input)
        {
            var car = await GetCar(id);
            if (car == null)
            {
                throw new Exception("Car not exist");
            }
            car.Model = input.Model;
            car.Vin = input.Vin;
            car.Price = input.Price;
            car.BranchId = input.BranchId;
            _carRepository.Update(car);
            if (car.Type == EngineType.Electric)
            {
                car.ElectricProfile.BatteryCapacityKwh = input.ElectricCarProfile.BatteryCapacityKwh;
                car.ElectricProfile.RangeKm = input.ElectricCarProfile.RangeKm;
                car.ElectricProfile.ChargingPort = input.ElectricCarProfile.ChargingPort;
                _electricCarProfileRepository.Update(car.ElectricProfile);
            }
            else if (car.Type == EngineType.Fuel)
            {
                car.FuelProfile.EngineCapacityCc = input.FuelCarProfile.EngineCapacityCc;
                car.FuelProfile.FuelType = input.FuelCarProfile.FuelType;
                car.FuelProfile.ConsumptionPer100Km = input.FuelCarProfile.ConsumptionPer100Km;
                _fuelCarProfileRepository.Update(car.FuelProfile);
            }
            if (input.Features != null)
            {
                var carFeaturesToAdd = new List<CarFeature>();
                _carFeatureRepository.DeleteRange(car.CarFeatures);
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
                await _carFeatureRepository.InsertRange(carFeaturesToAdd);
            }
            await _carFeatureRepository.SaveChanges();
            var carResult = await GetCar(car.Id);
            return MapToDto(carResult);
        }

        public async Task<CarDto> GetCarById(int id)
        {
            var car = await GetCar(id);
            if (car == null)
            {
                throw new Exception("Car not exist");
            }
            return MapToDto(car);
        }

        public async Task<PaginationDto<CarDto>> ListCars(CarFilterDto filter)
        {
            var cars = await _carRepository.ListCars(filter);

            var result = new PaginationDto<CarDto>
            {
                Count = cars.Count,
                List = cars.List.Select(MapToDto).ToList()
            };

            return result;
        }

        public async Task DeleteCar(int id)
        {
            var car = await GetCar(id);
            if (car == null)
            {
                throw new Exception("Car not exist");
            }
            if (car.Type == EngineType.Electric)
            {
                var profile = car.ElectricProfile;
                _electricCarProfileRepository.Delete(profile);
            }
            else
            {
                var profile = car.FuelProfile;
                _fuelCarProfileRepository.Delete(profile);
            }
            var carFeatures = car.CarFeatures;
            if (carFeatures.Any())
            {
                _carFeatureRepository.DeleteRange(carFeatures);
            }
            _carRepository.Delete(car);
            await _carFeatureRepository.SaveChanges();
        }


        private async Task<Car> GetCar(int id)
        {
            var car = await _carRepository.GetCarDetails(id);

            return car;
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
    }
}
