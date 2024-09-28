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
        service.AddSingleton<IComponentService, ComponentService>();
        service.AddSingleton<IPrinterService, PrinterService>();
        service.AddSingleton<IUserService, UserService>();
        service.AddSingleton<IOrderService, OrderService>();
        
        service.AddDbContext<ShopDbContext>(x => x
            .UseLazyLoadingProxies()
            .UseSqlite("DataSource=:memory:"));
        
        return service;
    }
}