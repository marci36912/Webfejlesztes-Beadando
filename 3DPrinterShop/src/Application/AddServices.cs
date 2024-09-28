using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PrinterShop.Shared.Validators;

namespace PrinterShop.Core.Application;

public static class AddServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(MappingProfile).Assembly);
        
        service.AddValidatorsFromAssemblyContaining(typeof(UserValidator));
        
        return service;
    }
}