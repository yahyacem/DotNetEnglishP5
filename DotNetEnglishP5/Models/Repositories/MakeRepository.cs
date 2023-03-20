using DotNetEnglishP5.Data;
using Microsoft.EntityFrameworkCore;

namespace DotNetEnglishP5.Models.Repositories
{
    public class MakeRepository : IMakeRepository
    {
        private ApplicationDbContext _context;
        public MakeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Make?> GetByIdAsync(int id)
        {
            return await _context.Make.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<Make?> GetByNameAsync(string name)
        {
            return await _context.Make.FirstOrDefaultAsync(m => m.Name.ToUpper() == name.ToUpper());
        }
        public async Task<IList<Make>> GetAllAsync()
        {
            return await _context.Make.Where(m => m.Id > 0).OrderBy(s => s.Name).ToListAsync();
        }
        public async Task SaveAsync(Make make)
        {
            if (make != null)
            {
                _context.Make.Add(make);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(int id)
        {
            Make makeToRemove = await _context.Make.FirstAsync(m => m.Id == id);
            if (makeToRemove != null)
            {
                _context.Make.Remove(makeToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}
