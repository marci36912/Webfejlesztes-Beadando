using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrinterShop.Core.Infrastructure.Data;

namespace PrinterShop.Core.Infrastructure;

public static class AddServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection service)
    {
        service.AddDbContext<ShopDbContext>(x => x
            .UseLazyLoadingProxies()
            .UseSqlite("DataSource=:memory:"));
        
        return service;
    }
}