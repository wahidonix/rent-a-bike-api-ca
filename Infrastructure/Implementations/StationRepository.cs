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
    public class StationRepository : IStationRepository
    {
        private readonly DataContext _context;

        public StationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Station>> GetAllAsync()
        {
            return await _context.Stations.ToListAsync();
        }

        public async Task<Station> GetByIdAsync(int id)
        {
            return await _context.Stations.FindAsync(id);
        }

        public async Task AddAsync(Station station)
        {
            await _context.Stations.AddAsync(station);
        }

        public async Task RemoveAsync(Station station)
        {
            _context.Stations.Remove(station);
        }

    }
}
