namespace PugnaFighting.Services.Data
{
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;

    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadAsync(IFormFile file, string fileName, string folder)
        {
            byte[] destinationImage;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                destinationImage = memoryStream.ToArray();
            }

            UploadResult uploadResult = null;

            using (var destinationStream = new MemoryStream(destinationImage))
            {
                var uploadparams = new ImageUploadParams()
                {
                    Folder = folder,
                    File = new FileDescription(fileName, destinationStream),
                };

                uploadResult = await this.cloudinary.UploadAsync(uploadparams);
            }

            return uploadResult?.SecureUri.AbsoluteUri;
        }
    }
}
