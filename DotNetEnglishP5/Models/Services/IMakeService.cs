namespace DotNetEnglishP5.Models.Services
{
    public interface IMakeService
    {
        Task<MakeViewModel> GetMakeByIdAsync(int id);
        Task<MakeViewModel> GetMakeByNameAsync(string name);
        Task<IList<MakeViewModel>> GetAllMakesAsync();
        Task SaveMakeAsync(MakeViewModel make);
        Task DeleteMakeAsync(int id);
    }
}
