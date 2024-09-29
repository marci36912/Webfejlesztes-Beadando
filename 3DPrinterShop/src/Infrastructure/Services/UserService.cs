using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PrinterShop.Core.Application.Interfaces.Services;
using PrinterShop.Core.Domain.Entities;
using PrinterShop.Core.Infrastructure.Data;
using PrinterShop.Shared.Dtos;

namespace PrinterShop.Core.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly ShopDbContext _dbContext;
    private readonly IValidator<UserDto> _validator;
    private readonly IMapper _mapper;

    public UserService(ShopDbContext dbContext, IValidator<UserDto> validator, IMapper mapper)
    {
        _dbContext = dbContext;
        _validator = validator;
        _mapper = mapper;
    }
    
    public async Task AddAsync(UserDto user)
    {
        var result = await GetAsync(user.Id);

        if (result is not null)
        {
            throw new ArgumentException($"User {user.UserName} already exists");
        }
        
        var validation = await _validator.ValidateAsync(user);

        if (!validation.IsValid)
        {
            throw new ValidationException(validation.Errors);
        }
        
        var mapped = _mapper.Map<User>(user);
        
        _dbContext.Users.Add(mapped);
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserDto> GetAsync(Guid id)
    {
        var result = await _dbContext.FindAsync<User>(id);

        if (result is null)
        {
            return null;
        }
        
        var mapped = _mapper.Map<UserDto>(result);
        
        return mapped;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync() => 
        (await _dbContext.Users.ToListAsync())
        .Select(_mapper.Map<User, UserDto>);

    public async Task UpdateAsync(UserDto user)
    {
        var result = await GetAsync(user.Id);

        if (result is null)
        {
            throw new ArgumentNullException();
        }
        
        var validation = await _validator.ValidateAsync(user);

        if (!validation.IsValid)
        {
            throw new ValidationException(validation.Errors);
        }
        
        result.UserName = user.UserName;
        result.Email = user.Email;
        result.Password = user.Password;
        result.Type = user.Type;
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var result = await _dbContext.FindAsync<User>(id);

        if (result is null)
        {
            throw new ArgumentNullException();
        }
        
        _dbContext.Users.Remove(result);
        
        await _dbContext.SaveChangesAsync();
    }
}