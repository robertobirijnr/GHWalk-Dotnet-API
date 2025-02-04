
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
        }
    }
}