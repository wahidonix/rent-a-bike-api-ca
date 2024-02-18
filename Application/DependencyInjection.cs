using Application.Interfaces;
using Application.MapperProfiles;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;


public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
