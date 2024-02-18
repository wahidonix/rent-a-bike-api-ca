using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class Bike
{
    public int BikeId { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Identification number must be between 1 and 100 characters long.", MinimumLength = 1)]
    public string IdentificationNumber { get; set; }

    [Required]
    public string BikeType { get; set; }

    [Required]
    [RegularExpression(@"^[0-9A-Za-z]+$", ErrorMessage = "Lock code must be alphanumeric.")]
    public string LockCode { get; set; }

    public int StationId { get; set; }
    public Station Station { get; set; }

    public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}

public class Station
{
    public int StationId { get; set; }

    [Required]
    [StringLength(255, ErrorMessage = "Location must be between 1 and 255 characters long.", MinimumLength = 1)]
    public string Location { get; set; }

    public ICollection<Bike> Bikes { get; set; } = new List<Bike>();
}

public class Rental
{
    public int RentalId { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int BikeId { get; set; }
    public Bike Bike { get; set; }

    [Required]
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}

public class ServiceReport
{
    public int ServiceReportId { get; set; }

    public int BikeId { get; set; }
    public Bike Bike { get; set; }

    [Required]
    public DateTime ReportDate { get; set; }

    [Required]
    [StringLength(1000, ErrorMessage = "Description must be under 1000 characters.")]
    public string Description { get; set; }

    public bool IsResolved { get; set; }
}
