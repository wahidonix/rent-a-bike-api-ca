using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Implementations;

namespace Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Bikes = new BikeRepository(_context);
            Stations = new StationRepository(_context);
            Rentals = new RentalRepository(_context);
            ServiceReports = new ServiceReportRepository(_context);
        }

        public IBikeRepository Bikes { get; private set; }
        public IStationRepository Stations { get; private set; }
        public IRentalRepository Rentals { get; private set; }
        public IServiceReportRepository ServiceReports { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
