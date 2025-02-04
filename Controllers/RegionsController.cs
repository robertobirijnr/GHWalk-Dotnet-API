using System.Threading.Tasks;
using GHWalk.Data;
using GHWalk.Models.Domain;
using GHWalk.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GHWalk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController: ControllerBase
    {
        private readonly GHWalksDbContext _dbContext;
        public RegionsController(GHWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
         
         //Get Data From Database - Domain model
         var regions = await _dbContext.Regions.ToListAsync();

         //Map Domain models to DTOs
        var regionsDTO = new List<RegionDto>();
        foreach(var region in regions){
            regionsDTO.Add(new RegionDto(){
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            });
        }

            //Return DTO   
            return Ok(regionsDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id){
           var region = await _dbContext.Regions.FindAsync(id);
           //var region = _dbContext.Regions.FirstOrDefault(r=>r.Id == id);
            if(region is null){
                return NotFound();
            }

            //Map DTOs
            var regionDto = new RegionDto{
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };
            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto request){
            //Map DTO to Domain Model
            var payload = new Region{
                Code = request.Code,
                Name = request.Name,
                RegionImageUrl = request.RegionImageUrl
            };

            //use payload to create region
           await _dbContext.Regions.AddAsync(payload);
           await _dbContext.SaveChangesAsync();

            //DTO for Response
            var ResponseDTO = new RegionDto{
                Id = payload.Id,
                Code = payload.Code,
                Name = payload.Name,
                RegionImageUrl = payload.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById),new {id = ResponseDTO.Id}, ResponseDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRgion([FromRoute] Guid id, [FromBody] AddRegionRequestDto request){
            var region = await _dbContext.Regions.FindAsync(id);
            if(region is null){
                return NotFound();
            }

            region.Code = request.Code;
            region.Name = request.Name;
            region.RegionImageUrl = request.RegionImageUrl;

           await _dbContext.SaveChangesAsync();

             var ResponseDTO = new RegionDto{
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(ResponseDTO);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id){
            var region =await _dbContext.Regions.FindAsync(id);

            if(region is null){
                return NotFound();
            }

            _dbContext.Regions.Remove(region);
           await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }

    
}