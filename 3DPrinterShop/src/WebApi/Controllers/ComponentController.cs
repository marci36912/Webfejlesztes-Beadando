using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PrinterShop.Core.Application.Interfaces.Services;
using PrinterShop.Shared.Dtos;

namespace PrinterShop.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ComponentController : ControllerBase
{
    private readonly IComponentService _componentService;

    public ComponentController(IComponentService componentService)
    {
        _componentService = componentService;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] ComponentDto component)
    {
        try
        {
            await _componentService.AddAsync(component);
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
    public async Task<ActionResult<ComponentDto>> GetAsync(Guid id)
    {
        var result = await _componentService.GetAsync(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<ComponentDto>>> GetAllAsync()
    {
        var result = await _componentService.GetAllAsync();
        
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] ComponentDto component)
    {
        try
        {
            await _componentService.UpdateAsync(component);
        }
        catch (ArgumentNullException e)
        {
            await AddAsync(component);
            
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
            await _componentService.DeleteAsync(id);
        }
        catch (ArgumentNullException e)
        {
            return NotFound();
        }
        
        return Ok();
    }
}