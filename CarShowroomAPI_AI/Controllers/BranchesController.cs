using CarShowroomAPI_AI.Context;
using CarShowroomAPI_AI.DTOs.Branch;
using CarShowroomAPI_AI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarShowroomAPI_AI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public BranchesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("CreateBranch")]
        public IActionResult CreateBranch([FromBody] SaveBranchDto input)
        {
            var branch = new Branch
            {
                Name = input.Name,
                Address = input.Address
            };

            _dbContext.Branches.Add(branch);
            _dbContext.SaveChanges();

            var result = new BranchDto
            {
                Id = branch.Id,
                Name = input.Name,
                Address = input.Address
            };

            return Ok(result);
        }

        [HttpDelete("DeleteBranch/{id}")]
        public IActionResult DeleteBranch(int id)
        {
            var branch = _dbContext.Branches.FirstOrDefault(x => x.Id == id);
            if (branch == null)
            {
                return NotFound("Branch not exist");
            }
            var isThereCars = _dbContext.Cars.Any(x => x.BranchId == id);
            if (isThereCars)
            {
                return BadRequest("You cannt delete branch");
            }

            _dbContext.Branches.Remove(branch);
            _dbContext.SaveChanges();

            return Ok();
        }


        [HttpGet("ListBranches")]
        public IActionResult ListBranches()
        {
            var barnches = _dbContext.Branches.Select(x => new BranchDto
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            }).ToList();

            return Ok(barnches);
        }

    }
}
