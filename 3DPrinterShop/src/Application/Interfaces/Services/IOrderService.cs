using PrinterShop.Core.Domain.Entities;

namespace PrinterShop.Core.Application.Interfaces.Services;

public interface IOrderService
{
    Task AddAsync(Order order);
    
    Task<Order> GetAsync(Guid id);
    
    Task<List<Order>> GetAllAsync();
    
    Task UpdateAsync(Order order);
    
    Task DeleteAsync(Guid id);
}