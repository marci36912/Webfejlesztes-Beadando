using PrinterShop.Shared.Dtos;
using Refit;

namespace PrinterShop.WebUi.Services;

[Headers("Content-Type: application/json")]
public interface IUserService
{
    [Post("/user")]
    Task AddAsync(UserDto user);
    
    [Get("/user/{id}")]
    Task<UserDto> GetAsync(Guid id);

    [Get("/user")]
    Task<IEnumerable<UserDto>> GetAllAsync();
    
    [Put("/user")]
    Task UpdateAsync(UserDto user);
    
    [Delete("/user/{id}")]
    Task DeleteAsync(Guid id);
}