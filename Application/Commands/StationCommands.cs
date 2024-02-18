using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class AddStationCommand : IRequest<int>
    {
        public string Location { get; set; }
    }

    public class DeleteStationCommand : IRequest<bool>
    {
        public int StationId { get; set; }
    }
}
