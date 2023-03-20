using DotNetEnglishP5.Data;
using DotNetEnglishP5.Models.Repositories;

namespace DotNetEnglishP5.Models.Services
{
    public interface ICarService
    {
        Task<CarViewModel?> GetCarByIdAsync(int id);
        Task<IList<CarViewModel>> GetAllCarsAsync();
        Task SaveCarAsync(CarViewModel car);
        Task UpdateCarAsync(CarViewModel car);
        Task DeleteCarAsync(int id);
    }
}