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
    public class AddServiceReportCommandHandler : IRequestHandler<AddServiceReportCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddServiceReportCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(AddServiceReportCommand request, CancellationToken cancellationToken)
        {
            var serviceReport = new ServiceReport
            {
                BikeId = request.BikeId,
                Description = request.Description,
                IsResolved = false, // Initially set to false
                ReportDate = DateTime.UtcNow
            };

            await _unitOfWork.ServiceReports.AddAsync(serviceReport);
            await _unitOfWork.CompleteAsync();

            return serviceReport.ServiceReportId;
        }
    }

    public class UpdateServiceReportStatusCommandHandler : IRequestHandler<UpdateServiceReportStatusCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateServiceReportStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateServiceReportStatusCommand request, CancellationToken cancellationToken)
        {
            var serviceReport = await _unitOfWork.ServiceReports.GetByIdAsync(request.ServiceReportId);
            if (serviceReport == null) return false;

            serviceReport.IsResolved = request.IsResolved;
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
    public class GetAllServiceReportsQueryHandler : IRequestHandler<GetAllServiceReportsQuery, IEnumerable<ServiceReportDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllServiceReportsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceReportDto>> Handle(GetAllServiceReportsQuery request, CancellationToken cancellationToken)
        {
            var serviceReports = await _unitOfWork.ServiceReports.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceReportDto>>(serviceReports);
        }
    }

    public class GetServiceReportByIdQueryHandler : IRequestHandler<GetServiceReportByIdQuery, ServiceReportDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetServiceReportByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceReportDto> Handle(GetServiceReportByIdQuery request, CancellationToken cancellationToken)
        {
            var serviceReport = await _unitOfWork.ServiceReports.GetByIdAsync(request.ServiceReportId);
            return _mapper.Map<ServiceReportDto>(serviceReport);
        }
    }
}
