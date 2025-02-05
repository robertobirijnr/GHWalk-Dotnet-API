

using System.ComponentModel.DataAnnotations;

namespace GHWalk.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3,ErrorMessage ="Code should be atleast 3 characters")]
        [MaxLength(3)]
        public required string Code {get; set;}
        [Required]
        [MinLength(3,ErrorMessage ="Code should be atleast 3 characters")]
        public required string Name {get; set;}

        public string? RegionImageUrl {get; set;}
    }
}