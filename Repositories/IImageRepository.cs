using GHWalk.Models.Domain;

namespace GHWalk.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}