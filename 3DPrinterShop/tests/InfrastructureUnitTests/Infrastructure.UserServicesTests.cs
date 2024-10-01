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
using PrinterShop.Shared.Validators;

namespace PrinterShop.InfrastructureUnitTests;

public class InfrastructureUserServicesTests
{
    ShopDbContext _dbContext;
    private UserService _userService;

    public InfrastructureUserServicesTests()
    {
        _dbContext = NSubstitute.Substitute.For<ShopDbContext>();
        
        var mockedUsers = (new List<User>()).BuildMockDbSet();
        _dbContext.Users = mockedUsers;
        
        var mockedValidator = NSubstitute.Substitute.For<IValidator<UserDto>>();
        
        var mockedResult = NSubstitute.Substitute.For<ValidationResult>();
        mockedResult.IsValid.Returns(true);
        
        mockedValidator
            .Configure()
            .ValidateAsync(Arg.Any<UserDto>())
            .Returns(mockedResult);

        var mockedMapper = new MapperConfiguration(x => x
                .AddProfile(new MappingProfile()))
            .CreateMapper();
        
        _userService = new(_dbContext, mockedValidator, mockedMapper);
    }

    [Fact]
    public async Task Test()
    {
        var x = await _userService.GetAllAsync();
        
        Assert.NotNull(x);
    }
}