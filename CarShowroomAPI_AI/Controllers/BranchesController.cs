using Application.Services.BranchesService;
using Application.Services.BranchesService.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarShowroomAPI_AI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchesService _branchesService;
        public BranchesController(IBranchesService branchesService)
        {
            _branchesService = branchesService;
        }

        [HttpPost("CreateBranch")]
        public async Task<IActionResult> CreateBranch([FromBody] SaveBranchDto input)
        {
            try
            {
                var result = await _branchesService.CreateBranch(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteBranch/{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            try
            {
                await _branchesService.DeleteBranch(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }


        [HttpGet("ListBranches")]
        public IActionResult ListBranches(int pagIndex = 0, int pageSize = 10)
        {
            try
            {
                var result = _branchesService.ListBranches(pagIndex, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
