using Application.Services.BranchesService.DTOs;
using CarShowroomAPI_AI.Domian.Entities;
using CarShowroomAPI_AI.DTOs;
using CarShowroomAPI_AI.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Application.Services.BranchesService
{
    public class BranchesService : IBranchesService
    {
        private readonly IGenericRepository<Branch> _branchRepsitory;
        private readonly IGenericRepository<Car> _carRepository;
        public BranchesService(IGenericRepository<Branch> branchRepsitory, IGenericRepository<Car> carRepository)
        {
            _branchRepsitory = branchRepsitory;
            _carRepository = carRepository;
        }

        public async Task<BranchDto> CreateBranch(SaveBranchDto input)
        {
            var branch = new Branch
            {
                Name = input.Name,
                Address = input.Address,
            };
            await _branchRepsitory.Insert(branch);
            await _branchRepsitory.SaveChanges();
            var result = new BranchDto
            {
                Id = branch.Id,
                Name = branch.Name,
                Address = branch.Address,
            };
            return result;
        }

        public async Task DeleteBranch(int id)
        {
            var branch = await _branchRepsitory.GetById(id);
            if (branch == null)
            {
                throw new Exception("Branch not exist");
            }
            var isThereCars = await _carRepository.GetAll().AnyAsync(x => x.BranchId == id);
            if (isThereCars)
            {
                throw new Exception("You cannt delete branch");
            }
            _branchRepsitory.Delete(branch);
            await _branchRepsitory.SaveChanges();
        }

        public async Task<PaginationDto<BranchDto>> ListBranches(int pagIndex = 0, int pageSize = 10)
        {
            var query = _branchRepsitory.GetAll();
            var totalCount = await query.CountAsync();
            var branches = await query.Skip(pagIndex * pageSize).Take(pageSize).ToListAsync();
            var result = new PaginationDto<BranchDto>
            {
                Count = totalCount,
                List = branches.Select(x => new BranchDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                }).ToList()
            };
            return result;
        }

    }
}
