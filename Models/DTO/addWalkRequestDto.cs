using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GHWalk.Models.DTO
{
    public class addWalkRequestDto
    {
        public required string Name {get; set;}
        public required string Description {get; set;}
        public double LengthInKm {get; set;}
        public string? WalkImageUrl {get; set;}

         public Guid DifficultyId {get; set;}
        public Guid RegionId {get; set;}
    }
}