using PrinterShop.Shared.Dtos;
using Refit;

namespace WebUi.Services;


[Headers("Content-Type: application/json")]
public interface IOrderService
{
    [Post("/order")]
    Task AddAsync(OrderDto order);
    
    [Get("/order/{id}")]
    Task<OrderDto> GetAsync(Guid id);

    [Get("/order")]
    Task<IEnumerable<OrderDto>> GetAllAsync();
    
    [Put("/order")]
    Task UpdateAsync(OrderDto user);
    
    [Delete("/order/{id}")]
    Task DeleteAsync(Guid id);
}