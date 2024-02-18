using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BikeDto
    {
        public int BikeId { get; set; }
        public string IdentificationNumber { get; set; }
        public string BikeType { get; set; }
        public string LockCode { get; set; }
        public int StationId { get; set; }
    }

    public class StationDto
    {
        public int StationId { get; set; }
        public string Location { get; set; }
    }

    public class RentalDto
    {
        public int RentalId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int BikeId { get; set; }
        public string UserId { get; set; }
    }

    public class ServiceReportDto
    {
        public int ServiceReportId { get; set; }
        public int BikeId { get; set; }
        public DateTime ReportDate { get; set; }
        public string Description { get; set; }
        public bool IsResolved { get; set; }
    }
}

