namespace DotNetEnglishP5.Models.Services
{
    public interface IImageService
    {
        Task<ImageViewModel> GetImageByIdAsync(int id);
        Task<IList<ImageViewModel>> GetImagesByCarAsync(CarViewModel car);
        Task<IList<ImageViewModel>> GetAllImagesAsync();
        Task SaveImageAsync(ImageViewModel image);
        Task SaveImageAsync(ImageViewModel images, int carId);
        Task SaveImagesAsync(IFormFile[] images, int carId);
        Task SaveImagesAsync(IEnumerable<ImageViewModel> image);
        Task DeleteImageAsync(int id);
    }
}
