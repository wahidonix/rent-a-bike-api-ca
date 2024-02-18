using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetAllServiceReportsQuery : IRequest<IEnumerable<ServiceReportDto>> { }

    public class GetServiceReportByIdQuery : IRequest<ServiceReportDto>
    {
        public int ServiceReportId { get; set; }
    }
}
