using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MockQueryable.NSubstitute;
using NSubstitute;
using NSubstitute.Extensions;
using PrinterShop.Core.Application;
using PrinterShop.Core.Domain.Entities;
using PrinterShop.Core.Infrastructure.Data;
using PrinterShop.Core.Infrastructure.Services;
using PrinterShop.Shared.Dtos;

namespace PrinterShop.InfrastructureUnitTests;

public class InfrastructureOrderServicesTests
{
    private readonly ShopDbContext _dbContext;
    private readonly OrderService _service;
    private readonly IMapper _mapper;

    public InfrastructureOrderServicesTests()
    {
        _dbContext = NSubstitute.Substitute.For<ShopDbContext>();
        
        var mockedOrders = (new List<Order>()).BuildMockDbSet();
        _dbContext.Orders = mockedOrders;
        
        var mockedValidator = NSubstitute.Substitute.For<IValidator<OrderDto>>();
        
        var mockedResult = NSubstitute.Substitute.For<ValidationResult>();
        mockedResult.IsValid.Returns(true);
        
        mockedValidator
            .Configure()
            .ValidateAsync(Arg.Any<OrderDto>())
            .Returns(mockedResult);

        _mapper = new MapperConfiguration(x => x
                .AddProfile(new MappingProfile()))
            .CreateMapper();
        
        _service = new(_dbContext, mockedValidator, _mapper);
    }

    [Fact]
    public async Task ValidOrder_AddAsync_AddsSuccessfully()
    {
        // Arrange
        var order = EntityBuilder(Guid.NewGuid());
        
        // Act
        await _service.AddAsync(order);
        
        // Assert
        _dbContext.Orders.Received().Add(Arg.Any<Order>());
        await _dbContext.Received().SaveChangesAsync();
    }

    [Fact]
    public async Task ValidOrder_GetAsync_ReturnsOrder()
    {
        // Arrange
        var order = EntityBuilder(Guid.NewGuid());
        
        var mappedOrder = _mapper.Map<Order>(order);
        
        _dbContext.FindAsync<Order>(order.Id).Returns(mappedOrder);
        
        // Act
        var result = await _service.GetAsync(order.Id);
        
        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task List_GetAllAsync_ReturnsAllComponents()
    {
        // Act
        var list = await _service.GetAllAsync();
        
        // Assert
        Assert.NotNull(list);
    }

    [Fact]
    public async Task ValidOrder_UpdateAsync_UpdatesSuccessfully()
    {
        // Arrange
        var order = EntityBuilder(Guid.NewGuid());
        
        var mappedOrder = _mapper.Map<Order>(order);
        
        _dbContext.FindAsync<Order>(order.Id).Returns(mappedOrder);
        
        var updatedOrder = order;
        updatedOrder.CustomerId = Guid.NewGuid();
        
        // Act
        await _service.UpdateAsync(updatedOrder);
        var result = await _service.GetAsync(updatedOrder.Id);
        
        // Assert
        Assert.Equal(updatedOrder.CustomerId, result.CustomerId);
    }

    [Fact]
    public async Task ValidOrder_DeleteAsync_DeletesSuccessfully()
    {
        // Arrange
        var order = EntityBuilder(Guid.NewGuid());
        
        var mappedOrder = _mapper.Map<Order>(order);
        
        _dbContext.FindAsync<Order>(mappedOrder.Id).Returns(mappedOrder);

        // Act
        await _service.DeleteAsync(mappedOrder.Id);
        
        // Assert
        _dbContext.Orders.Received().Remove(Arg.Any<Order>());
        await _dbContext.Received().SaveChangesAsync();
    }
    
    private OrderDto EntityBuilder(Guid id)
    {
        var order = new OrderDto()
        {
            Id = id,
        };

        return order;
    }
}