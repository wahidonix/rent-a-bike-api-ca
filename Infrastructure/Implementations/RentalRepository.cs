using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementations
{
    public class RentalRepository : IRentalRepository
    {
        private readonly DataContext _context;

        public RentalRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rental>> GetByUserIdAsync(string userId)
        {
            return await _context.Rentals
                                 .Where(r => r.UserId == userId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Rental>> GetAllAsync()
        {
            return await _context.Rentals.Include(r => r.Bike).ToListAsync();
        }

        public async Task<Rental> GetByIdAsync(int id)
        {
            return await _context.Rentals.Include(r => r.Bike).FirstOrDefaultAsync(r => r.RentalId == id);
        }

        public async Task AddAsync(Rental rental)
        {
            await _context.Rentals.AddAsync(rental);
        }

        // Implement other methods as needed
    }
}
