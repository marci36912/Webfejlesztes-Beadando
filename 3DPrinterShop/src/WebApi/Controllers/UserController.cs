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
        await _userService.AddAsync(user);
        
        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> Get(Guid id)
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
    public async Task<IActionResult> Update([FromBody] UserDto user)
    {
        await _userService.UpdateAsync(user);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _userService.DeleteAsync(id);
        
        return Ok();
    }
}