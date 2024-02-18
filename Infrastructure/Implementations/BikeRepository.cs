using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementations
{
    public class BikeRepository : IBikeRepository
    {
        private readonly DataContext _context;

        public BikeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bike>> GetAllAsync()
        {
            return await _context.Bikes.ToListAsync();
        }

        public async Task<Bike> GetByIdAsync(int id)
        {
            return await _context.Bikes.FindAsync(id);
        }

        public async Task AddAsync(Bike bike)
        {
            await _context.Bikes.AddAsync(bike);
        }

        public async Task RemoveAsync(Bike bike)
        {
            _context.Bikes.Remove(bike);
        }

        // Implement other methods defined in the interface
    }
}