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
using PrinterShop.Shared.Enums;

namespace PrinterShop.InfrastructureUnitTests;

public class InfrastructureUserServicesTests
{
    private readonly ShopDbContext _dbContext;
    private readonly UserService _service;
    private readonly IMapper _mapper;

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

        _mapper = new MapperConfiguration(x => x
                .AddProfile(new MappingProfile()))
            .CreateMapper();
        
        _service = new(_dbContext, mockedValidator, _mapper);
    }

    [Fact]
    public async Task ValidUser_AddAsync_AddsSuccessfully()
    {
        // Arrange
        var user = EntityBuilder(Guid.NewGuid());
        
        // Act
        await _service.AddAsync(user);
        
        // Assert
        _dbContext.Users.Received().Add(Arg.Any<User>());
        await _dbContext.Received().SaveChangesAsync();
    }

    [Fact]
    public async Task ValidUser_GetAsync_ReturnsUser()
    {
        // Arrange
        var user = EntityBuilder(Guid.NewGuid());
        
        var mappedUser = _mapper.Map<User>(user);
        
        _dbContext.FindAsync<User>(mappedUser.Id).Returns(mappedUser);
        
        // Act
        var result = await _service.GetAsync(mappedUser.Id);
        
        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task List_GetAllAsync_ReturnsAllUsers()
    {
        // Act
        var list = await _service.GetAllAsync();
        
        // Assert
        Assert.NotNull(list);
    }

    [Fact]
    public async Task ValidUser_UpdateAsync_UpdatesSuccessfully()
    {
        // Arrange
        var user = EntityBuilder(Guid.NewGuid());
        
        var mappedUser = _mapper.Map<User>(user);
        
        _dbContext.FindAsync<User>(mappedUser.Id).Returns(mappedUser);
        
        var updatedUser = user;
        updatedUser.Email = "updated@email.com";
        
        // Act
        await _service.UpdateAsync(updatedUser);
        var result = await _service.GetAsync(updatedUser.Id);
        
        // Assert
        Assert.Equal(updatedUser.Email, result.Email);
    }

    [Fact]
    public async Task ValidUser_DeleteAsync_DeletesSuccessfully()
    {
        // Arrange
        var user = EntityBuilder(Guid.NewGuid());
        
        var mappedUser = _mapper.Map<User>(user);
        
        _dbContext.FindAsync<User>(mappedUser.Id).Returns(mappedUser);

        // Act
        await _service.DeleteAsync(mappedUser.Id);
        
        // Assert
        _dbContext.Users.Received().Remove(Arg.Any<User>());
        await _dbContext.Received().SaveChangesAsync();
    }
    
    private UserDto EntityBuilder(Guid userId)
    {
        var user = new UserDto()
        {
            Id = userId,
            Email = "test@email.com",
            Password = "Password",
            UserName = "test",
            Type = UserType.Admin,
        };

        return user;
    }
}