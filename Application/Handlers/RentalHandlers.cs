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

    public class GetAllRentalsQueryHandler : IRequestHandler<GetAllRentalsQuery, IEnumerable<RentalDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllRentalsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RentalDto>> Handle(GetAllRentalsQuery request, CancellationToken cancellationToken)
        {
            var rentals = await _unitOfWork.Rentals.GetAllAsync();
            return _mapper.Map<IEnumerable<RentalDto>>(rentals);
        }
    }

    public class GetRentalByIdQueryHandler : IRequestHandler<GetRentalByIdQuery, RentalDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRentalByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RentalDto> Handle(GetRentalByIdQuery request, CancellationToken cancellationToken)
        {
            var rental = await _unitOfWork.Rentals.GetByIdAsync(request.RentalId);
            return _mapper.Map<RentalDto>(rental);
        }
    }
    public class GetRentalsByUserIdQueryHandler : IRequestHandler<GetRentalsByUserIdQuery, IEnumerable<RentalDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRentalsByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RentalDto>> Handle(GetRentalsByUserIdQuery request, CancellationToken cancellationToken)
        {
            // Assuming there's a method in your rentals repository to get rentals by user ID
            var rentals = await _unitOfWork.Rentals.GetByUserIdAsync(request.UserId);
            return _mapper.Map<IEnumerable<RentalDto>>(rentals);
        }
    }

    public class AddRentalCommandHandler : IRequestHandler<AddRentalCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddRentalCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(AddRentalCommand request, CancellationToken cancellationToken)
        {
            var rental = new Rental
            {
                BikeId = request.BikeId,
                UserId = request.UserId,
                StartTime = DateTime.UtcNow,
            };

            await _unitOfWork.Rentals.AddAsync(rental);
            await _unitOfWork.CompleteAsync();

            return rental.RentalId;
        }
    }

    public class ReturnRentalCommandHandler : IRequestHandler<ReturnRentalCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReturnRentalCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ReturnRentalCommand request, CancellationToken cancellationToken)
        {
 
            var rental = await _unitOfWork.Rentals.GetByIdAsync(request.RentalId);
            if (rental == null) return false;


            rental.EndTime = DateTime.UtcNow;


            var bike = await _unitOfWork.Bikes.GetByIdAsync(rental.BikeId);
            if (bike == null) return false;

            bike.StationId = request.StationId;


            await _unitOfWork.CompleteAsync();

            return true;
        }
    }

}
