using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AddBikeCommand : IRequest<int>
    {
        public string IdentificationNumber { get; set; }
        public string BikeType { get; set; }
        public string LockCode { get; set; }
        public int StationId { get; set; }
    }
    public class RentBikeCommand : IRequest<int>
    {
        public int BikeId { get; set; }
        public string UserId { get; set; }
    }
    public class ReturnBikeCommand : IRequest<bool>
    {
        public int RentalId { get; set; }
        public int StationId { get; set; }
    }
    public class CreateServiceReportCommand : IRequest<int>
    {
        public int BikeId { get; set; }
        public string Description { get; set; }
    }

}
