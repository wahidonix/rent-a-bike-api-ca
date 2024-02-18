using Application.Commands;
using Application.DTOs;
using Application.Interfaces;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class AddStationCommandHandler : IRequestHandler<AddStationCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddStationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(AddStationCommand request, CancellationToken cancellationToken)
        {
            var station = new Station { Location = request.Location };
            await _unitOfWork.Stations.AddAsync(station);
            await _unitOfWork.CompleteAsync();
            return station.StationId;
        }
    }

    public class DeleteStationCommandHandler : IRequestHandler<DeleteStationCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteStationCommand request, CancellationToken cancellationToken)
        {
            var station = await _unitOfWork.Stations.GetByIdAsync(request.StationId);
            if (station == null)
            {
                return false; 
            }

            await _unitOfWork.Stations.RemoveAsync(station); 
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }



    public class GetAllStationsQueryHandler : IRequestHandler<GetAllStationsQuery, IEnumerable<StationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllStationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StationDto>> Handle(GetAllStationsQuery request, CancellationToken cancellationToken)
        {
            var stations = await _unitOfWork.Stations.GetAllAsync();
            return _mapper.Map<IEnumerable<StationDto>>(stations);
        }
    }

    public class GetStationByIdQueryHandler : IRequestHandler<GetStationByIdQuery, StationDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetStationByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StationDto> Handle(GetStationByIdQuery request, CancellationToken cancellationToken)
        {
            var station = await _unitOfWork.Stations.GetByIdAsync(request.StationId);
            return _mapper.Map<StationDto>(station);
        }
    }
}
