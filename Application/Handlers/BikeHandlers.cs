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

    public class GetAllBikesQueryHandler : IRequestHandler<GetAllBikesQuery, IEnumerable<BikeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllBikesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BikeDto>> Handle(GetAllBikesQuery request, CancellationToken cancellationToken)
        {
            var bikes = await _unitOfWork.Bikes.GetAllAsync();
            return _mapper.Map<IEnumerable<BikeDto>>(bikes);
        }
    }

    public class GetBikeByIdQueryHandler : IRequestHandler<GetBikeByIdQuery, BikeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBikeByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BikeDto> Handle(GetBikeByIdQuery request, CancellationToken cancellationToken)
        {
            var bike = await _unitOfWork.Bikes.GetByIdAsync(request.BikeId);
            return _mapper.Map<BikeDto>(bike);
        }
    }
    public class AddBikeCommandHandler : IRequestHandler<AddBikeCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddBikeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(AddBikeCommand request, CancellationToken cancellationToken)
        {
            var bike = new Bike
            {
                IdentificationNumber = request.IdentificationNumber,
                BikeType = request.BikeType,
                LockCode = request.LockCode,
                StationId = request.StationId
            };

            await _unitOfWork.Bikes.AddAsync(bike);
            await _unitOfWork.CompleteAsync();

            return bike.BikeId;
        }
    }
    public class RentBikeCommandHandler : IRequestHandler<RentBikeCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RentBikeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(RentBikeCommand request, CancellationToken cancellationToken)
        {
            var rental = new Rental
            {
                BikeId = request.BikeId,
                UserId = request.UserId,
                StartTime = DateTime.UtcNow
            };

            await _unitOfWork.Rentals.AddAsync(rental);
            await _unitOfWork.CompleteAsync();

            return rental.RentalId;
        }
    }

    public class ReturnBikeCommandHandler : IRequestHandler<ReturnBikeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReturnBikeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ReturnBikeCommand request, CancellationToken cancellationToken)
        {
            var rental = await _unitOfWork.Rentals.GetByIdAsync(request.RentalId);
            if (rental == null) return false;

            rental.EndTime = DateTime.UtcNow; // Mark the rental as ended

            var bike = await _unitOfWork.Bikes.GetByIdAsync(rental.BikeId);
            bike.StationId = request.StationId; // Update the bike's current station

            await _unitOfWork.CompleteAsync();

            return true;
        }
    }

    public class CreateServiceReportCommandHandler : IRequestHandler<CreateServiceReportCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateServiceReportCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateServiceReportCommand request, CancellationToken cancellationToken)
        {
            var serviceReport = new ServiceReport
            {
                BikeId = request.BikeId,
                Description = request.Description,
                ReportDate = DateTime.UtcNow,
                IsResolved = false // Initially, the report is not resolved
            };

            await _unitOfWork.ServiceReports.AddAsync(serviceReport);
            await _unitOfWork.CompleteAsync();

            return serviceReport.ServiceReportId;
        }
    }

}
