using PrinterShop.Core.Application.Interfaces.Services;
using PrinterShop.Core.Domain.Entities;

namespace PrinterShop.Core.Infrastructure.Services;

public class ComponentService : IComponentService
{
    public Task AddAsync(Component component)
    {
        throw new NotImplementedException();
    }

    public Task<Component> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Component>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Component component)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}