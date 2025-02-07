using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHWalk.Models.Domain;
using GHWalk.Models.DTO;
using GHWalk.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GHWalk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageUploadController : ControllerBase
    {   
        private readonly IImageRepository _imageRepository;
        public ImageUploadController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
            
        }
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request){
            ValidateFileUpload(request);
            if(ModelState.IsValid){
                var Image = new Image{
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription
                };

                await _imageRepository.Upload(Image);
                return Ok(Image);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto request){
            var allowedEx = new string[] {".jpg",".jpeg",".png"};
            if(!allowedEx.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file","Unsupported file extension");
            }

            if(request.File.Length > 10485760){
                ModelState.AddModelError("file","file size is too large");
            }
        }
    }
}