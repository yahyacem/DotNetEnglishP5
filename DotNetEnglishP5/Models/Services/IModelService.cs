namespace DotNetEnglishP5.Models.Services
{
    public interface IModelService
    {
        Task<ModelViewModel> GetModelByIdAsync(int id);
        Task<ModelViewModel> GetModelByNameAsync(string name);
        Task<ModelViewModel> GetModelByNameAsync(string modelName, string makeName);
        Task<IList<ModelViewModel>> GetAllModelsAsync();
        Task SaveModelAsync(ModelViewModel model);
        Task DeleteModelAsync(int id);
    }
}
