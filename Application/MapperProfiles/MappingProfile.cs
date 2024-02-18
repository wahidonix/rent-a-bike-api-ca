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
        CreateMap<Rental, RentalDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id)) // Assuming ApplicationUser has an Id property
            .ReverseMap();
        CreateMap<ServiceReport, ServiceReportDto>().ReverseMap();
    }
}
