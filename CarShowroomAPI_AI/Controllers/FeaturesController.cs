using Application.Services.Features;
using Application.Services.FeaturesService.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarShowroomAPI_AI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeaturesService _featuresService;
        public FeaturesController(IFeaturesService featuresService)
        {
            _featuresService = featuresService;
        }

        [HttpPost("CreateFeature")]
        public async Task<IActionResult> CreateFeature([FromBody] SaveFeatureDto input)
        {
            try
            {
                var result = await _featuresService.CreateFeature(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ListFeaturees")]
        public async Task<IActionResult> ListFeaturees()
        {
            try
            {
                var result = await _featuresService.ListFeatures();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
