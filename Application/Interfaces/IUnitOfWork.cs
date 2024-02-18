namespace Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBikeRepository Bikes { get; }
        IStationRepository Stations { get; }
        IRentalRepository Rentals { get; }
        IServiceReportRepository ServiceReports { get; }
        Task<int> CompleteAsync();
    }
}
