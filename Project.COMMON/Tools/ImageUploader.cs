using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.COMMON.Tools
{
    public static class ImageUploader
    {
        public static string? UploadImageToWwwroot(IFormFile pictureToUpload, IFileProvider fileProvider, string nameOfTheFileToUploadImageInWwwroot, out string? entityImagePath)
        {
            entityImagePath = null;
            if (pictureToUpload == null) return "To be upload picture is empty !";

            string extension = Path.GetExtension(pictureToUpload.FileName).ToLower();

            if (extension == ".jpg" || extension == ".gif" || extension == ".png" || extension == ".jpeg")
            {
                IDirectoryContents wwwroot = fileProvider.GetDirectoryContents("wwwroot");
                IFileInfo? locationToUpload = wwwroot.FirstOrDefault(x => x.Name == nameOfTheFileToUploadImageInWwwroot);

                if (locationToUpload == null) return "Couldn't find location to upload";

                string randomFileName = $"{Guid.NewGuid()}{extension}";
                string newPicturePath = Path.Combine(locationToUpload.PhysicalPath!, randomFileName);

                using FileStream stream = new FileStream(newPicturePath, FileMode.Create);
                pictureToUpload.CopyTo(stream);

                entityImagePath = randomFileName;
                return null;
            }
            else return "Selected file is not a picture !  Only jpg, gif, png and jpeg extensions are accepted.";
        }
    }
}
