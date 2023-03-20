using DotNetEnglishP5.Data;
using Microsoft.EntityFrameworkCore;

namespace DotNetEnglishP5.Models.Repositories
{
    public class ModelRepository : IModelRepository
    {
        private ApplicationDbContext _context;
        public ModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Model?> GetByIdAsync(int id)
        {
            return await _context.Model.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<Model?> GetByNameAsync(string name)
        {
            return await _context.Model.FirstOrDefaultAsync(m => m.Name.ToUpper() == name.ToUpper());
        }
        public async Task<Model?> GetByNameAsync(string modelName, string makeName)
        {
            return await _context.Model.FirstOrDefaultAsync(m =>
            m.Name.ToUpper() == modelName.ToUpper()
            && m.Make.Name.ToUpper() == makeName.ToUpper());
        }
        public async Task<IList<Model>> GetAllAsync()
        {
            return await _context.Model.Where(m => m.Id > 0).OrderBy(s => s.Name).ToListAsync();
        }
        public async Task SaveAsync(Model model)
        {
            if (model != null)
            {
                _context.Model.Add(model);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(int id)
        {
            Model modelToRemove = await _context.Model.FirstAsync(m => m.Id == id);
            if (modelToRemove != null)
            {
                _context.Model.Remove(modelToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}