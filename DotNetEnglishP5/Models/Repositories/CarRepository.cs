using DotNetEnglishP5.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace DotNetEnglishP5.Models.Repositories
{
    public class CarRepository : ICarRepository
    {
        private ApplicationDbContext _context;
        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Car?> GetByIdAsync(int id)
        {
            return await _context.Car.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IList<Car>> GetAllAsync()
        {
            IList<Car> carEntities = await _context.Car.Where(c => c.Id > 0).OrderBy(s => s.Id).ToListAsync();
            return carEntities;
        }
        public async Task<Car?> SaveAsync(Car car)
        {
            Car? carToAdd = car;
            if (carToAdd != null)
            {
                carToAdd = car;
                _context.Car.Add(carToAdd);
                await _context.SaveChangesAsync();
            }
            return carToAdd;
        }
        public async Task<Car?> UpdateAsync(Car car)
        {
            Car carToUpdate = car;
            if (carToUpdate != null)
            {
                carToUpdate = car;
                _context.Car.Update(carToUpdate);
                await _context.SaveChangesAsync();
            }
            return carToUpdate;
        }
        public async Task DeleteAsync(int id)
        {
            Car car = await _context.Car.FirstAsync(c => c.Id == id);
            if (car != null)
            {
                _context.Car.Remove(car);
                await _context.SaveChangesAsync();
            }
        }
    }
}