using PrinterShop.Shared.Dtos;

namespace PrinterShop.Core.Application.Interfaces.Services;

public interface IComponentService
{
    Task AddAsync(ComponentDto component);
    
    Task<ComponentDto> GetAsync(Guid id);
    
    Task<IEnumerable<ComponentDto>> GetAllAsync();
    
    Task UpdateAsync(ComponentDto component);
    
    Task DeleteAsync(Guid id);
}