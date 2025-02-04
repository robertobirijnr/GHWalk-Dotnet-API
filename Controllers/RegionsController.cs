using GHWalk.Data;
using GHWalk.Models.Domain;
using GHWalk.Models.DTO;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll()
        {
         
         //Get Data From Database - Domain model
         var regions = _dbContext.Regions.ToList();

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
        public IActionResult GetById([FromRoute] Guid id){
           var region = _dbContext.Regions.Find(id);
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
        public IActionResult CreateRegion([FromBody] AddRegionRequestDto request){
            //Map DTO to Domain Model
            var payload = new Region{
                Code = request.Code,
                Name = request.Name,
                RegionImageUrl = request.RegionImageUrl
            };

            //use payload to create region
            _dbContext.Regions.Add(payload);
            _dbContext.SaveChanges();

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
        public IActionResult UpdateRgion([FromRoute] Guid id, [FromBody] AddRegionRequestDto request){
            var region = _dbContext.Regions.Find(id);
            if(region is null){
                return NotFound();
            }

            region.Code = request.Code;
            region.Name = request.Name;
            region.RegionImageUrl = request.RegionImageUrl;

            _dbContext.SaveChanges();

             var ResponseDTO = new RegionDto{
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(ResponseDTO);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteRegion([FromRoute] Guid id){
            var region = _dbContext.Regions.Find(id);

            if(region is null){
                return NotFound();
            }

            _dbContext.Regions.Remove(region);
            _dbContext.SaveChanges();
            return Ok();
        }
    }

    
}