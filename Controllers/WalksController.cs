
using System.Threading.Tasks;
using AutoMapper;
using GHWalk.Models.Domain;
using GHWalk.Models.DTO;
using GHWalk.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GHWalk.Controllers
{
     [ApiController]
    [Route("api/[controller]")]
    
    public class WalksController:ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] addWalkRequestDto request){
         if(ModelState.IsValid){
           var payload =  _mapper.Map<Walk>(request);
          await _walkRepository.Create(payload);

          var responseDTO = _mapper.Map<WalkDto>(payload);

           return Ok(responseDTO);
         }else{
          return BadRequest(ModelState);
         }

        }

        [HttpGet]

         public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending){
           
          var walks =  await _walkRepository.GetAll(filterOn,filterQuery,sortBy,isAscending ?? true);

            //Map Domian Model to DTO
          var responseDTO = _mapper.Map<List<WalkDto>>(walks);

          return Ok(responseDTO);
        }

        // public async Task<IActionResult> GetAllWalks(){
           
        //   var walks =  await _walkRepository.GetAll();

        //     //Map Domian Model to DTO
        //   var responseDTO = _mapper.Map<List<WalkDto>>(walks);

        //   return Ok(responseDTO);
        // }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWalk(Guid id){
           var walk = await _walkRepository.GetById(id);

           var responseDTO = _mapper.Map<WalkDto>(walk);

           return Ok(responseDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWalk(Guid id, UpdateWalkDto updateWalkDto){

           if(ModelState.IsValid){
             var requestDTO = _mapper.Map<Walk>(updateWalkDto);
           var walk = await _walkRepository.Update(id,requestDTO);

           if(walk is null){
            return NotFound();
           }

           var updateDTO = _mapper.Map<UpdateWalkDto>(walk);

           return Ok(updateDTO);
           }else{
            return BadRequest(ModelState);
           }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWalk(Guid id){
           var walk = await _walkRepository.Delete(id);
           if(walk is null){
            return NotFound();
           }

           var responseDTO = _mapper.Map<WalkDto>(walk);
           return Ok(responseDTO);
        }
    }
}