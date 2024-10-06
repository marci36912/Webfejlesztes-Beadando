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

public class InfrastructureComponentServicesTests
{
    private readonly ShopDbContext _dbContext;
    private readonly ComponentService _service;
    private readonly IMapper _mapper;

    public InfrastructureComponentServicesTests()
    {
        _dbContext = NSubstitute.Substitute.For<ShopDbContext>();
        
        var mockedComponents = (new List<Component>()).BuildMockDbSet();
        _dbContext.Components = mockedComponents;
        
        var mockedValidator = NSubstitute.Substitute.For<IValidator<ComponentDto>>();
        
        var mockedResult = NSubstitute.Substitute.For<ValidationResult>();
        mockedResult.IsValid.Returns(true);
        
        mockedValidator
            .Configure()
            .ValidateAsync(Arg.Any<ComponentDto>())
            .Returns(mockedResult);

        _mapper = new MapperConfiguration(x => x
                .AddProfile(new MappingProfile()))
            .CreateMapper();
        
        _service = new(_dbContext, mockedValidator, _mapper);
    }

    [Fact]
    public async Task ValidComponent_AddAsync_AddsSuccessfully()
    {
        // Arrange
        var component = EntityBuilder(Guid.NewGuid());
        
        // Act
        await _service.AddAsync(component);
        
        // Assert
        _dbContext.Components.Received().Add(Arg.Any<Component>());
        await _dbContext.Received().SaveChangesAsync();
    }

    [Fact]
    public async Task ValidComponents_GetAsync_ReturnsComponent()
    {
        // Arrange
        var component = EntityBuilder(Guid.NewGuid());
        
        var mappedComponent = _mapper.Map<Component>(component);
        
        _dbContext.FindAsync<Component>(component.ModelNumber).Returns(mappedComponent);
        
        // Act
        var result = await _service.GetAsync(component.ModelNumber);
        
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
    public async Task ValidComponent_UpdateAsync_UpdatesSuccessfully()
    {
        // Arrange
        var component = EntityBuilder(Guid.NewGuid());
        
        var mappedComponent = _mapper.Map<Component>(component);
        
        _dbContext.FindAsync<Component>(mappedComponent.ModelNumber).Returns(mappedComponent);
        
        var updatedComponent = component;
        updatedComponent.Brand = "NewBrand";
        
        // Act
        await _service.UpdateAsync(updatedComponent);
        var result = await _service.GetAsync(updatedComponent.ModelNumber);
        
        // Assert
        Assert.Equal(updatedComponent.Brand, result.Brand);
    }

    [Fact]
    public async Task ValidComponent_DeleteAsync_DeletesSuccessfully()
    {
        // Arrange
        var component = EntityBuilder(Guid.NewGuid());
        
        var mappedComponent = _mapper.Map<Component>(component);
        
        _dbContext.FindAsync<Component>(mappedComponent.ModelNumber).Returns(mappedComponent);

        // Act
        await _service.DeleteAsync(mappedComponent.ModelNumber);
        
        // Assert
        _dbContext.Components.Received().Remove(Arg.Any<Component>());
        await _dbContext.Received().SaveChangesAsync();
    }
    
    private ComponentDto EntityBuilder(Guid id)
    {
        var component = new ComponentDto()
        {
            ModelNumber = id,
        };

        return component;
    }
}