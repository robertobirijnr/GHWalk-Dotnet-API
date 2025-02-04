using System.Threading.Tasks;
using GHWalk.Data;
using GHWalk.Models.Domain;
using GHWalk.Models.DTO;
using GHWalk.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GHWalk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController: ControllerBase
    {
        private readonly GHWalksDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        public RegionsController(GHWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
         
         //Get Data From Database - Domain model
         var regions = await _regionRepository.GetAllAsync();

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
           var region = await _regionRepository.GetById(id);
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
        //    await _dbContext.Regions.AddAsync(payload);
        //    await _dbContext.SaveChangesAsync();
            await _regionRepository.Create(payload);

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
            var RequestDTO = new Region{
                Code = request.Code,
                Name = request.Name,
                RegionImageUrl = request.RegionImageUrl
            };

            var region = await _regionRepository.Update(id,RequestDTO);
            if(region is null){
                return NotFound();
            }

           
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
            var region = await _regionRepository.Delete(id);

            if(region is null){
                return NotFound();
            }


            return Ok();
        }
    }

    
}