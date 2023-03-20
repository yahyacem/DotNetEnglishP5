using System.Drawing;
using DotNetEnglishP5.Models;

namespace DotNetEnglishP5.Helpers
{
    public static class Helper
    {
        public static ImageViewModel GetNoImagePNG()
        {
            ImageViewModel imageToReturn = new ImageViewModel();
            Image imageIn = Image.FromFile("Assets/PNG/no-photo-available.png");
            imageToReturn.FileName = "no-photo-available";
            imageToReturn.FileType = ".png";
            imageToReturn.Data = imageToByteArray(imageIn);
            return imageToReturn;
        }
        public static ImageViewModel GetImageFromURL(string url)
        {
            var fileInfo = new FileInfo(url);
            ImageViewModel imageToReturn = new ImageViewModel();
            Image imageIn = Image.FromFile(url);
            imageToReturn.FileName = Path.GetFileNameWithoutExtension(fileInfo.Name);
            imageToReturn.FileType = Path.GetExtension(fileInfo.Name);
            imageToReturn.Data = imageToByteArray(imageIn);
            return imageToReturn;
        }

        private static byte[] imageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
    }
}
