using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHWalk.Data;
using GHWalk.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace GHWalk.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly GHWalksDbContext _gHWalksDbContext;
        public SQLWalkRepository(GHWalksDbContext gHWalksDbContext)
        {
            _gHWalksDbContext = gHWalksDbContext;
            
        }
        public async Task<Walk> Create(Walk walk)
        {
          await _gHWalksDbContext.Walks.AddAsync(walk);
          await _gHWalksDbContext.SaveChangesAsync();
          return walk;
        }

        public async Task<Walk?> Delete(Guid id)
        {
            var walk = await _gHWalksDbContext.Walks.FirstOrDefaultAsync(w=>w.Id == id);
            if(walk is null){
                return null;
            }
            _gHWalksDbContext.Walks.Remove(walk);
            await _gHWalksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAll(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending=true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = _gHWalksDbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
        
            //Filtering
            if(string.IsNullOrWhiteSpace(filterOn)==false && string.IsNullOrWhiteSpace(filterQuery) == false){
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase)){
                    walks = walks.Where(w=>w.Name.Contains(filterQuery));
                }
            }

            //Sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false){
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase)){
                    walks = isAscending? walks.OrderBy(w=>w.Name) : walks.OrderByDescending(w=>w.Name);
                }
            }


            //Pagination
            var Skipresults  = (pageNumber - 1) * pageSize;

            return await walks.Skip(Skipresults).Take(pageSize).ToListAsync();
            // return await walks.ToListAsync();
           // return await _gHWalksDbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
           
        }



        public async Task<Walk?> GetById(Guid id)
        {
           return await _gHWalksDbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(w=>w.Id == id);
        }

        public async Task<Walk?> Update(Guid id, Walk walk)
        {
           var response = await _gHWalksDbContext.Walks.FirstOrDefaultAsync(w=> w.Id == id);
           if(response is null){
            return null;
           }

           response.Name = walk.Name;
           response.Description = walk.Description;
           response.LengthInKm = walk.LengthInKm;
           response.WalkImageUrl = walk.WalkImageUrl;
           response.RegionId = walk.RegionId;
           response.DifficultyId = walk.DifficultyId;

           await _gHWalksDbContext.SaveChangesAsync();

           return response;

        }
    }
}