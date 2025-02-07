using System.Threading.Tasks;
using AutoMapper;
using GHWalk.CustomActionFilters;
using GHWalk.Data;
using GHWalk.Models.Domain;
using GHWalk.Models.DTO;
using GHWalk.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GHWalk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RegionsController: ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _Mapper;
        public RegionsController(IRegionRepository regionRepository,IMapper mapper)
        {
            _Mapper = mapper;
            _regionRepository = regionRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
         
         //Get Data From Database - Domain model
         var regions = await _regionRepository.GetAllAsync();


        //Map Model to DTO
        var regionsDTO = _Mapper.Map<List<RegionDto>>(regions);

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
            var regionDto = _Mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto request){

           
            var payload =  _Mapper.Map<Region>(request);

            await _regionRepository.Create(payload);

             var ResponseDTO = _Mapper.Map<RegionDto>(payload);

            return CreatedAtAction(nameof(GetById),new {id = ResponseDTO.Id}, ResponseDTO);
            

           
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRgion([FromRoute] Guid id, [FromBody] AddRegionRequestDto request){
           
           
            var RequestDTO = _Mapper.Map<Region>(request);

            var region = await _regionRepository.Update(id,RequestDTO);
            if(region is null){
                return NotFound();
            }

           var ResponseDTO = _Mapper.Map<RegionDto>(region);
            
            return Ok(ResponseDTO);
          
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id){
            var region = await _regionRepository.Delete(id);

            if(region is null){
                return NotFound();
            }

          var response =  _Mapper.Map<RegionDto>(region);
            return Ok(response);
        }
    }

    
}