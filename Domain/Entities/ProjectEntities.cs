using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Bike
{
    public int BikeId { get; set; }
    public string IdentificationNumber { get; set; }
    public string BikeType { get; set; }
    public string LockCode { get; set; }
    public int StationId { get; set; }
    public Station Station { get; set; }
    public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}

public class Station
{
    public int StationId { get; set; }
    public string Location { get; set; }
    public ICollection<Bike> Bikes { get; set; } = new List<Bike>();
}

public class Rental
{
    public int RentalId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int BikeId { get; set; }
    public Bike Bike { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}

public class ServiceReport
{
    public int ServiceReportId { get; set; }
    public int BikeId { get; set; }
    public Bike Bike { get; set; }
    public DateTime ReportDate { get; set; }
    public string Description { get; set; }
    public bool IsResolved { get; set; }
}

// ApplicationUser class extends IdentityUser, includes custom properties like Credits
