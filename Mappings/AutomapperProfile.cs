
using AutoMapper;
using GHWalk.Models.Domain;
using GHWalk.Models.DTO;

namespace GHWalk.Mappings
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Region,RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto,Region>().ReverseMap();

            CreateMap<addWalkRequestDto,Walk>().ReverseMap();
            CreateMap<Walk,WalkDto>().ReverseMap();
            CreateMap<UpdateWalkDto,Walk>().ReverseMap();
            CreateMap<Difficulty,DifficultyDto>().ReverseMap();
        }
    }
}