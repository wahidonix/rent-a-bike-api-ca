using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBikeRepository
    {
        Task<IEnumerable<Bike>> GetAllAsync();
        Task<Bike> GetByIdAsync(int id);
        Task AddAsync(Bike bike);
        Task RemoveAsync(Bike bike);
        // Add other methods specific to bike operations
    }

    public interface IStationRepository
    {
        Task<IEnumerable<Station>> GetAllAsync();
        Task<Station> GetByIdAsync(int id);
        Task AddAsync(Station station);
        Task RemoveAsync(Station station);
        // Add other methods specific to station operations
    }

    public interface IRentalRepository
    {
        Task<IEnumerable<Rental>> GetAllAsync();
        Task<Rental> GetByIdAsync(int id);
        Task AddAsync(Rental rental);
        Task<IEnumerable<Rental>> GetByUserIdAsync(string userId);
        // Add other methods specific to rental operations
    }

    public interface IServiceReportRepository
    {
        Task<IEnumerable<ServiceReport>> GetAllAsync();
        Task<ServiceReport> GetByIdAsync(int id);
        Task AddAsync(ServiceReport serviceReport);
        // Add other methods specific to service report operations
    }
}
