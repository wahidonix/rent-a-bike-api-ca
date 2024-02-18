using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetAllStationsQuery : IRequest<IEnumerable<StationDto>> { }

    public class GetStationByIdQuery : IRequest<StationDto>
    {
        public int StationId { get; set; }
    }
}
