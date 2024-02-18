using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.MapperProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Bike, BikeDto>().ReverseMap();
        CreateMap<Station, StationDto>().ReverseMap();
        CreateMap<Rental, RentalDto>().ReverseMap();
        CreateMap<ServiceReport, ServiceReportDto>().ReverseMap();
    }
}
