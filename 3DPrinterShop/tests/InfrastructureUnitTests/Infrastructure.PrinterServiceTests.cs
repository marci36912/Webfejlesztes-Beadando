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

public class InfrastructurePrinterServicesTests
{
    private readonly ShopDbContext _dbContext;
    private readonly PrinterService _service;
    private readonly IMapper _mapper;

    public InfrastructurePrinterServicesTests()
    {
        _dbContext = NSubstitute.Substitute.For<ShopDbContext>();
        
        var mockedPrinters = (new List<Printer>()).BuildMockDbSet();
        _dbContext.Printers = mockedPrinters;
        
        var mockedValidator = NSubstitute.Substitute.For<IValidator<PrinterDto>>();
        
        var mockedResult = NSubstitute.Substitute.For<ValidationResult>();
        mockedResult.IsValid.Returns(true);
        
        mockedValidator
            .Configure()
            .ValidateAsync(Arg.Any<PrinterDto>())
            .Returns(mockedResult);

        _mapper = new MapperConfiguration(x => x
                .AddProfile(new MappingProfile()))
            .CreateMapper();
        
        _service = new(_dbContext, mockedValidator, _mapper);
    }

    [Fact]
    public async Task ValidPrinter_AddAsync_AddsSuccessfully()
    {
        // Arrange
        var printer = EntityBuilder(Guid.NewGuid());
        
        // Act
        await _service.AddAsync(printer);
        
        // Assert
        _dbContext.Printers.Received().Add(Arg.Any<Printer>());
        await _dbContext.Received().SaveChangesAsync();
    }

    [Fact]
    public async Task ValidPrinter_GetAsync_ReturnsPrinter()
    {
        // Arrange
        var printer = EntityBuilder(Guid.NewGuid());
        
        var mappedPrinter = _mapper.Map<Printer>(printer);
        
        _dbContext.FindAsync<Printer>(printer.Id).Returns(mappedPrinter);
        
        // Act
        var result = await _service.GetAsync(printer.Id);
        
        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task List_GetAllAsync_ReturnsAllPrinters()
    {
        // Act
        var list = await _service.GetAllAsync();
        
        // Assert
        Assert.NotNull(list);
    }

    [Fact]
    public async Task ValidPrinter_UpdateAsync_UpdatesSuccessfully()
    {
        // Arrange
        var printer = EntityBuilder(Guid.NewGuid());
        
        var mappedPrinter = _mapper.Map<Printer>(printer);
        
        _dbContext.FindAsync<Printer>(mappedPrinter.Id).Returns(mappedPrinter);
        
        var updatedPrinter = printer;
        updatedPrinter.Brand = "NewBrand";
        
        // Act
        await _service.UpdateAsync(updatedPrinter);
        var result = await _service.GetAsync(updatedPrinter.Id);
        
        // Assert
        Assert.Equal(updatedPrinter.Brand, result.Brand);
    }

    [Fact]
    public async Task ValidPrinter_DeleteAsync_DeletesSuccessfully()
    {
        // Arrange
        var printer = EntityBuilder(Guid.NewGuid());
        
        var mappedPrinter = _mapper.Map<Printer>(printer);
        
        _dbContext.FindAsync<Printer>(mappedPrinter.Id).Returns(mappedPrinter);

        // Act
        await _service.DeleteAsync(mappedPrinter.Id);
        
        // Assert
        _dbContext.Printers.Received().Remove(Arg.Any<Printer>());
        await _dbContext.Received().SaveChangesAsync();
    }
    
    private PrinterDto EntityBuilder(Guid id)
    {
        var printer = new PrinterDto()
        {
            Id = id,
            Brand = "Brand",
        };

        return printer;
    }
}