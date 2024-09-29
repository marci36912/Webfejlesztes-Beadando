using PrinterShop.Shared.Dtos;

namespace PrinterShop.Core.Application.Interfaces.Services;

public interface IPrinterService
{
    Task AddAsync(PrinterDto printer);
    
    Task<PrinterDto> GetAsync(Guid id);
    
    Task<IEnumerable<PrinterDto>> GetAllAsync();
    
    Task UpdateAsync(PrinterDto printer);
    
    Task DeleteAsync(Guid id);
}