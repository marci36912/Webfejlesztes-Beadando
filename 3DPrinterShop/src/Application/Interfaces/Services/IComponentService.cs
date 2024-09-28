using PrinterShop.Core.Domain.Entities;

namespace PrinterShop.Core.Application.Interfaces.Services;

public interface IComponentService
{
    Task AddAsync(Component component);
    
    Task<Component> GetAsync(Guid id);
    
    Task<List<Component>> GetAllAsync();
    
    Task UpdateAsync(Component component);
    
    Task DeleteAsync(Guid id);
}