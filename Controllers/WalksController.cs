
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
          var payload =  _mapper.Map<Walk>(request);
          await _walkRepository.Create(payload);

          var responseDTO = _mapper.Map<WalkDto>(payload);

        return Ok(responseDTO);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks(){
           
          var walks =  await _walkRepository.GetAll();

          var responseDTO = _mapper.Map<List<WalkDto>>(walks);

          return Ok(responseDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWalk(Guid id){
           var walk = await _walkRepository.GetById(id);

           var responseDTO = _mapper.Map<WalkDto>(walk);

           return Ok(responseDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWalk(Guid id, UpdateWalkDto updateWalkDto){

            var requestDTO = _mapper.Map<Walk>(updateWalkDto);
           var walk = await _walkRepository.Update(id,requestDTO);

           if(walk is null){
            return NotFound();
           }

           var updateDTO = _mapper.Map<UpdateWalkDto>(walk);

           return Ok(updateDTO);
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