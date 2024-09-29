using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PrinterShop.Core.Application.Interfaces.Services;
using PrinterShop.Shared.Dtos;

namespace PrinterShop.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] UserDto user)
    {
        try
        {
            await _userService.AddAsync(user);
        }
        catch (ArgumentException e)
        {
            return Conflict(e.Message);
        }
        catch (ValidationException e)
        {
            return Conflict(e.Message);
        }
        
        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> GetAsync(Guid id)
    {
        var result = await _userService.GetAsync(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAllAsync()
    {
        var result = await _userService.GetAllAsync();
        
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UserDto user)
    {
        try
        {
            await _userService.UpdateAsync(user);
        }
        catch (ArgumentNullException e)
        {
            await AddAsync(user);
            
            return Created();
        }
        catch (ValidationException e)
        {
            return Conflict(e.Message);
        }
        
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            await _userService.DeleteAsync(id);
        }
        catch (ArgumentNullException e)
        {
            return NotFound();
        }
        
        return Ok();
    }
}