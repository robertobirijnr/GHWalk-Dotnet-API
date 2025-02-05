using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GHWalk.Models.DTO
{
    public class addWalkRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage ="Name should be at least 3 characters")]
        public required string Name {get; set;}

         [Required]
        [MinLength(10, ErrorMessage ="Name should be at least 10 characters")]
        public required string Description {get; set;}

         [Required]
         [Range(0,50, ErrorMessage ="Length must be in the range of 0 to 50")]
        public double LengthInKm {get; set;}
        public string? WalkImageUrl {get; set;}

         public Guid DifficultyId {get; set;}
        public Guid RegionId {get; set;}
    }
}