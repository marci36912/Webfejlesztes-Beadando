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
        await _componentService.AddAsync(component);
        
        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ComponentDto>> Get(Guid id)
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
    public async Task<IActionResult> Update([FromBody] ComponentDto component)
    {
        await _componentService.UpdateAsync(component);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _componentService.DeleteAsync(id);
        
        return Ok();
    }
}