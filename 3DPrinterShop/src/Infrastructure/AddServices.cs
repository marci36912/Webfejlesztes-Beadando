using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrinterShop.Core.Application.Interfaces.Services;
using PrinterShop.Core.Infrastructure.Data;
using PrinterShop.Core.Infrastructure.Services;

namespace PrinterShop.Core.Infrastructure;

public static class AddServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection service)
    {
        service.AddScoped<IComponentService, ComponentService>();
        service.AddScoped<IPrinterService, PrinterService>();
        service.AddScoped<IUserService, UserService>();
        service.AddScoped<IOrderService, OrderService>();
        
        service.AddDbContext<ShopDbContext>(x => x
            .UseLazyLoadingProxies()
            .UseSqlite("Data Source=prod.db"));
        
        return service;
    }
}