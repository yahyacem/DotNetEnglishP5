using DotNetEnglishP5.Data;
using Microsoft.EntityFrameworkCore;

namespace DotNetEnglishP5.Models.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private ApplicationDbContext _context;
        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Image?> GetByIdAsync(int id)
        {
            return await _context.Image.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<Image>> GetByCarAsync(Car car)
        {
            return await _context.Image.Where(x => x.Car.Id == car.Id).OrderBy(s => s.Id).ToListAsync();
        }
        public async Task<IEnumerable<Image>> GetByCarIdAsync(int id)
        {
            return await _context.Image.Where(x => x.Car.Id == id).OrderBy(s => s.Id).ToListAsync();
        }
        public async Task<IEnumerable<Image>> GetAllAsync()
        {
            return await _context.Image.Where(x => x.Id > 0).OrderBy(s => s.Id).ToListAsync();
        }
        public async Task SaveAsync(Image image)
        {
            if (image != null)
            {
                _context.Image.Add(image);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(int id)
        {
            Image image = await _context.Image.FirstAsync(x => x.Id == id);
            if (image != null)
            {
                _context.Image.Remove(image);
                await _context.SaveChangesAsync();
            }
        }
    }
}
