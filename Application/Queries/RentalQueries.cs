using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetAllRentalsQuery : IRequest<IEnumerable<RentalDto>> { }

    public class GetRentalByIdQuery : IRequest<RentalDto>
    {
        public int RentalId { get; set; }
    }
    public class GetRentalsByUserIdQuery : IRequest<IEnumerable<RentalDto>>
    {
        public string UserId { get; set; }

        public GetRentalsByUserIdQuery(string userId)
        {
            UserId = userId;
        }
    }

}
