
using GHWalk.Models.Domain;

namespace GHWalk.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> Create(Walk walk);
        Task<List<Walk>> GetAll(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending=true, int pageNumber = 1, int pageSize = 1000);
        Task<Walk?>  GetById(Guid id);
        Task<Walk?>  Update(Guid id, Walk walk);
        Task<Walk?> Delete(Guid id);
    }
}