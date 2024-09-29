using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PrinterShop.Core.Application.Interfaces.Services;
using PrinterShop.Core.Domain.Entities;
using PrinterShop.Core.Infrastructure.Data;
using PrinterShop.Shared.Dtos;

namespace PrinterShop.Core.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly ShopDbContext _dbContext;
    private readonly IValidator<OrderDto> _validator;
    private readonly IMapper _mapper;
    
    public OrderService(ShopDbContext dbContext, IValidator<OrderDto> validator, IMapper mapper)
    {
        _dbContext = dbContext;
        _validator = validator;
        _mapper = mapper;
    }
    
    public async Task AddAsync(OrderDto order)
    {
        var result = await GetAsync(order.Id);

        if (result is not null)
        {
            throw new ArgumentException($"Order already exists");
        }
        
        var validation = await _validator.ValidateAsync(order);

        if (!validation.IsValid)
        {
            throw new ValidationException(validation.Errors);
        }
        
        var mapped = _mapper.Map<Order>(order);
        
        _dbContext.Orders.Add(mapped);
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task<OrderDto> GetAsync(Guid id)
    {
        var result = await _dbContext.FindAsync<Order>(id);

        if (result is null)
        {
            return null;
        }
        
        var mapped = _mapper.Map<OrderDto>(result);
        
        return mapped;
    }

    public async Task<IEnumerable<OrderDto>> GetAllAsync() => 
        (await _dbContext.Orders.ToListAsync())
        .Select(_mapper.Map<Order, OrderDto>);

    public async Task UpdateAsync(OrderDto order)
    {
        var result = await GetAsync(order.Id);

        if (result is null)
        {
            throw new ArgumentNullException();
        }
        
        var validation = await _validator.ValidateAsync(order);

        if (!validation.IsValid)
        {
            throw new ValidationException(validation.Errors);
        }

        result.Components = order.Components;
        result.CustomerId = order.CustomerId;
        result.PrinterId = order.PrinterId;
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var result = await _dbContext.FindAsync<Order>(id);

        if (result is null)
        {
            throw new ArgumentNullException();
        }
        
        var mapped = _mapper.Map<Order>(result);
        
        _dbContext.Orders.Remove(mapped);
        
        await _dbContext.SaveChangesAsync();
    }
}