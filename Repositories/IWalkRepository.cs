
using GHWalk.Models.Domain;

namespace GHWalk.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> Create(Walk walk);
        Task<List<Walk>> GetAll(string? filterOn = null, string? filterQuery = null);
        Task<Walk?>  GetById(Guid id);
        Task<Walk?>  Update(Guid id, Walk walk);
        Task<Walk?> Delete(Guid id);
    }
}