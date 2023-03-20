using DotNetEnglishP5.Data;

namespace DotNetEnglishP5.Models.Repositories
{
    public interface IMakeRepository
    {
        Task<IList<Make>> GetAllAsync();
        Task<Make?> GetByNameAsync(string name);
        Task<Make?> GetByIdAsync(int id);
        Task SaveAsync(Make make);
        Task DeleteAsync(int id);
    }
}
