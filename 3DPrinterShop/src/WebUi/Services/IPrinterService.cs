using PrinterShop.Shared.Dtos;
using Refit;

namespace PrinterShop.WebUi.Services;

[Headers("Content-Type: application/json")]
public interface IPrinterService
{
    [Post("/printer")]
    Task AddAsync(PrinterDto printer);
    
    [Get("/printer/{id}")]
    Task<PrinterDto> GetAsync(Guid id);

    [Get("/printer")]
    Task<IEnumerable<PrinterDto>> GetAllAsync();
    
    [Put("/printer")]
    Task UpdateAsync(PrinterDto user);
    
    [Delete("/printer/{id}")]
    Task DeleteAsync(Guid id);
}