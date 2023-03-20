using DotNetEnglishP5.Data;

namespace DotNetEnglishP5.Models.Repositories
{
    public interface IModelRepository
    {
        Task<Model?> GetByIdAsync(int id);
        Task<Model?> GetByNameAsync(string name);
        Task<Model?> GetByNameAsync(string modelName, string makeName);
        Task<IList<Model>> GetAllAsync();
        Task SaveAsync(Model model);
        Task DeleteAsync(int id);
    }
}
