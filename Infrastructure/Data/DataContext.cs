using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data;

public class DataContext : IdentityDbContext<ApplicationUser>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    public DbSet<Bicycle> Bicycles { get; set; }
    public DbSet<Bike> Bikes { get; set; }
    public DbSet<Station> Stations { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<ServiceReport> ServiceReports { get; set; }
}
