using DotNetEnglishP5.Data;

namespace DotNetEnglishP5.Models.Repositories
{
    public interface ICarRepository
    {
        Task<Car?> GetByIdAsync(int id);
        Task<IList<Car>> GetAllAsync();
        Task<Car?> SaveAsync(Car car);
        Task<Car?> UpdateAsync(Car car);
        Task DeleteAsync(int id);
    }
}