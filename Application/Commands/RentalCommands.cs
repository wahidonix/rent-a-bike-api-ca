using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AddRentalCommand : IRequest<int>
    {
        public int BikeId { get; set; }
        public string UserId { get; set; }
    }

    public class ReturnRentalCommand : IRequest<bool>
    {
        public int RentalId { get; set; }
        public int StationId { get; set; }
    }
}
