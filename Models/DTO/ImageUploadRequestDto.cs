using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GHWalk.Models.DTO
{
    public class ImageUploadRequestDto
    {
        [Required]
        public required IFormFile File {get; set;}
        [Required]
        public required string FileName {get; set;}

        public string? FileDescription {get; set;}
    }
}