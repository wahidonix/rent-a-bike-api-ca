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

    }

    public interface IStationRepository
    {
        Task<IEnumerable<Station>> GetAllAsync();
        Task<Station> GetByIdAsync(int id);
        Task AddAsync(Station station);
        Task RemoveAsync(Station station);

    }

    public interface IRentalRepository
    {
        Task<IEnumerable<Rental>> GetAllAsync();
        Task<Rental> GetByIdAsync(int id);
        Task AddAsync(Rental rental);
        Task<IEnumerable<Rental>> GetByUserIdAsync(string userId);

    }

    public interface IServiceReportRepository
    {
        Task<IEnumerable<ServiceReport>> GetAllAsync();
        Task<ServiceReport> GetByIdAsync(int id);
        Task AddAsync(ServiceReport serviceReport);

    }
}
