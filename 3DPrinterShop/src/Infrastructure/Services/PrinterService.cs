using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PrinterShop.Core.Application.Interfaces.Services;
using PrinterShop.Core.Domain.Entities;
using PrinterShop.Core.Infrastructure.Data;
using PrinterShop.Shared.Dtos;

namespace PrinterShop.Core.Infrastructure.Services;

public class PrinterService : IPrinterService
{
    private readonly ShopDbContext _dbContext;
    private readonly IValidator<PrinterDto> _validator;
    private readonly IMapper _mapper;
    
    public PrinterService(ShopDbContext dbContext, IValidator<PrinterDto> validator, IMapper mapper)
    {
        _dbContext = dbContext;
        _validator = validator;
        _mapper = mapper;
    }
    
    public async Task AddAsync(PrinterDto printer)
    {
        var result = await GetAsync(printer.Id);

        if (result is not null)
        {
            throw new ArgumentException($"Printer {printer.Id} already exists");
        }
        
        var validation = await _validator.ValidateAsync(printer);

        if (!validation.IsValid)
        {
            throw new ValidationException(validation.Errors);
        }
        
        var mapped = _mapper.Map<Printer>(printer);
        
        _dbContext.Printers.Add(mapped);
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PrinterDto> GetAsync(Guid id)
    {
        var result = await _dbContext.FindAsync<Printer>(id);

        if (result is null)
        {
            return null;
        }
        
        var mapped = _mapper.Map<PrinterDto>(result);
        
        return mapped;
    }

    public async Task<IEnumerable<PrinterDto>> GetAllAsync() => 
        (await _dbContext.Printers.ToListAsync())
        .Select(_mapper.Map<Printer, PrinterDto>);

    public async Task UpdateAsync(PrinterDto printer)
    {
        var result = await _dbContext.FindAsync<Printer>(printer.Id);

        if (result is null)
        {
            throw new ArgumentNullException();
        }
        
        var validation = await _validator.ValidateAsync(printer);

        if (!validation.IsValid)
        {
            throw new ValidationException(validation.Errors);
        }
        
        result.Brand = printer.Brand;
        result.Model = printer.Model;
        result.Extruder = printer.Extruder;
        result.MainBoard = printer.MainBoard;
        result.HeatingBed = printer.HeatingBed;
        result.StepperMotor = printer.StepperMotor;
        result.BedLevelingSensor = printer.BedLevelingSensor;
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var result = await _dbContext.FindAsync<Printer>(id);

        if (result is null)
        {
            throw new ArgumentNullException();
        }
        
        var mapped = _mapper.Map<Printer>(result);
        
        _dbContext.Printers.Remove(mapped);
        
        await _dbContext.SaveChangesAsync();
    }
}