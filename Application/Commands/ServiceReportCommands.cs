using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AddServiceReportCommand : IRequest<int>
    {
        public int BikeId { get; set; }
        public string Description { get; set; }
    }

    public class UpdateServiceReportStatusCommand : IRequest<bool>
    {
        public int ServiceReportId { get; set; }
        public bool IsResolved { get; set; }
    }
}
