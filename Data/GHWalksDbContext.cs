

using GHWalk.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace GHWalk.Data
{
    public class GHWalksDbContext:DbContext
    {

        public GHWalksDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
        
        }

        public DbSet<Difficulty> Difficulties {get; set;}
        public DbSet<Region> Regions {get; set;}
        public DbSet<Walk> Walks {get; set;}
    }
}