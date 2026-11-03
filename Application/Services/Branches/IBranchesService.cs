using Application.Services.BranchesService.DTOs;
using CarShowroomAPI_AI.DTOs;

namespace Application.Services.BranchesService
{
    public interface IBranchesService
    {
        public Task<BranchDto> CreateBranch(SaveBranchDto input);
        public Task DeleteBranch(int id);
        public Task<PaginationDto<BranchDto>> ListBranches(int pagIndex = 0, int pageSize = 10);
    }
}
