using PrinterShop.Shared.Dtos;

namespace PrinterShop.Core.Application.Interfaces.Services;

public interface IUserService
{
    Task AddAsync(UserDto user);
    
    Task<UserDto> GetAsync(Guid id);
    
    Task<List<UserDto>> GetAllAsync();
    
    Task UpdateAsync(UserDto user);
    
    Task DeleteAsync(Guid id);
}