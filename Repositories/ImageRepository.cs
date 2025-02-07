using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GHWalk.Data;
using GHWalk.Models.Domain;

namespace GHWalk.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GHWalksDbContext _dbContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, GHWalksDbContext dbContext )
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Image> Upload(Image image)
        {
           var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath,"Images",$"{image.FileName}{image.FileExtension}");

           //Upload Image to Local Path
           using var stream = new FileStream(localFilePath, FileMode.Create);
           await image.File.CopyToAsync(stream);

           var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

           image.FilePath = urlFilePath;

           //Add image to Image Table
           await _dbContext.Images.AddAsync(image);
           await _dbContext.SaveChangesAsync();

           return image;

        }
    }
}