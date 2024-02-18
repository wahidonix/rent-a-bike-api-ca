using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries;

public class GetAllBikesQuery : IRequest<IEnumerable<BikeDto>> { }

public class GetBikeByIdQuery : IRequest<BikeDto>
{
    public int BikeId { get; }

    public GetBikeByIdQuery(int bikeId)
    {
        BikeId = bikeId;
    }
}
