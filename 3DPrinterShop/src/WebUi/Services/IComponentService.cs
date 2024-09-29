using PrinterShop.Shared.Dtos;
using Refit;

namespace WebUi.Services;

[Headers("Content-Type: application/json")]
public interface IComponentService
{
    [Post("/component")]
    Task AddAsync(ComponentDto component);
    
    [Get("/component/{id}")]
    Task<ComponentDto> GetAsync(Guid id);

    [Get("/component")]
    Task<IEnumerable<ComponentDto>> GetAllAsync();
    
    [Put("/component")]
    Task UpdateAsync(ComponentDto user);
    
    [Delete("/component/{id}")]
    Task DeleteAsync(Guid id);
}