using PrinterShop.Core.Domain.Entities;

namespace PrinterShop.Core.Application.Interfaces.Services;

public interface IUserService
{
    Task AddAsync(User user);
    
    Task<User> GetAsync(Guid id);
    
    Task<List<User>> GetAllAsync();
    
    Task UpdateAsync(User user);
    
    Task DeleteAsync(Guid id);
}