

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var difficulties = new List<Difficulty>(){
               new Difficulty(){
                 Id = Guid.Parse("0f142854-f595-4339-1474-08dd44f91363"),
                Name ="Easy"
               },
                new Difficulty(){
                 Id = Guid.Parse("5d3b713f-b458-4fdc-8394-08dd4501ec6a"),
                Name =""
               },
                new Difficulty(){
                 Id = Guid.Parse("66a7ef7f-c0a1-4312-38de-08dd45336635"),
                Name ="Hard"
               }
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);
        }
    }
}