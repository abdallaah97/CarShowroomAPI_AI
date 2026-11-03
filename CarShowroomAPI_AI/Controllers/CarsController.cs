using Application.Services.Cars.DTOs;
using CarShowroomAPI_AI.Services.Cars;
using Microsoft.AspNetCore.Mvc;

namespace CarShowroomAPI_AI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        //Sync
        //Async
        [HttpPost("CreateCar")]
        public async Task<IActionResult> CreateCar([FromBody] SaveCarDto input)
        {
            try
            {
                var carResult = await _carService.CreateCar(input);
                return Ok(carResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateCar/{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] SaveCarDto input)
        {
            try
            {
                var carResult = await _carService.UpdateCar(id, input);
                return Ok(carResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Pagination
        [HttpPost("ListCars")]
        public async Task<IActionResult> ListCars([FromBody] CarFilterDto filter)
        {
            try
            {
                var result = await _carService.ListCars(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCarDetails/{id}")]
        public async Task<IActionResult> GetCarDetails(int id)
        {
            try
            {
                var carResult = await _carService.GetCarById(id);
                return Ok(carResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCar/{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _carService.DeleteCar(id);
            return Ok();
        }
    }
}
