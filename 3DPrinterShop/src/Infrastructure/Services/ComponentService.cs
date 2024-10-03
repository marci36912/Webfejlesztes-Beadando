using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PrinterShop.Core.Application.Interfaces.Services;
using PrinterShop.Core.Domain.Entities;
using PrinterShop.Core.Infrastructure.Data;
using PrinterShop.Shared.Dtos;

namespace PrinterShop.Core.Infrastructure.Services;

public class ComponentService : IComponentService
{
    private readonly ShopDbContext _dbContext;
    private readonly IValidator<ComponentDto> _validator;
    private readonly IMapper _mapper;

    public ComponentService(ShopDbContext dbContext, IValidator<ComponentDto> validator, IMapper mapper)
    {
        _dbContext = dbContext;
        _validator = validator;
        _mapper = mapper;
    }
    
    public async Task AddAsync(ComponentDto component)
    {
        var result = await GetAsync(component.ModelNumber);

        if (result is not null)
        {
            throw new ArgumentException($"Component {component.ModelNumber} already exists");
        }
        
        var validation = await _validator.ValidateAsync(component);

        if (!validation.IsValid)
        {
            throw new ValidationException(validation.Errors);
        }
        
        var mapped = _mapper.Map<Component>(component);
        
        _dbContext.Components.Add(mapped);
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ComponentDto> GetAsync(Guid id)
    {
        var result = await _dbContext.FindAsync<Component>(id);

        if (result is null)
        {
            return null;
        }
        
        var mapped = _mapper.Map<ComponentDto>(result);
        
        return mapped;
    }

    public async Task<IEnumerable<ComponentDto>> GetAllAsync() => 
        (await _dbContext.Components.ToListAsync())
        .Select(_mapper.Map<Component, ComponentDto>);

    public async Task UpdateAsync(ComponentDto component)
    {
        var result = await _dbContext.FindAsync<Component>(component.ModelNumber);

        if (result is null)
        {
            throw new ArgumentNullException();
        }
        
        var validation = await _validator.ValidateAsync(component);

        if (!validation.IsValid)
        {
            throw new ValidationException(validation.Errors);
        }
        
        result.Brand = component.Brand;
        result.Description = component.Description;
        result.Price = component.Price;
        result.Model = component.Model;
        result.ComponentType = component.ComponentType;
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var result = await _dbContext.FindAsync<Component>(id);

        if (result is null)
        {
            throw new ArgumentNullException();
        }
        
        var mapped = _mapper.Map<Component>(result);
        
        _dbContext.Components.Remove(mapped);
        
        await _dbContext.SaveChangesAsync();
    }
}