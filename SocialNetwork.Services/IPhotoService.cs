using Microsoft.AspNetCore.Http;

namespace SocialNetwork.Services
{
    public interface IPhotoService : IService
    {
        int Create(IFormFile photo, string userId);

        bool PhotoExists(int photoId);

        byte[] PhotoAsBytes(IFormFile photo);
    }
}