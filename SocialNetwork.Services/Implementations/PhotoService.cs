using Microsoft.AspNetCore.Http;
using SocialNetwork.Data;
using SocialNetwork.Data.Entities;
using System.IO;
using System.Linq;

namespace SocialNetwork.Services.Implementations
{
    public class PhotoService : IPhotoService
    {
        private readonly SocialNetworkDbContext db;

        public PhotoService(SocialNetworkDbContext db)
        {
            this.db = db;
        }

        public int Create(IFormFile photo, string userId)
        {
            using (var memoryStream = new MemoryStream())
            {
                photo.CopyTo(memoryStream);

                var instanceOfPhoto = new Photo
                {
                    IsProfilePicture = false,
                    PhotoAsBytes = memoryStream.ToArray(),
                    UserId = userId
                };

                db.Photos.Add(instanceOfPhoto);
                db.SaveChanges();

                return instanceOfPhoto.Id;
            }
        }

        public byte[] PhotoAsBytes(IFormFile photo)
        {
            byte[] photoAsBytes;

            using (var memoryStream = new MemoryStream())
            {
                photo.CopyTo(memoryStream);
                photoAsBytes = memoryStream.ToArray();
            };

            return photoAsBytes;
        }

        public bool PhotoExists(int photoId) => this.db.Photos.Any(p => p.Id == photoId);
    }
}