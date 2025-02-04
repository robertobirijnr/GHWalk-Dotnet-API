using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHWalk.Data;
using GHWalk.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace GHWalk.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly GHWalksDbContext _dbContext;
        public SQLRegionRepository(GHWalksDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }

        public async Task<Region> Create(Region region)
        {
           await _dbContext.Regions.AddAsync(region);
           await _dbContext.SaveChangesAsync();
           return region;
        }

        public async Task<Region?> Delete(Guid id)
        {
            var region = await _dbContext.Regions.FirstOrDefaultAsync(r=>r.Id == id);
            if(region is null){
                return null;
            }

            _dbContext.Regions.Remove(region);
            await _dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<List<Region>> GetAllAsync()
        {
          return  await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetById(Guid id)
        {
            return await _dbContext.Regions.FindAsync(id);
        }

        public async Task<Region?> Update(Guid id, Region region)
        {
            var isExist = await _dbContext.Regions.FirstOrDefaultAsync(r=>r.Id == id);
            if(isExist is null){
                return null;
            }

            isExist.Code = region.Code;
            isExist.Name = region.Name;
            isExist.RegionImageUrl = region.RegionImageUrl;

             await _dbContext.SaveChangesAsync();
             return isExist;
        }
    }
}