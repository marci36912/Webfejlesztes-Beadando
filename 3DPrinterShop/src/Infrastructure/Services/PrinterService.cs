using PrinterShop.Core.Application.Interfaces.Services;
using PrinterShop.Core.Domain.Entities;

namespace PrinterShop.Core.Infrastructure.Services;

public class PrinterService : IPrinterService
{
    public Task AddAsync(Printer printer)
    {
        throw new NotImplementedException();
    }

    public Task<Printer> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Printer>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Printer printer)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}