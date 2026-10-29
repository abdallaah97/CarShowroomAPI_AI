using CarShowroomAPI_AI.Context;
using CarShowroomAPI_AI.DTOs.Features;
using CarShowroomAPI_AI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarShowroomAPI_AI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public FeaturesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("CreateFeature")]
        public IActionResult CreateFeature([FromBody] SaveFeatureDto input)
        {
            var Feature = new Feature
            {
                Name = input.Name,
            };

            _dbContext.Features.Add(Feature);
            _dbContext.SaveChanges();

            var result = new FeatureDto
            {
                Id = Feature.Id,
                Name = input.Name,
            };

            return Ok(result);
        }

        [HttpGet("ListFeaturees")]
        public IActionResult ListFeaturees()
        {
            var barnches = _dbContext.Features.Select(x => new FeatureDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            return Ok(barnches);
        }

    }
}
