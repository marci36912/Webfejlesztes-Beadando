﻿using PrinterShop.Shared.Dtos;

namespace PrinterShop.Core.Application.Interfaces.Services;

public interface IOrderService
{
    Task AddAsync(OrderDto order);
    
    Task<OrderDto> GetAsync(Guid id);
    
    Task<IEnumerable<OrderDto>> GetAllAsync();
    
    Task UpdateAsync(OrderDto order);
    
    Task DeleteAsync(Guid id);
}