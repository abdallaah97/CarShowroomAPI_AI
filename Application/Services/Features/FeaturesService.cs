using Application.Services.FeaturesService.DTOs;
using CarShowroomAPI_AI.Domian.Entities;
using CarShowroomAPI_AI.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Application.Services.Features
{
    public class FeaturesService : IFeaturesService
    {
        private readonly IGenericRepository<Feature> _featureRepository;
        public FeaturesService(IGenericRepository<Feature> featureRepository)
        {
            _featureRepository = featureRepository;
        }

        public async Task<List<FeatureDto>> ListFeatures()
        {
            var features = await _featureRepository.GetAll().Select(x => new FeatureDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
            return features;
        }

        public async Task<FeatureDto> CreateFeature(SaveFeatureDto input)
        {
            var feature = new Feature
            {
                Name = input.Name,
            };
            await _featureRepository.Insert(feature);
            await _featureRepository.SaveChanges();
            var result = new FeatureDto
            {
                Id = feature.Id,
                Name = input.Name,
            };
            return result;
        }
    }
}
