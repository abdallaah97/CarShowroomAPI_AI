using Application.Services.FeaturesService.DTOs;

namespace Application.Services.Features
{
    public interface IFeaturesService
    {
        public Task<List<FeatureDto>> ListFeatures();
        public Task<FeatureDto> CreateFeature(SaveFeatureDto input);
    }
}
