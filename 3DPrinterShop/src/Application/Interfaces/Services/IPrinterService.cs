using PrinterShop.Core.Domain.Entities;

namespace PrinterShop.Core.Application.Interfaces.Services;

public interface IPrinterService
{
    Task AddAsync(Printer printer);
    
    Task<Printer> GetAsync(Guid id);
    
    Task<List<Printer>> GetAllAsync();
    
    Task UpdateAsync(Printer printer);
    
    Task DeleteAsync(Guid id);
}