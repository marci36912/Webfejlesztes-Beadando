using PrinterShop.Core.Application.Interfaces.Services;
using PrinterShop.Core.Domain.Entities;

namespace PrinterShop.Core.Infrastructure.Services;

public class OrderService : IOrderService
{
    public Task AddAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Order>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}