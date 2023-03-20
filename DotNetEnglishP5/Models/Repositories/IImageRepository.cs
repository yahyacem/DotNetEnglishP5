using DotNetEnglishP5.Data;

namespace DotNetEnglishP5.Models.Repositories
{
    public interface IImageRepository
    {
        Task<Image?> GetByIdAsync(int id);
        Task<IEnumerable<Image>> GetByCarAsync(Car car);
        Task<IEnumerable<Image>> GetByCarIdAsync(int id);
        Task<IEnumerable<Image>> GetAllAsync();
        Task SaveAsync(Image image);
        Task DeleteAsync(int id);
    }
}
