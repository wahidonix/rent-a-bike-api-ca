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
    public class ServiceReportRepository : IServiceReportRepository
    {
        private readonly DataContext _context;

        public ServiceReportRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceReport>> GetAllAsync()
        {
            return await _context.ServiceReports.Include(sr => sr.Bike).ToListAsync();
        }

        public async Task<ServiceReport> GetByIdAsync(int id)
        {
            return await _context.ServiceReports.Include(sr => sr.Bike).FirstOrDefaultAsync(sr => sr.ServiceReportId == id);
        }

        public async Task AddAsync(ServiceReport serviceReport)
        {
            await _context.ServiceReports.AddAsync(serviceReport);
        }

        // Implement other methods as needed
    }
}
